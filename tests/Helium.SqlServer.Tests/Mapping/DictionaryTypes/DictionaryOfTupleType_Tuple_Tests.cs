using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Helium.SqlServer.Tests.Mapping.CollectionTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.DictionaryTypes
{
    [TestClass]
    public sealed class DictionaryOfTupleType_Tuple_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task DictionaryOfTuple_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<Dictionary<int, Tuple<int, string>>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.ToDictionary(
                    x => x.Id,
                    x => Tuple.Create(x.Id, x.Name)));
        }

        [TestMethod]
        public async Task DictionaryOfTuple_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<Dictionary<int, Tuple<int, string>>>();

            result.Print();
            result.Should().BeEmpty();
        }
    }
}
