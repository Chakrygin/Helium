using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Helium.SqlServer.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping
{
    [TestClass]
    public sealed class Other_ManyColumns_Tests : TestClassBase
    {
        public static SqlServerTableHelper<TestClass> Table { get; } = CreateTableHelper<TestClass>();

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Table.Create(
                "[Id] int primary key",
                "[IntValue0] int not null",
                "[IntValue1] int not null",
                "[IntValue2] int not null",
                "[IntValue3] int not null",
                "[IntValue4] int not null",
                "[IntValue5] int not null",
                "[IntValue6] int not null",
                "[IntValue7] int not null",
                "[IntValue8] int not null",
                "[IntValue9] int not null",
                "[StrValue0] nvarchar(100) not null",
                "[StrValue1] nvarchar(100) not null",
                "[StrValue2] nvarchar(100) not null",
                "[StrValue3] nvarchar(100) not null",
                "[StrValue4] nvarchar(100) not null",
                "[StrValue5] nvarchar(100) not null",
                "[StrValue6] nvarchar(100) not null",
                "[StrValue7] nvarchar(100) not null",
                "[StrValue8] nvarchar(100) not null",
                "[StrValue9] nvarchar(100) not null");

            Table.Fill(10, (id, faker) => new TestClass
            {
                Id = id,

                IntValue0 = faker.Random.Int(),
                IntValue1 = faker.Random.Int(),
                IntValue2 = faker.Random.Int(),
                IntValue3 = faker.Random.Int(),
                IntValue4 = faker.Random.Int(),
                IntValue5 = faker.Random.Int(),
                IntValue6 = faker.Random.Int(),
                IntValue7 = faker.Random.Int(),
                IntValue8 = faker.Random.Int(),
                IntValue9 = faker.Random.Int(),

                StrValue0 = faker.Lorem.Sentence(),
                StrValue1 = faker.Lorem.Sentence(),
                StrValue2 = faker.Lorem.Sentence(),
                StrValue3 = faker.Lorem.Sentence(),
                StrValue4 = faker.Lorem.Sentence(),
                StrValue5 = faker.Lorem.Sentence(),
                StrValue6 = faker.Lorem.Sentence(),
                StrValue7 = faker.Lorem.Sentence(),
                StrValue8 = faker.Lorem.Sentence(),
                StrValue9 = faker.Lorem.Sentence(),
            });
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Table.Drop();
        }

        [TestMethod]
        public async Task Class_Test()
        {
            var result = await Database
                .Query($@"
                    select top 1 *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestClass>();

            result.Print();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Struct_Test()
        {
            var result = await Database
                .Query($@"
                    select top 1 *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStruct>();

            result.Print();
            result.Should().NotBe(default(TestStruct));
        }

        [TestMethod]
        public async Task NullableStruct_Test()
        {
            var result = await Database
                .Query($@"
                    select top 1 *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<TestStruct?>();

            result.Print();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ListOfClass_Test()
        {
            var result = await Database
                .Query($@"
                    select *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<List<TestClass>>();

            result.Print();
            result.Should().NotBeNull();
            result.Should().HaveCount(Table.Rows.Count);
        }

        [TestMethod]
        public async Task ListOfStruct_Test()
        {
            var result = await Database
                .Query($@"
                    select *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<List<TestStruct>>();

            result.Print();
            result.Should().NotBeNull();
            result.Should().HaveCount(Table.Rows.Count);
        }

        [TestMethod]
        public async Task ListOfNullableStruct_Test()
        {
            var result = await Database
                .Query($@"
                    select *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<List<TestStruct?>>();

            result.Print();
            result.Should().NotBeNull();
            result.Should().HaveCount(Table.Rows.Count);
        }

        [TestMethod]
        public async Task DictionaryOfClass_Test()
        {
            var result = await Database
                .Query($@"
                    select *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<Dictionary<int, TestClass>>();

            result.Print();
            result.Should().NotBeNull();
            result.Should().HaveCount(Table.Rows.Count);
        }

        [TestMethod]
        public async Task DictionaryOfStruct_Test()
        {
            var result = await Database
                .Query($@"
                    select *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<Dictionary<int, TestStruct>>();

            result.Print();
            result.Should().NotBeNull();
            result.Should().HaveCount(Table.Rows.Count);
        }

        [TestMethod]
        public async Task DictionaryOfNullableStruct_Test()
        {
            var result = await Database
                .Query($@"
                    select *
                    from [dbo].[{Table.Name}];")
                .ExecuteAsync<Dictionary<int, TestStruct?>>();

            result.Print();
            result.Should().NotBeNull();
            result.Should().HaveCount(Table.Rows.Count);
        }

        public sealed class TestClass
        {
            public int Id { get; set; }

            public int IntValue0 { get; set; }
            public int IntValue1 { get; set; }
            public int IntValue2 { get; set; }
            public int IntValue3 { get; set; }
            public int IntValue4 { get; set; }
            public int IntValue5 { get; set; }
            public int IntValue6 { get; set; }
            public int IntValue7 { get; set; }
            public int IntValue8 { get; set; }
            public int IntValue9 { get; set; }

            public string StrValue0 { get; set; } = null!;
            public string StrValue1 { get; set; } = null!;
            public string StrValue2 { get; set; } = null!;
            public string StrValue3 { get; set; } = null!;
            public string StrValue4 { get; set; } = null!;
            public string StrValue5 { get; set; } = null!;
            public string StrValue6 { get; set; } = null!;
            public string StrValue7 { get; set; } = null!;
            public string StrValue8 { get; set; } = null!;
            public string StrValue9 { get; set; } = null!;
        }

        public struct TestStruct
        {
            public int Id { get; set; }

            public int IntValue0 { get; set; }
            public int IntValue1 { get; set; }
            public int IntValue2 { get; set; }
            public int IntValue3 { get; set; }
            public int IntValue4 { get; set; }
            public int IntValue5 { get; set; }
            public int IntValue6 { get; set; }
            public int IntValue7 { get; set; }
            public int IntValue8 { get; set; }
            public int IntValue9 { get; set; }

            public string StrValue0 { get; set; }
            public string StrValue1 { get; set; }
            public string StrValue2 { get; set; }
            public string StrValue3 { get; set; }
            public string StrValue4 { get; set; }
            public string StrValue5 { get; set; }
            public string StrValue6 { get; set; }
            public string StrValue7 { get; set; }
            public string StrValue8 { get; set; }
            public string StrValue9 { get; set; }
        }
    }
}
