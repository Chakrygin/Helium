using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Reflection;

using Helium.Common.Descriptors;

namespace Helium.Provider
{
    internal sealed class DbDataReaderTypeDescriptor : TypeDescriptorBase
    {
        private readonly Dictionary<Type, MethodInfo> _getNativeValueMethods;
        private readonly MethodInfo _getFieldValueMethod;

        public DbDataReaderTypeDescriptor(Type dataReaderType) :
            base(dataReaderType)
        {
            HasRowsProperty = Type.GetProperty(nameof(DbDataReader.HasRows))!;
            FieldCountProperty = Type.GetProperty(nameof(DbDataReader.FieldCount))!;
            ReadMethod = Type.GetMethod(nameof(DbDataReader.Read))!;
            NextResultMethod = Type.GetMethod(nameof(DbDataReader.NextResult))!;
            IsDBNullMethod = Type.GetMethod(nameof(DbDataReader.IsDBNull), new[] {typeof(int)})!;
            // GetValueMethod = Type.GetMethod(nameof(DbDataReader.GetValue), new[] {typeof(int)});
            // GetNativeValueMethods = CreateGetNativeValueMethods(Type);

            _getNativeValueMethods = CreateGetNativeValueMethods(dataReaderType);
            _getFieldValueMethod = Type.GetMethod(nameof(DbDataReader.GetFieldValue), new[] {typeof(int)})!;
        }

        public PropertyInfo HasRowsProperty { get; }

        public PropertyInfo FieldCountProperty { get; }

        public MethodInfo ReadMethod { get; }

        public MethodInfo NextResultMethod { get; }

        public MethodInfo IsDBNullMethod { get; }

        public MethodInfo? GetNativeValueMethod(Type type)
        {
            _getNativeValueMethods.TryGetValue(type, out var method);

            return method;
        }

        public MethodInfo GetFieldValueMethod(Type type)
        {
            return _getFieldValueMethod.MakeGenericMethod(type);
        }

        private static Dictionary<Type, MethodInfo> CreateGetNativeValueMethods(Type dataReaderType)
        {
            var result = new Dictionary<Type, MethodInfo>();

            var methods = dataReaderType.GetMethods();
            foreach (var method in methods)
            {
                if (method.IsStatic)
                    continue;

                if (method.IsSpecialName)
                    continue;

                var returnType = method.ReturnType;
                if (typeof(IDisposable).IsAssignableFrom(returnType))
                    continue;

                var methodName = returnType != typeof(float)
                    ? "Get" + returnType.Name
                    : "GetFloat";
                if (method.Name != methodName)
                    continue;

                var parameters = method.GetParameters();
                if (parameters.Length != 1)
                    continue;

                var parameter = parameters[0];
                var parameterType = parameter.ParameterType;
                if (parameterType != typeof(int))
                    continue;

                result.Add(method.ReturnType, method);
            }

            return result;
        }
    }
}
