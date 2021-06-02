using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public sealed class EntityType_Struct_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task Struct_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStruct_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct?>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task Struct_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<TestStruct>();

            var expected = default(TestStruct);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStruct_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<TestStruct?>();

            result.Print();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task Struct_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = default!,
            };

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStruct_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct?>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = default!,
            };

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task Struct_OnlySecondColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct>();

            var expected = new TestStruct
            {
                Id = default,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStruct_OnlySecondColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct?>();

            var expected = new TestStruct
            {
                Id = default,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task Struct_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Date], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NullableStruct_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Date], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct?>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Struct_ShuffledColumns_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NullableStruct_ShuffledColumns_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStruct?>();

            var expected = new TestStruct
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public struct TestStruct
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
