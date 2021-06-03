using System;

using BenchmarkDotNet.Attributes;

using Helium.SqlServer.Helpers;

namespace Helium.SqlServer.Benchmarks.GetListOfEntity
{
    public abstract class GetListOfEntity_Base : BenchmarkClassBase
    {
        public static readonly string Query = $@"
            select [Id],
                [IntValue0], [IntValue1], [IntValue2], [IntValue3], [IntValue4],
                [IntValue5], [IntValue6], [IntValue7], [IntValue8], [IntValue9],
                [StrValue0], [StrValue1], [StrValue2], [StrValue3], [StrValue4],
                [StrValue5], [StrValue6], [StrValue7], [StrValue8], [StrValue9]
            from [Item]
            order by [Id] asc";

        public SqlServerTableHelper<Item> Table { get; } = CreateTableHelper<Item>("Item");

        public abstract int RowCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Master.DropDatabase(DatabaseName);
            Master.CreateDatabase(DatabaseName);

            Table.Create(
                "[Id]        int primary key",
                "[IntValue0] int not null",
                "[IntValue1] int not null",
                "[IntValue2] int not null",
                "[IntValue3] int not null",
                "[IntValue4] int not null",
                "[IntValue5] int not null",
                "[IntValue6] int not null",
                "[IntValue7] int not null",
                "[IntValue8] int not null",
                "[IntValue9] int not null",
                "[StrValue0] nvarchar(1000) not null",
                "[StrValue1] nvarchar(1000) not null",
                "[StrValue2] nvarchar(1000) not null",
                "[StrValue3] nvarchar(1000) not null",
                "[StrValue4] nvarchar(1000) not null",
                "[StrValue5] nvarchar(1000) not null",
                "[StrValue6] nvarchar(1000) not null",
                "[StrValue7] nvarchar(1000) not null",
                "[StrValue8] nvarchar(1000) not null",
                "[StrValue9] nvarchar(1000) not null");

            Table.Fill(RowCount, id => new Item
            {
                Id = id,

                IntValue0 = RandomUtils.GetRandomInt(),
                IntValue1 = RandomUtils.GetRandomInt(),
                IntValue2 = RandomUtils.GetRandomInt(),
                IntValue3 = RandomUtils.GetRandomInt(),
                IntValue4 = RandomUtils.GetRandomInt(),
                IntValue5 = RandomUtils.GetRandomInt(),
                IntValue6 = RandomUtils.GetRandomInt(),
                IntValue7 = RandomUtils.GetRandomInt(),
                IntValue8 = RandomUtils.GetRandomInt(),
                IntValue9 = RandomUtils.GetRandomInt(),

                StrValue0 = RandomUtils.GetRandomString(),
                StrValue1 = RandomUtils.GetRandomString(),
                StrValue2 = RandomUtils.GetRandomString(),
                StrValue3 = RandomUtils.GetRandomString(),
                StrValue4 = RandomUtils.GetRandomString(),
                StrValue5 = RandomUtils.GetRandomString(),
                StrValue6 = RandomUtils.GetRandomString(),
                StrValue7 = RandomUtils.GetRandomString(),
                StrValue8 = RandomUtils.GetRandomString(),
                StrValue9 = RandomUtils.GetRandomString(),
            });
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Master.DropDatabase(DatabaseName);
        }

        public sealed class Item
        {
            public int Id { get; set; }

            public int IntValue0 { get; set; }
            public int IntValue1 { get; set; }
            public int IntValue2 { get; set; }
            public int IntValue3 { get; set; }
            public int IntValue4 { get; set; }
            public int IntValue5 { get; set; }
            public int IntValue6 { get; set; }
            public int IntValue7 { get; set; }
            public int IntValue8 { get; set; }
            public int IntValue9 { get; set; }

            public string StrValue0 { get; set; } = null!;
            public string StrValue1 { get; set; } = null!;
            public string StrValue2 { get; set; } = null!;
            public string StrValue3 { get; set; } = null!;
            public string StrValue4 { get; set; } = null!;
            public string StrValue5 { get; set; } = null!;
            public string StrValue6 { get; set; } = null!;
            public string StrValue7 { get; set; } = null!;
            public string StrValue8 { get; set; } = null!;
            public string StrValue9 { get; set; } = null!;
        }
    }
}
