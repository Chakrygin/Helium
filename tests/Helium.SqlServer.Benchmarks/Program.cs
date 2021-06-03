using System;
using System.Diagnostics;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

using Helium.SqlServer.Benchmarks.GetListOfEntity;
using Helium.SqlServer.Benchmarks.GetListOfString;

namespace Helium.SqlServer.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfig? config = Debugger.IsAttached
                ? new DebugInProcessConfig()
                : null;

            BenchmarkRunner.Run<GetListOfEntity_Async_100>(config);
        }
    }
}
