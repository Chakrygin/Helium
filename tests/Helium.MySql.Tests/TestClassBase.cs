using Helium.MySql.Helpers;

namespace Helium.MySql.Tests
{
    public abstract class TestClassBase
    {
        protected const string DatabaseName = "helium_tests";

        protected static DbClient Database { get; } = MySqlUtils.CreateClient(DatabaseName);

        protected static DbClient MySql { get; } = MySqlUtils.CreateClient("mysql");
    }
}
