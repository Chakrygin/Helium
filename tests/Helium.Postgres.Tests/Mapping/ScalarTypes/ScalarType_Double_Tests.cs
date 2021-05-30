using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.Postgres.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Double_Tests : TestClassBase
    {
        private const double Expected = 3.1415d;

        [TestMethod]
        public async Task Double_NotNull_Test()
        {
            var result = await Database
                .Query("select 3.1415::float")
                .ExecuteAsync<double>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Double_Null_Test()
        {
            var result = await Database
                .Query("select null::float")
                .ExecuteAsync<double>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableDouble_NotNull_Test()
        {
            var result = await Database
                .Query("select 3.1415::float")
                .ExecuteAsync<double?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableDouble_Null_Test()
        {
            var result = await Database
                .Query("select null::float")
                .ExecuteAsync<double?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
