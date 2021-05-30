using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Int32_Tests : TestClassBase
    {
        private const int Expected = 42;

        [TestMethod]
        public async Task Int32_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(42 as int)")
                .ExecuteAsync<int>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Int32_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as int)")
                .ExecuteAsync<int>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableInt32_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(42 as int)")
                .ExecuteAsync<int?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableInt32_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as int)")
                .ExecuteAsync<int?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
