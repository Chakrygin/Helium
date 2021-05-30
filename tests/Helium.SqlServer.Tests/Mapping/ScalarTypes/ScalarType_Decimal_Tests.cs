using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Decimal_Tests : TestClassBase
    {
        private const decimal Expected = 3.1415m;

        [TestMethod]
        public async Task Decimal_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(3.1415 as decimal(8, 4))")
                .ExecuteAsync<decimal>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Decimal_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as decimal(8, 4))")
                .ExecuteAsync<decimal>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableDecimal_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(3.1415 as decimal(8, 4))")
                .ExecuteAsync<decimal?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableDecimal_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as decimal(8, 4))")
                .ExecuteAsync<decimal?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
