using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.MySql.Tests.Execution
{
    [TestClass]
    public sealed class Procedure_Tests : TestClassBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Database
                .Query(@"
                    create procedure TestProcedure()
                    begin
                        select 42;
                    end")
                .Execute();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Database
                .Query("drop procedure TestProcedure")
                .Execute();
        }

        [TestMethod]
        public void Procedure_ExecuteNonQuery_Test()
        {
            var result = Database
                .Procedure("TestProcedure")
                .Execute();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public async Task Procedure_ExecuteNonQueryAsync_Test()
        {
            var result = await Database
                .Procedure("TestProcedure")
                .ExecuteAsync();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public void Procedure_Execute_Test()
        {
            var result = Database
                .Procedure("TestProcedure")
                .Execute<int>();

            result.Print();
            result.Should().Be(42);
        }

        [TestMethod]
        public async Task Procedure_ExecuteAsync_Test()
        {
            var result = await Database
                .Procedure("TestProcedure")
                .ExecuteAsync<int>();

            result.Print();
            result.Should().Be(42);
        }
    }
}
