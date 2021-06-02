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
    public sealed class DictionaryOfEntityType_Class_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task DictionaryOfClass_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<Dictionary<int, TestClass>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.ToDictionary(
                    x => x.Id,
                    x => new TestClass
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }));
        }

        [TestMethod]
        public async Task DictionaryOfClass_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<Dictionary<int, TestClass>>();

            result.Print();
            result.Should().BeEmpty();
        }

        public sealed class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}
