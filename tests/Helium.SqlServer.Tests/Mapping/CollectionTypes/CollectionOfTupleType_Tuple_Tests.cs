using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public sealed class CollectionOfTupleType_Tuple_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task ListOfTuple_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<Tuple<int, string>>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => Tuple.Create(x.Id, x.Name)));
        }

        [TestMethod]
        public async Task ListOfTuple_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<Tuple<int, string>>>();

            result.Print();
            result.Should().BeEmpty();
        }
    }
}
