using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_UInt32_Tests : TestClassBase
    {
        private const uint Expected = 42;

        [TestMethod]
        public async Task UInt32_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<uint>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task UInt32_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<uint>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableUInt32_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<uint?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableUInt32_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<uint?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
