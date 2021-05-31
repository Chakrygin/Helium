using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class PrintExtensions
    {
        private static readonly JsonSerializerOptions Options = new() {WriteIndented = true};

        [Conditional("DEBUG")]
        public static void Print<T>(this T value)
        {
            var json = value is ITuple tuple
                ? JsonSerializer.Serialize(ToArray(tuple), typeof(object[]), Options)
                : JsonSerializer.Serialize(value, typeof(T), Options);

            Console.WriteLine(json);
            Console.WriteLine();
        }

        private static object?[] ToArray(ITuple tuple)
        {
            var result = new object?[tuple.Length];

            for (var index = 0; index < result.Length; index++)
            {
                result[index] = tuple[index];
            }

            return result;
        }
    }
}
