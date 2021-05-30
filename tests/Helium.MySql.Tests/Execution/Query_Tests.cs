using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests.Execution
{
    [TestClass]
    public sealed class Query_Tests : TestClassBase
    {
        [TestMethod]
        public void Query_ExecuteNonQuery_Test()
        {
            var result = Database
                .Query("select 42")
                .Execute();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public async Task Query_ExecuteNonQueryAsync_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public void Query_Execute_Test()
        {
            var result = Database
                .Query("select 42")
                .Execute<int>();

            result.Print();
            result.Should().Be(42);
        }

        [TestMethod]
        public async Task Query_ExecuteAsync_Test()
        {
            var result = await Database
                .Query("select 42")
                .ExecuteAsync<int>();

            result.Print();
            result.Should().Be(42);
        }
    }
}
