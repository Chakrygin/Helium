using Helium.SqlServer.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests
{
    [TestClass]
    public sealed class TestAssembly : TestClassBase
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Master.DropDatabase(DatabaseName);
            Master.CreateDatabase(DatabaseName);
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            Master.DropDatabase(DatabaseName);
        }
    }
}
