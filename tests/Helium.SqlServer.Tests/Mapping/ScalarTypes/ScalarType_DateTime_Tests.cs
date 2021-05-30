using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_DateTime_Tests : TestClassBase
    {
        private static readonly DateTime Expected =
            new DateTime(1987, 12, 03, 12, 34, 56);

        [TestMethod]
        public async Task DateTime_NotNull_Test()
        {
            var result = await Database
                .Query("select cast('1987-12-03 12:34:56' as datetime)")
                .ExecuteAsync<DateTime>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task DateTime_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as datetime)")
                .ExecuteAsync<DateTime>();

            result.Print();
            result.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        public async Task NullableDateTime_NotNull_Test()
        {
            var result = await Database
                .Query("select cast('1987-12-03 12:34:56' as datetime)")
                .ExecuteAsync<DateTime?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableDateTime_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as datetime)")
                .ExecuteAsync<DateTime?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
