using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Helium.Annotations;
using Helium.Common.Descriptors;

namespace Helium.Mapping.Descriptors
{
    internal sealed class DbEntityTypeDescriptor : TypeDescriptorBase
    {
        public DbEntityTypeDescriptor(Type entityType) :
            base(entityType)
        {
            var underlyingType = Nullable.GetUnderlyingType(entityType);
            if (underlyingType != null)
            {
                entityType = underlyingType;
                NullableConstructor = Type.GetConstructor(new[] {entityType});
            }

            Constructor = GetEntityConstructor(entityType);

            var parameters = GetEntityParameters(Constructor);
            var properties = GetEntityProperties(entityType);

            Parameters = new List<DbEntityParameterDescriptor>();
            Properties = new List<DbEntityPropertyDescriptor>();
            OrdinalIndices = new Dictionary<string, int>();

            InitEntityParameters(parameters, properties);
            InitEntityProperties(parameters, properties);
        }

        public bool IsValueType => Type.IsValueType;

        public ConstructorInfo? Constructor { get; }

        public ConstructorInfo? NullableConstructor { get; }

        public List<DbEntityParameterDescriptor> Parameters { get; }

        public List<DbEntityPropertyDescriptor> Properties { get; }

        public Dictionary<string, int> OrdinalIndices { get; }

        private static ConstructorInfo? GetEntityConstructor(Type entityType)
        {
            ConstructorInfo? defaultConstructor = null;
            List<ConstructorInfo>? otherConstructors = null;

            var constructors = entityType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    defaultConstructor = constructor;
                    continue;
                }

                if (otherConstructors == null)
                    otherConstructors = new List<ConstructorInfo>(1);

                otherConstructors.Add(constructor);
            }

            if (defaultConstructor != null)
                return defaultConstructor;

            if (otherConstructors == null || otherConstructors.Count == 0)
            {
                if (entityType.IsValueType)
                    return null;
            }

            if (otherConstructors == null || otherConstructors.Count != 1)
            {
                var message =
                    $"Entity type {entityType} should have a public default " +
                    $"constructor or single constructor with parameters.";

                throw new InvalidOperationException(message);
            }

            return otherConstructors.Single();
        }

        private static List<ParameterInfo> GetEntityParameters(ConstructorInfo? constructor)
        {
            return constructor != null
                ? constructor.GetParameters().ToList()
                : new List<ParameterInfo>();
        }

        private static List<PropertyInfo> GetEntityProperties(Type entityType)
        {
            var result = new List<PropertyInfo>();

            var types = GetTypeHierarchy(entityType);
            var indices = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var type in types)
            {
                var properties = type.GetProperties(
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (var property in properties)
                {
                    if (indices.TryGetValue(property.Name, out var index))
                    {
                        result[index] = property;
                    }
                    else
                    {
                        indices.Add(property.Name, result.Count);
                        result.Add(property);
                    }
                }
            }

            result.RemoveAll(property =>
            {
                var columnIgnore = property.GetCustomAttribute<ColumnIgnoreAttribute>();
                return columnIgnore != null;
            });

            return result;
        }

        private static List<Type> GetTypeHierarchy(Type type)
        {
            var result = new List<Type>();

            while (type.BaseType != null)
            {
                result.Add(type);
                type = type.BaseType;
            }

            result.Reverse();

            return result;
        }

        private void InitEntityParameters(List<ParameterInfo> parameters, List<PropertyInfo> properties)
        {
            foreach (var parameter in parameters)
            {
                var property = properties
                    .Single(x => x.Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase));

                var columnName = GetColumnName(property);
                var ordinalIndex = OrdinalIndices.Count;
                var descriptor = new DbEntityParameterDescriptor(parameter, columnName, ordinalIndex);

                Parameters.Add(descriptor);
                OrdinalIndices.Add(columnName, ordinalIndex);
            }
        }

        private void InitEntityProperties(List<ParameterInfo> parameters, List<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var parameter = parameters
                    .SingleOrDefault(x => x.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase));

                if (parameter != null)
                    continue;

                var columnName = GetColumnName(property);
                var ordinalIndex = OrdinalIndices.Count;
                var descriptor = new DbEntityPropertyDescriptor(property, columnName, ordinalIndex);

                Properties.Add(descriptor);
                OrdinalIndices.Add(columnName, ordinalIndex);
            }
        }

        private static string GetColumnName(PropertyInfo property)
        {
            var column = property.GetCustomAttribute<ColumnAttribute>();
            var columnName = column != null && column.Name.Length > 0 ? column.Name : property.Name;

            return columnName;
        }
    }
}
