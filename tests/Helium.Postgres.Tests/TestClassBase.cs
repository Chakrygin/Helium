using Helium.Postgres.Helpers;

namespace Helium.Postgres.Tests
{
    public abstract class TestClassBase
    {
        protected const string DatabaseName = "helium_tests";

        protected static DbClient Database { get; } = PostgresUtils.CreateClient(DatabaseName);

        protected static DbClient Postgres { get; } = PostgresUtils.CreateClient("postgres");
    }
}
