using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public sealed class EntityType_TypeWithPrivateSetters_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task ClassWithPrivateSetters_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClassWithPrivateSetters>();

            var expected = TestClassWithPrivateSetters.Create(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public sealed class TestClassWithPrivateSetters
        {
            public static TestClassWithPrivateSetters Create(int id, string name)
            {
                var result = new TestClassWithPrivateSetters
                {
                    Id = id,
                    Name = name
                };

                return result;
            }

            public int Id { get; private set; }

            public string Name { get; private set; } = null!;
        }

        [TestMethod]
        public async Task StructWithPrivateSetters_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStructClassWithPrivateSetters>();

            var expected = TestStructClassWithPrivateSetters.Create(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task NullableStructWithPrivateSetters_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestStructClassWithPrivateSetters?>();

            var expected = TestStructClassWithPrivateSetters.Create(Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public struct TestStructClassWithPrivateSetters
        {
            public static TestStructClassWithPrivateSetters Create(int id, string name)
            {
                var result = new TestStructClassWithPrivateSetters
                {
                    Id = id,
                    Name = name
                };

                return result;
            }

            public int Id { get; private set; }

            public string Name { get; private set; }
        }
    }
}
