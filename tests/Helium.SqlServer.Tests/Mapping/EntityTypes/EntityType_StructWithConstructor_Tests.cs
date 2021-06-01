using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public sealed class EntityType_StructWithConstructor_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task StructWithConstructor_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor>();

            var expected = new TestStructWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStructWithConstructor_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor?>();

            var expected = new TestStructWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task StructWithConstructor_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0;")
                .ExecuteAsync<TestStructWithConstructor>();

            var expected = default(TestStructWithConstructor);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStructWithConstructor_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0;")
                .ExecuteAsync<TestStructWithConstructor?>();

            result.Print();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task StructWithConstructor_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor>();

            var expected = new TestStructWithConstructor(Table.Row.Id, default!);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStructWithConstructor_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor?>();

            var expected = new TestStructWithConstructor(Table.Row.Id, default!);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task StructWithConstructor_OnlySecondColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor>();

            var expected = new TestStructWithConstructor(default, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableStructWithConstructor_OnlySecondColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor?>();

            var expected = new TestStructWithConstructor(default, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task StructWithConstructor_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Date], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor>();

            var expected = new TestStructWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NullableStructWithConstructor_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Date], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor?>();

            var expected = new TestStructWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task StructWithConstructor_ShuffledColumns_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor>();

            var expected = new TestStructWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NullableStructWithConstructor_ShuffledColumns_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithConstructor?>();

            var expected = new TestStructWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public readonly struct TestStructWithConstructor
        {
            public TestStructWithConstructor(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; }

            public string Name { get; }
        }

        [TestMethod]
        public async Task StructWithOnlyFirstColumnConstructor_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithOnlyFirstColumnConstructor>();

            var expected = new TestStructWithOnlyFirstColumnConstructor(Table.Row.Id)
            {
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NullableStructWithOnlyFirstColumnConstructor_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithOnlyFirstColumnConstructor?>();

            var expected = new TestStructWithOnlyFirstColumnConstructor(Table.Row.Id)
            {
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public struct TestStructWithOnlyFirstColumnConstructor
        {
            public TestStructWithOnlyFirstColumnConstructor(int id)
            {
                Id = id;
                Name = default!;
            }

            public int Id { get; }
            public string Name { get; init; }
        }

        [TestMethod]
        public async Task StructWithOnlySecondColumnConstructor_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithOnlySecondColumnConstructor>();

            var expected = new TestStructWithOnlySecondColumnConstructor(Table.Row.Name)
            {
                Id = Table.Row.Id,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NullableStructWithOnlySecondColumnConstructor_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStructWithOnlySecondColumnConstructor?>();

            var expected = new TestStructWithOnlySecondColumnConstructor(Table.Row.Name)
            {
                Id = Table.Row.Id,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public struct TestStructWithOnlySecondColumnConstructor
        {
            public TestStructWithOnlySecondColumnConstructor(string name)
            {
                Id = default;
                Name = name;
            }

            public int Id { get; init; }
            public string Name { get; }
        }
    }
}
