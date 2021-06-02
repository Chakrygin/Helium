using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public sealed class CollectionOfScalarType_Int32_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task ListOfInt32_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<int>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => x.Id));
        }

        [TestMethod]
        public async Task ListOfInt32_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<int>>();

            result.Print();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task ListOfNullableInt32_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<int?>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => x.Id));
        }

        [TestMethod]
        public async Task ListOfNullableInt32_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<int?>>();

            result.Print();
            result.Should().BeEmpty();
        }
    }
}
