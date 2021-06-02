using System;
using System.Collections;
using System.Collections.Generic;

using Helium.Provider;

namespace Helium.Mapping
{
    internal static class DbMappingExtensions
    {
        public static bool IsDynamicType(this Type type)
        {
            return type == typeof(object);
        }

        public static bool IsTupleType(this Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            // ReSharper disable once PossibleNullReferenceException
            return
                type.FullName.StartsWith("System.Tuple`") ||
                type.FullName.StartsWith("System.ValueTuple`");
        }

        public static bool IsScalarType(this Type type, DbDataReaderTypeDescriptor dataReaderType)
        {
            if (type.IsArray)
            {
                type = type.GetElementType()!;
            }

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                type = underlyingType;
            }

            if (type.IsEnum)
            {
                type = type.GetEnumUnderlyingType();
            }

            return dataReaderType.IsNativeType(type);
        }

        public static bool IsEnumerableType(this Type type)
        {
            return type.FindInterface(typeof(IEnumerable)) != null;
        }

        public static bool IsCollectionType(this Type type)
        {
            return type.FindInterface(typeof(ICollection)) != null;
        }

        public static bool IsGenericCollectionType(this Type type, out Type itemType)
        {
            return type.FindInterface(typeof(ICollection<>), out itemType) != null
                || type.FindInterface(typeof(IReadOnlyCollection<>), out itemType) != null;
        }

        public static bool IsDictionaryType(this Type type)
        {
            return type.FindInterface(typeof(IDictionary)) != null;
        }

        public static bool IsGenericDictionaryType(this Type type, out Type keyType, out Type valueType)
        {
            return type.FindInterface(typeof(IDictionary<,>), out keyType, out valueType) != null
                || type.FindInterface(typeof(IReadOnlyDictionary<,>), out keyType, out valueType) != null;
        }

        private static Type? FindInterface(this Type type, Type interfaceType, out Type argument)
        {
            argument = null!;

            var result = FindInterface(type, interfaceType);
            if (result != null)
            {
                var arguments = result.GetGenericArguments();
                argument = arguments[0];
            }

            return result;
        }

        private static Type? FindInterface(this Type type, Type interfaceType, out Type argument0, out Type argument1)
        {
            argument0 = null!;
            argument1 = null!;

            var result = FindInterface(type, interfaceType);
            if (result != null)
            {
                var arguments = result.GetGenericArguments();
                argument0 = arguments[0];
                argument1 = arguments[1];
            }

            return result;
        }

        private static Type? FindInterface(this Type type, Type interfaceType)
        {
            if (interfaceType.IsGenericTypeDefinition)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == interfaceType)
                {
                    return type;
                }

                return type.GetInterface(interfaceType.FullName);
            }

            if (interfaceType.IsAssignableFrom(type))
            {
                return interfaceType;
            }

            return null;
        }
    }
}
