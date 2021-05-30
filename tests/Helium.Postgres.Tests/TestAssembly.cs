using Helium.Postgres.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.Postgres.Tests
{
    [TestClass]
    public sealed class TestAssembly : TestClassBase
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Postgres.DropDatabase(DatabaseName);
            Postgres.CreateDatabase(DatabaseName);
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            Postgres.DropDatabase(DatabaseName);
        }
    }
}
