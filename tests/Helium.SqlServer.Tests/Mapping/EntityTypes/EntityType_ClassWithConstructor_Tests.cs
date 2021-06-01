using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public sealed class EntityType_ClassWithConstructor_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task ClassWithConstructor_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClassWithConstructor>();

            var expected = new TestClassWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task ClassWithConstructor_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0;")
                .ExecuteAsync<TestClassWithConstructor>();

            result.Print();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task ClassWithConstructor_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClassWithConstructor>();

            var expected = new TestClassWithConstructor(Table.Row.Id, default!);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task ClassWithConstructor_OnlySecondColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClassWithConstructor>();

            var expected = new TestClassWithConstructor(default, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task ClassWithConstructor_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Date], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClassWithConstructor>();

            var expected = new TestClassWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task ClassWithConstructor_ShuffledColumns_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name], [Id]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClassWithConstructor>();

            var expected = new TestClassWithConstructor(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
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

        [TestMethod]
        public async Task ClassWithOnlyFirstColumnConstructor_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClassWithOnlyFirstColumnConstructor>();

            var expected = new TestClassWithOnlyFirstColumnConstructor(Table.Row.Id)
            {
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public sealed class TestClassWithOnlyFirstColumnConstructor
        {
            public TestClassWithOnlyFirstColumnConstructor(int id)
            {
                Id = id;
            }

            public int Id { get; }
            public string Name { get; init; } = null!;
        }

        [TestMethod]
        public async Task ClassWithOnlySecondColumnConstructor_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClassWithOnlySecondColumnConstructor>();

            var expected = new TestClassWithOnlySecondColumnConstructor(Table.Row.Name)
            {
                Id = Table.Row.Id,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public sealed class TestClassWithOnlySecondColumnConstructor
        {
            public TestClassWithOnlySecondColumnConstructor(string name)
            {
                Name = name;
            }

            public int Id { get; init; }
            public string Name { get; }
        }
    }
}
