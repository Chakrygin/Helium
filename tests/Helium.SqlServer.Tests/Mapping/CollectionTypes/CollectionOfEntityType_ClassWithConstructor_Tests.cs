using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public sealed class CollectionOfEntityType_ClassWithConstructor_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task ListOfClassWithConstructor_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<TestClassWithConstructor>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => new TestClassWithConstructor(x.Id, x.Name)));
        }

        [TestMethod]
        public async Task ListOfClassWithConstructor_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<TestClassWithConstructor>>();

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
