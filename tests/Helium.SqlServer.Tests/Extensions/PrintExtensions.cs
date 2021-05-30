using System.Diagnostics;
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
            var json = JsonSerializer.Serialize(value, typeof(T), Options);

            Console.WriteLine(json);
            Console.WriteLine();
        }
    }
}
