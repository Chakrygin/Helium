using Helium.SqlServer.Helpers;

namespace Helium.SqlServer.Benchmarks
{
    public abstract class BenchmarkClassBase
    {
        public const string DatabaseName = "Helium_Benchmarks";

        public static DbClient Database { get; } = SqlServerUtils.CreateClient(DatabaseName);

        public static DbClient Master { get; } = SqlServerUtils.CreateClient("master");

        public static string ConnectionString { get; } = SqlServerUtils.CreateConnectionString(DatabaseName);

        protected static SqlServerTableHelper<TRow> CreateTableHelper<TRow>(string name)
        {
            var connectionString = SqlServerUtils.CreateConnectionString(DatabaseName);
            return new SqlServerTableHelper<TRow>(name, connectionString);
        }
    }
}
