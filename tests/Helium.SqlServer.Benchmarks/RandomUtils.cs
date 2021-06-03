using System;

namespace Helium.SqlServer.Benchmarks
{
    public static class RandomUtils
    {
        private static readonly Random Random = new();

        public static int GetRandomInt()
        {
            return Random.Next();
        }

        public static string GetRandomString()
        {
            var guid = Guid.NewGuid();
            return guid.ToString("D");
        }
    }
}
