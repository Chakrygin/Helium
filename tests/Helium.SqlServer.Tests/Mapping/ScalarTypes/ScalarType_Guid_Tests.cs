using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_Guid_Tests : TestClassBase
    {
        private static readonly Guid Expected =
            new Guid("FE7A4A1C-77B4-4708-BAAD-0DCA5041C319");

        [TestMethod]
        public async Task Guid_NotNull_Test()
        {
            var result = await Database
                .Query("select cast('FE7A4A1C-77B4-4708-BAAD-0DCA5041C319' as uniqueidentifier)")
                .ExecuteAsync<Guid>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task Guid_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as uniqueidentifier)")
                .ExecuteAsync<Guid>();

            result.Print();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task NullableGuid_NotNull_Test()
        {
            var result = await Database
                .Query("select cast('FE7A4A1C-77B4-4708-BAAD-0DCA5041C319' as uniqueidentifier)")
                .ExecuteAsync<Guid?>();

            result.Print();
            result.Should().Be(Expected);
        }

        [TestMethod]
        public async Task NullableGuid_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as uniqueidentifier)")
                .ExecuteAsync<Guid?>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
