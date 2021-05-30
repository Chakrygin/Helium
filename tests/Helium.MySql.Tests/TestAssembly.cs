using Helium.MySql.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests
{
    [TestClass]
    public sealed class TestAssembly : TestClassBase
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            MySql.DropDatabase(DatabaseName);
            MySql.CreateDatabase(DatabaseName);
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            MySql.DropDatabase(DatabaseName);
        }
    }
}
