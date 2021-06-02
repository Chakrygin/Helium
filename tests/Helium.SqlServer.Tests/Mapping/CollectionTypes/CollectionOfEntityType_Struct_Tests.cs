using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public sealed class CollectionOfEntityType_Struct_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task ListOfStruct_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<TestStruct>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => new TestStruct
                {
                    Id = x.Id,
                    Name = x.Name
                }));
        }

        [TestMethod]
        public async Task ListOfStruct_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<TestStruct>>();

            result.Print();
            result.Should().BeEmpty();
        }

        [TestMethod]
        public async Task ListOfNullableStruct_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<TestStruct?>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => new TestStruct
                {
                    Id = x.Id,
                    Name = x.Name
                }));
        }

        [TestMethod]
        public async Task ListOfNullableStruct_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<TestStruct?>>();

            result.Print();
            result.Should().BeEmpty();
        }

        public struct TestStruct
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
