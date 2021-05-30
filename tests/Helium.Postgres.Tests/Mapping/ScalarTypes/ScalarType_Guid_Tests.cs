using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.Postgres.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Guid_Tests : TestClassBase
    {
        private static readonly Guid Expected =
            new Guid("E8AD4145-C596-4D62-9843-C1A84BB4DB5B");

        [TestMethod]
        public async Task Guid_NotNull_Test()
        {
            var result = await Database
                .Query("select 'E8AD4145-C596-4D62-9843-C1A84BB4DB5B'::uuid")
                .ExecuteAsync<Guid>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Guid_Null_Test()
        {
            var result = await Database
                .Query("select null::uuid")
                .ExecuteAsync<Guid>();

            result.Print();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task NullableGuid_NotNull_Test()
        {
            var result = await Database
                .Query("select 'E8AD4145-C596-4D62-9843-C1A84BB4DB5B'::uuid")
                .ExecuteAsync<Guid?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableGuid_Null_Test()
        {
            var result = await Database
                .Query("select null::uuid")
                .ExecuteAsync<Guid?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
