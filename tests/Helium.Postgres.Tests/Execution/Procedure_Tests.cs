using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.Postgres.Tests.Execution
{
    [TestClass]
    public sealed class Procedure_Tests : TestClassBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            // Npgsql calls functions instead of procedures...
            // See https://www.npgsql.org/doc/basic-usage.html#stored-functions-and-procedures

            Database
                .Query(@"
                    create function test_function()
                    returns int
                    language sql
                    as $$
                        select 42;
                    $$")
                .Execute();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Database
                .Query("drop function test_function")
                .Execute();
        }

        [TestMethod]
        public void Procedure_ExecuteNonQuery_Test()
        {
            var result = Database
                .Procedure("test_function")
                .Execute();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public async Task Procedure_ExecuteNonQueryAsync_Test()
        {
            var result = await Database
                .Procedure("test_function")
                .ExecuteAsync();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public void Procedure_Execute_Test()
        {
            var result = Database
                .Procedure("test_function")
                .Execute<int>();

            result.Print();
            result.Should().Be(42);
        }

        [TestMethod]
        public async Task Procedure_ExecuteAsync_Test()
        {
            var result = await Database
                .Procedure("test_function")
                .ExecuteAsync<int>();

            result.Print();
            result.Should().Be(42);
        }
    }
}
