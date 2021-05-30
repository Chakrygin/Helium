using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_UInt16_Tests : TestClassBase
    {
        private const ushort Expected = 42;

        [TestMethod]
        public async Task UInt16_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<ushort>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task UInt16_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<ushort>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableUInt16_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<ushort?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableUInt16_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<ushort?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
