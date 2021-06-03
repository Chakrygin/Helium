using BenchmarkDotNet.Attributes;

using Helium.SqlServer.Helpers;

namespace Helium.SqlServer.Benchmarks.GetListOfString
{
    public abstract class GetListOfString_Base : BenchmarkClassBase
    {
        public static readonly string Query = $@"
            select [Value] from [Item]
            order by [Id] asc";

        public SqlServerTableHelper<Item> Table { get; } = CreateTableHelper<Item>("Item");

        public abstract int RowCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Master.DropDatabase(DatabaseName);
            Master.CreateDatabase(DatabaseName);

            Table.Create(
                "[Id]    int primary key",
                "[Value] nvarchar(1000) not null");

            Table.Fill(RowCount, (id, faker) => new Item
            {
                Id = id,
                Value = faker.Lorem.Sentence(),
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
            public string Value { get; set; } = null!;
        }
    }
}
