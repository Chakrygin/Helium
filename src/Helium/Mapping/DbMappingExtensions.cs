using System;

namespace Helium.Mapping
{
    internal static class DbMappingExtensions
    {
        public static bool IsDynamicType(this Type type)
        {
            return type == typeof(object);
        }
    }
}
