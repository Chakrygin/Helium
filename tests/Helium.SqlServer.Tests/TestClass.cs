using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests
{
    [TestClass]
    public sealed class TestClass : TestClassBase
    {
        [TestMethod]
        public void TestMethod()
        {
            var result = Database
                .Query($"SELECT 42")
                .Execute();
        }
    }
}
