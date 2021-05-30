using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_String_Tests : TestClassBase
    {
        private const string Expected = "Test";

        [TestMethod]
        public async Task String_NotNull_Test()
        {
            var result = await Database
                .Query("select 'Test'")
                .ExecuteAsync<string>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task String_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as nvarchar)")
                .ExecuteAsync<string>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
