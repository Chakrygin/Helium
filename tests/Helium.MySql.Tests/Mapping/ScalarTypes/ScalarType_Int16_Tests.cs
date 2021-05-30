using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MySqlConnector;

namespace Helium.MySql.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Int16_Tests : TestClassBase
    {
        private const short Expected = 42;

        [TestMethod]
        public async Task Int16_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<short>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Int16_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<short>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableInt16_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<short?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableInt16_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<short?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
