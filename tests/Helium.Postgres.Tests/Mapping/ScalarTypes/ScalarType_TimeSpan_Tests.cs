using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.Postgres.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_TimeSpan_Tests : TestClassBase
    {
        private static readonly TimeSpan Expected =
            new TimeSpan(12, 34, 56);

        [TestMethod]
        public async Task TimeSpan_NotNull_Test()
        {
            var result = await Database
                .Query("select '12:34:56'::interval")
                .ExecuteAsync<TimeSpan>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task TimeSpan_Null_Test()
        {
            var result = await Database
                .Query("select null::interval")
                .ExecuteAsync<TimeSpan>();

            result.Print();
            result.Should().Be(TimeSpan.Zero);
        }

        [TestMethod]
        public async Task NullableTimeSpan_NotNull_Test()
        {
            var result = await Database
                .Query("select '12:34:56'::interval")
                .ExecuteAsync<TimeSpan?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableTimeSpan_Null_Test()
        {
            var result = await Database
                .Query("select null::interval")
                .ExecuteAsync<TimeSpan?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
