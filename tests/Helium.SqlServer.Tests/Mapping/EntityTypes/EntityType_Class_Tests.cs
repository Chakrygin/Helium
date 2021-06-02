using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public sealed class EntityType_Class_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task Class_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClass>();

            var expected = new TestClass
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Class_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<TestClass>();

            result.Print();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task Class_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClass>();

            var expected = new TestClass
            {
                Id = Table.Row.Id,
                Name = null!,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Class_OnlySecondColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClass>();

            var expected = new TestClass
            {
                Id = default,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Class_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Date], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClass>();

            var expected = new TestClass
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Class_ShuffledColumns_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClass>();

            var expected = new TestClass
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public sealed class TestClass
        {
            public int Id { get; set; }

            public string Name { get; set; } = null!;
        }
    }
}
