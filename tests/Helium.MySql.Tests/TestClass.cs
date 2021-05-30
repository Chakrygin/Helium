using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests
{
    [TestClass]
    public class TestClass : TestClassBase
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
