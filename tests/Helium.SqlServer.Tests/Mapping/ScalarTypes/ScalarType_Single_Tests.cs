using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Single_Tests : TestClassBase
    {
        private const float Expected = 3.1415f;

        [TestMethod]
        public async Task Single_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(3.1415 as real)")
                .ExecuteAsync<float>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Single_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as real)")
                .ExecuteAsync<float>();

            result.Print();
            result.Should().Be(0);
        }

        [TestMethod]
        public async Task NullableSingle_NotNull_Test()
        {
            var result = await Database
                .Query("select cast(3.1415 as real)")
                .ExecuteAsync<float?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableSingle_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as real)")
                .ExecuteAsync<float?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
