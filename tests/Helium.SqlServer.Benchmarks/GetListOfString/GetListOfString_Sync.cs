using System.Collections.Generic;

using BenchmarkDotNet.Attributes;

using Dapper;

using Microsoft.Data.SqlClient;

namespace Helium.SqlServer.Benchmarks.GetListOfString
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 100)]
    public class GetListOfString_Sync_10 : GetListOfString_Sync
    {
        [Params(10)]
        public override int RowCount { get; set; }
    }

    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 50)]
    public class GetListOfString_Sync_100 : GetListOfString_Sync
    {
        [Params(100)]
        public override int RowCount { get; set; }
    }

    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 20)]
    public class GetListOfString_Sync_1000 : GetListOfString_Sync
    {
        [Params(1000)]
        public override int RowCount { get; set; }
    }

    public abstract class GetListOfString_Sync : GetListOfString_Base
    {
        [Benchmark(Baseline = true)]
        public List<string> HandCoded_ByColumnName()
        {
            var result = new List<string>(RowCount);

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(Query, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var value = (string) reader["Value"];
                result.Add(value);
            }

            return result;
        }

        [Benchmark]
        public List<string> HandCoded_ByColumnIndex()
        {
            var result = new List<string>(RowCount);

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(Query, connection);

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var value = reader.GetString(0);
                result.Add(value);
            }

            return result;
        }

        [Benchmark]
        public List<string> Helium()
        {
            var result = Database
                .Query(Query)
                .Execute<List<string>>();

            return result;
        }

        [Benchmark]
        public List<string> Dapper()
        {
            using var connection = new SqlConnection(ConnectionString);

            var result = connection
                .Query<string>(Query);

            return result.AsList();
        }
    }
}
