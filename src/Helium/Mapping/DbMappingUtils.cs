using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Reflection;

namespace Helium.Mapping
{
    internal static class DbMappingUtils
    {
        public static MethodInfo MapDynamicMethod { get; } =
            typeof(DbMappingUtils).GetMethod(nameof(MapDynamic))!;

        public static dynamic MapDynamic(DbDataReader reader)
        {
            var result = (IDictionary<string, object>) new ExpandoObject();

            var fieldCount = reader.FieldCount;
            for (var ordinal = 0; ordinal < fieldCount; ordinal++)
            {
                var name = reader.GetName(ordinal);
                var value = reader.GetValue(ordinal);

                result.Add(name, value);
            }

            return result;
        }

        public static MethodInfo GetOrdinalsMethod { get; } =
            typeof(DbMappingUtils).GetMethod(nameof(GetOrdinals))!;

        public static int[] GetOrdinals(DbDataReader reader, Dictionary<string, int> ordinalIndices)
        {
            var result = new int[ordinalIndices.Count];

            for (var index = 0; index < result.Length; index++)
            {
                result[index] = -1;
            }

            var fieldCount = reader.FieldCount;
            for (var ordinal = 0; ordinal < fieldCount; ordinal++)
            {
                var name = reader.GetName(ordinal);
                if (ordinalIndices.TryGetValue(name, out var ordinalIndex))
                {
                    result[ordinalIndex] = ordinal;
                }
            }

            return result;
        }
    }
}
