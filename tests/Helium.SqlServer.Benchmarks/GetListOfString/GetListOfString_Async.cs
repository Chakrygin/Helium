using System.Collections.Generic;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using Dapper;

using Microsoft.Data.SqlClient;

namespace Helium.SqlServer.Benchmarks.GetListOfString
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 100)]
    public class GetListOfString_Async_10 : GetListOfString_Async
    {
        [Params(10)]
        public override int RowCount { get; set; }
    }

    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 50)]
    public class GetListOfString_Async_100 : GetListOfString_Async
    {
        [Params(100)]
        public override int RowCount { get; set; }
    }

    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 20)]
    public class GetListOfString_Async_1000 : GetListOfString_Async
    {
        [Params(1000)]
        public override int RowCount { get; set; }
    }

    public abstract class GetListOfString_Async : GetListOfString_Base
    {
        [Benchmark(Baseline = true)]
        public async Task<List<string>> HandCoded_ByColumnName()
        {
            var result = new List<string>(RowCount);

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(Query, connection);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var value = (string) reader["Value"];
                result.Add(value);
            }

            return result;
        }

        [Benchmark]
        public async Task<List<string>> HandCoded_ByColumnIndex()
        {
            var result = new List<string>(RowCount);

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(Query, connection);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var value = reader.GetString(0);
                result.Add(value);
            }

            return result;
        }

        [Benchmark]
        public async Task<List<string>> Helium()
        {
            var result = await Database
                .Query(Query)
                .ExecuteAsync<List<string>>();

            return result;
        }

        [Benchmark]
        public async Task<List<string>> Dapper()
        {
            using var connection = new SqlConnection(ConnectionString);

            var result = await connection
                .QueryAsync<string>(Query);

            return result.AsList();
        }
    }
}
