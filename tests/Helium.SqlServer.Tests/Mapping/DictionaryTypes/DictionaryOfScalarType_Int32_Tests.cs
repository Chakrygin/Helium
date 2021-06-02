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
    public sealed class DictionaryOfScalarType_Int32_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task DictionaryOfInt32_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<Dictionary<string, int>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.ToDictionary(
                    x => x.Name,
                    x => x.Id));
        }

        [TestMethod]
        public async Task DictionaryOfInt32_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<Dictionary<string, int>>();

            result.Print();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task DictionaryOfNullableInt32_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<Dictionary<string, int?>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.ToDictionary(
                    x => x.Name,
                    x => x.Id));
        }

        [TestMethod]
        public async Task DictionaryOfNullableInt32_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<Dictionary<string, int?>>();

            result.Print();
            result.Should().BeEmpty();
        }
    }
}
