using System.Collections.Generic;
using System.Linq;

using Helium.SqlServer.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public abstract class CollectionType_Base_Tests : TestClassBase
    {
        public static SqlServerTableHelper<TestRow> Table { get; } = CreateTableHelper<TestRow>();

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassInitialize(TestContext context)
        {
            if (!Table.IsCreated)
            {
                Table.Create(
                    "[Id]   int           primary key",
                    "[Name] nvarchar(max) not null");

                var rows = new List<(int, string)>
                {
                    (1, "One"),
                    (2, "Two"),
                    (3, "Three"),
                };

                Table.Fill(
                    from row in rows
                    select new TestRow
                    {
                        Id = row.Item1,
                        Name = row.Item2,
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

        public sealed class TestRow
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}
