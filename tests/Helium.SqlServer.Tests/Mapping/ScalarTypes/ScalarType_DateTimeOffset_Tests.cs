using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_DateTimeOffset_Tests : TestClassBase
    {
        private static readonly DateTimeOffset Expected =
            new DateTimeOffset(1987, 12, 03, 12, 34, 56, offset: new TimeSpan(03, 00, 00));

        [TestMethod]
        public async Task DateTimeOffset_NotNull_Test()
        {
            var result = await Database
                .Query("select cast('1987-12-03 12:34:56 +03:00' as datetimeoffset)")
                .ExecuteAsync<DateTimeOffset>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task DateTimeOffset_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as datetimeoffset)")
                .ExecuteAsync<DateTimeOffset>();

            result.Print();
            result.Should().Be(DateTimeOffset.MinValue);
        }

        [TestMethod]
        public async Task NullableDateTimeOffset_NotNull_Test()
        {
            var result = await Database
                .Query("select cast('1987-12-03 12:34:56 +03:00' as datetimeoffset)")
                .ExecuteAsync<DateTimeOffset?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableDateTimeOffset_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as datetimeoffset)")
                .ExecuteAsync<DateTimeOffset?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
