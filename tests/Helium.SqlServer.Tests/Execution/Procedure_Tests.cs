using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Execution
{
    [TestClass]
    public sealed class Procedure_Tests : TestClassBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Database
                .Query(@"
                    create procedure [dbo].[TestProcedure]
                    as
                    begin
                        select 42;
                    end")
                .Execute();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            Database
                .Query("drop procedure [dbo].[TestProcedure]")
                .Execute();
        }

        [TestMethod]
        public void Procedure_ExecuteNonQuery_Test()
        {
            var result = Database
                .Procedure("[dbo].[TestProcedure]")
                .Execute();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public async Task Procedure_ExecuteNonQueryAsync_Test()
        {
            var result = await Database
                .Procedure("[dbo].[TestProcedure]")
                .ExecuteAsync();

            result.Print();
            result.Should().Be(-1);
        }

        [TestMethod]
        public void Procedure_Execute_Test()
        {
            var result = Database
                .Procedure("[dbo].[TestProcedure]")
                .Execute<int>();

            result.Print();
            result.Should().Be(42);
        }

        [TestMethod]
        public async Task Procedure_ExecuteAsync_Test()
        {
            var result = await Database
                .Procedure("[dbo].[TestProcedure]")
                .ExecuteAsync<int>();

            result.Print();
            result.Should().Be(42);
        }
    }
}
