using System;

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
    }
}
