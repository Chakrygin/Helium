using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.ScalarTypes
{
    [TestClass]
    public sealed class ScalarType_ByteArray_Tests : TestClassBase
    {
        private static readonly byte[] Expected =
            {0x42, 0xAB, 0xCD, 0xEF};

        [TestMethod]
        public async Task ByteArray_NotNull_Test()
        {
            var result = await Database
                .Query("select 0x42ABCDEF")
                .ExecuteAsync<byte[]>();

            result.Print();
            result.Should().Equal(Expected);
        }

        [TestMethod]
        public async Task ByteArray_Null_Test()
        {
            var result = await Database
                .Query("select cast(null as varbinary)")
                .ExecuteAsync<byte[]>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
