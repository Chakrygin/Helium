using System;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public sealed class EntityType_ClassWithBaseClass_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task ClassWithInheritedProperty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClassWithInheritedProperty>();

            var expected = new TestClassWithInheritedProperty
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public abstract class TestClassBase
        {
            public int Id { get; set; }
        }

        public sealed class TestClassWithInheritedProperty : TestClassBase
        {
            public string Name { get; set; } = null!;
        }

        [TestMethod]
        public async Task ClassWithHiddenProperty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClassWithHiddenProperty>();

            var expected = new TestClassWithHiddenProperty
            {
                Id = Table.Row.Id,
                Name = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
            result.As<TestClassBase>().Id.Should().Be(0);
        }

        public sealed class TestClassWithHiddenProperty : TestClassBase
        {
            public new int Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}
