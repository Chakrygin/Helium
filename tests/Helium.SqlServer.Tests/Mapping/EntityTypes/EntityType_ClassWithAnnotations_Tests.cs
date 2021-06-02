using System;
using System.Threading.Tasks;

using FluentAssertions;

using Helium.Annotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public sealed class EntityType_ClassWithAnnotations_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task Class_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<TestClass>();

            var expected = new TestClass
            {
                TestId = Table.Row.Id,
                TestName = Table.Row.Name,
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        public sealed class TestClass
        {
            [Column("Id")]
            public int TestId { get; set; }

            [Column("Name")]
            public string TestName { get; set; } = null!;
        }
    }
}
