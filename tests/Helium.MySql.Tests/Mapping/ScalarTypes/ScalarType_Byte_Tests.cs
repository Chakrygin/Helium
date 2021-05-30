using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Byte_Tests : TestClassBase
    {
        private const byte Expected = 42;

        [TestMethod]
        public async Task Byte_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<byte>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Byte_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<byte>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableByte_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<byte?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableByte_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<byte?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
