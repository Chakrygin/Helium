using System;

using Helium.SqlServer.Helpers;

namespace Helium.SqlServer.Tests
{
    public abstract class TestClassBase
    {
        protected const string DatabaseName = "Helium_Tests";

        protected static DbClient Database { get; } = SqlServerUtils.CreateClient(DatabaseName);

        protected static DbClient Master { get; } = SqlServerUtils.CreateClient("master");

        protected static SqlServerTableHelper<TRow> CreateTableHelper<TRow>()
        {
            var guid = Guid.NewGuid();
            var name = "Test_" + guid.ToString("N");
            var connectionString = SqlServerUtils.CreateConnectionString(DatabaseName);
            return new SqlServerTableHelper<TRow>(name, connectionString);
        }
    }
}
