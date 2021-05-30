using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_UInt64_Tests : TestClassBase
    {
        private const ulong Expected = 42;

        [TestMethod]
        public async Task UInt64_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<ulong>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task UInt64_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<ulong>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableUInt64_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<ulong?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableUInt64_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<ulong?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
