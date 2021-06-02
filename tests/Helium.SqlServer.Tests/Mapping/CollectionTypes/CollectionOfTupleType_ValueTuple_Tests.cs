using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public sealed class CollectionOfTupleType_ValueTuple_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task ListOfValueTuple_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<(int, string)>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => (x.Id, x.Name)));
        }

        [TestMethod]
        public async Task ListOfValueTuple_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<(int, string)>>();

            result.Print();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task ListOfNullableValueTuple_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<(int, string)?>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => (x.Id, x.Name)));
        }

        [TestMethod]
        public async Task ListOfNullableValueTuple_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<(int, string)?>>();

            result.Print();
            result.Should().BeEmpty();
        }
    }
}
