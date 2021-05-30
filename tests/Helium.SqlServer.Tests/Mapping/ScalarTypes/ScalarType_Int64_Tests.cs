using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Int64_Tests : TestClassBase
    {
        private const long Expected = 42;

        [TestMethod]
        public async Task Int64_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(42 as bigint)")
                .ExecuteAsync<long>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Int64_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as bigint)")
                .ExecuteAsync<long>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableInt64_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(42 as bigint)")
                .ExecuteAsync<long?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableInt64_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as bigint)")
                .ExecuteAsync<long?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
