using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_SByte_Tests : TestClassBase
    {
        private const sbyte Expected = 42;

        [TestMethod]
        public async Task SByte_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<sbyte>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task SByte_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<sbyte>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableSByte_NotNull_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<sbyte?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableSByte_Null_Test()
        {
            var result = await Database
                .Query("select null")
                .ExecuteAsync<sbyte?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
