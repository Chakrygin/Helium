using System.Collections.Generic;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using Dapper;

using Microsoft.Data.SqlClient;

namespace Helium.SqlServer.Benchmarks.GetListOfEntity
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 100)]
    public class GetListOfEntity_Async_10 : GetListOfEntity_Async
    {
        [Params(10)]
        public override int RowCount { get; set; }
    }

    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 50)]
    public class GetListOfEntity_Async_100 : GetListOfEntity_Async
    {
        [Params(100)]
        public override int RowCount { get; set; }
    }

    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 10, targetCount: 50, invocationCount: 20)]
    public class GetListOfEntity_Async_1000 : GetListOfEntity_Async
    {
        [Params(1000)]
        public override int RowCount { get; set; }
    }

    public abstract class GetListOfEntity_Async : GetListOfEntity_Base
    {
        [Benchmark(Baseline = true)]
        public async Task<List<Item>> HandCoded_ByColumnName()
        {
            var result = new List<Item>(RowCount);

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(Query, connection);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var item = new Item
                {
                    Id = (int) reader["Id"],

                    IntValue0 = (int) reader["IntValue0"],
                    IntValue1 = (int) reader["IntValue1"],
                    IntValue2 = (int) reader["IntValue2"],
                    IntValue3 = (int) reader["IntValue3"],
                    IntValue4 = (int) reader["IntValue4"],
                    IntValue5 = (int) reader["IntValue5"],
                    IntValue6 = (int) reader["IntValue6"],
                    IntValue7 = (int) reader["IntValue7"],
                    IntValue8 = (int) reader["IntValue8"],
                    IntValue9 = (int) reader["IntValue9"],

                    StrValue0 = (string) reader["StrValue0"],
                    StrValue1 = (string) reader["StrValue1"],
                    StrValue2 = (string) reader["StrValue2"],
                    StrValue3 = (string) reader["StrValue3"],
                    StrValue4 = (string) reader["StrValue4"],
                    StrValue5 = (string) reader["StrValue5"],
                    StrValue6 = (string) reader["StrValue6"],
                    StrValue7 = (string) reader["StrValue7"],
                    StrValue8 = (string) reader["StrValue8"],
                    StrValue9 = (string) reader["StrValue9"],
                };

                result.Add(item);
            }

            return result;
        }

        [Benchmark]
        public async Task<List<Item>> HandCoded_ByColumnIndex()
        {
            var result = new List<Item>(RowCount);

            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(Query, connection);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            var idIndex = reader.GetOrdinal("Id");

            var intValue0Index = reader.GetOrdinal("IntValue0");
            var intValue1Index = reader.GetOrdinal("IntValue1");
            var intValue2Index = reader.GetOrdinal("IntValue2");
            var intValue3Index = reader.GetOrdinal("IntValue3");
            var intValue4Index = reader.GetOrdinal("IntValue4");
            var intValue5Index = reader.GetOrdinal("IntValue5");
            var intValue6Index = reader.GetOrdinal("IntValue6");
            var intValue7Index = reader.GetOrdinal("IntValue7");
            var intValue8Index = reader.GetOrdinal("IntValue8");
            var intValue9Index = reader.GetOrdinal("IntValue9");

            var strValue0Index = reader.GetOrdinal("StrValue0");
            var strValue1Index = reader.GetOrdinal("StrValue1");
            var strValue2Index = reader.GetOrdinal("StrValue2");
            var strValue3Index = reader.GetOrdinal("StrValue3");
            var strValue4Index = reader.GetOrdinal("StrValue4");
            var strValue5Index = reader.GetOrdinal("StrValue5");
            var strValue6Index = reader.GetOrdinal("StrValue6");
            var strValue7Index = reader.GetOrdinal("StrValue7");
            var strValue8Index = reader.GetOrdinal("StrValue8");
            var strValue9Index = reader.GetOrdinal("StrValue9");

            while (reader.Read())
            {
                var item = new Item
                {
                    Id = reader.GetInt32(idIndex),

                    IntValue0 = reader.GetInt32(intValue0Index),
                    IntValue1 = reader.GetInt32(intValue1Index),
                    IntValue2 = reader.GetInt32(intValue2Index),
                    IntValue3 = reader.GetInt32(intValue3Index),
                    IntValue4 = reader.GetInt32(intValue4Index),
                    IntValue5 = reader.GetInt32(intValue5Index),
                    IntValue6 = reader.GetInt32(intValue6Index),
                    IntValue7 = reader.GetInt32(intValue7Index),
                    IntValue8 = reader.GetInt32(intValue8Index),
                    IntValue9 = reader.GetInt32(intValue9Index),

                    StrValue0 = reader.GetString(strValue0Index),
                    StrValue1 = reader.GetString(strValue1Index),
                    StrValue2 = reader.GetString(strValue2Index),
                    StrValue3 = reader.GetString(strValue3Index),
                    StrValue4 = reader.GetString(strValue4Index),
                    StrValue5 = reader.GetString(strValue5Index),
                    StrValue6 = reader.GetString(strValue6Index),
                    StrValue7 = reader.GetString(strValue7Index),
                    StrValue8 = reader.GetString(strValue8Index),
                    StrValue9 = reader.GetString(strValue9Index),
                };

                result.Add(item);
            }

            return result;
        }

        [Benchmark]
        public async Task<List<Item>> Helium()
        {
            var result = await Database
                .Query(Query)
                .ExecuteAsync<List<Item>>();

            return result;
        }

        [Benchmark]
        public async Task<List<Item>> Dapper()
        {
            using var connection = new SqlConnection(ConnectionString);

            var result = await connection
                .QueryAsync<Item>(Query);

            return result.AsList();
        }
    }
}
