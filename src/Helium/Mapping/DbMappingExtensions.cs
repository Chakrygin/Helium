using System;

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
    }
}
