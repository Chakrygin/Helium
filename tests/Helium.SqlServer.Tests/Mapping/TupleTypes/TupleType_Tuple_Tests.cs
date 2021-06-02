using System;
using System.Threading.Tasks;

using FluentAssertions;

using Helium.SqlServer.Tests.Mapping.EntityTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.TupleTypes
{
    [TestClass]
    public sealed class TupleType_Tuple_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task Tuple_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<Tuple<int, string>>();

            var expected = Tuple.Create(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Tuple_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<Tuple<int, string>>();

            result.Print();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task Tuple_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<Tuple<int, string>>();

            var expected = Tuple.Create(Table.Row.Id, default(string));

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Tuple_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name], [Date]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<Tuple<int, string>>();

            var expected = Tuple.Create(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }
    }
}
