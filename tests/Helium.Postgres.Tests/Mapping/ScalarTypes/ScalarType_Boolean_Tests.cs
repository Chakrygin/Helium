using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.Postgres.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Boolean_Tests : TestClassBase
    {
        [TestMethod]
        public async Task Boolean_NotNull_Test()
        {
            var result = await Database
                .Query("select 1::boolean")
                .ExecuteAsync<bool>();

            result.Print();
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task Boolean_Null_Test()
        {
            var result = await Database
                .Query("select null::boolean")
                .ExecuteAsync<bool>();

            result.Print();
            result.Should().BeFalse();
        }

        [TestMethod]
        public async Task NullableBoolean_NotNull_Test()
        {
            var result = await Database
                .Query("select 1::boolean")
                .ExecuteAsync<bool?>();

            result.Print();
            result.Should().BeTrue();
        }

        [TestMethod]
        public async Task NullableBoolean_Null_Test()
        {
            var result = await Database
                .Query("select null::boolean")
                .ExecuteAsync<bool?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
