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
    public sealed class DictionaryOfEntityType_ClassWithConstructor_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task DictionaryOfClass_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<Dictionary<int, TestClassWithConstructor>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.ToDictionary(
                    x => x.Id,
                    x => new TestClassWithConstructor(x.Id, x.Name)));
        }

        [TestMethod]
        public async Task DictionaryOfClass_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<Dictionary<int, TestClassWithConstructor>>();

            result.Print();
            result.Should().BeEmpty();
        }

        public sealed class TestClassWithConstructor
        {
            public TestClassWithConstructor(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; }
            public string Name { get; }
        }
    }
}
