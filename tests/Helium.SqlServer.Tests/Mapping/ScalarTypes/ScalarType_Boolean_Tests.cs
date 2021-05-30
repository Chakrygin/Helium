using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Boolean_Tests : TestClassBase
    {
        [TestMethod]
        public async Task Boolean_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(1 as bit)")
                .ExecuteAsync<bool>();

            result.Print();
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task Boolean_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as bit)")
                .ExecuteAsync<bool>();

            result.Print();
            result.Should().BeFalse();
        }

        [TestMethod]
        public async Task NullableBoolean_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(1 as bit)")
                .ExecuteAsync<bool?>();

            result.Print();
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task NullableBoolean_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as bit)")
                .ExecuteAsync<bool?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
