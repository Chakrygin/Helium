using System;

using Bogus;

using Helium.SqlServer.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.EntityTypes
{
    [TestClass]
    public abstract class EntityType_Base_Tests : TestClassBase
    {
        protected static SqlServerTableHelper<TestRow> Table { get; } = CreateTableHelper<TestRow>();

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassInitialize(TestContext context)
        {
            if (!Table.IsCreated)
            {
                Table.Create(
                    "[Id]   int           primary key",
                    "[Name] nvarchar(max) not null",
                    "[Date] datetime      not null");

                Table.Fill(faker => new TestRow
                {
                    Id = faker.Random.Int(1),
                    Name = faker.Lorem.Sentence(),
                    Date = faker.Date.Future(),
                });
            }
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassCleanup()
        {
            if (!Table.IsDropped)
            {
                Table.Drop();
            }
        }

        protected sealed class TestRow
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public DateTime Date { get; set; }
        }
    }
}
