using System;
using System.Threading.Tasks;

using FluentAssertions;

using Helium.SqlServer.Tests.Mapping.EntityTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.TupleTypes
{
    [TestClass]
    public sealed class TupleType_ValueTuple_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task ValueTuple_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<(int, string)>();

            var expected = (Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableValueTuple_Exists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<(int, string)?>();

            var expected = (Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task ValueTuple_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<(int, string)>();

            var expected = (default(int), default(string)!);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableValueTuple_NotExists_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<(int, string)?>();

            result.Print();
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task ValueTuple_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<(int, string)>();

            var expected = (Table.Row.Id, default(string)!);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableValueTuple_OnlyFirstColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<(int, string)?>();

            var expected = (Table.Row.Id, default(string));

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task ValueTuple_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name], [Date]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<(int, string)>();

            var expected = (Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }

        [TestMethod]
        public async Task NullableValueTuple_ExtraColumn_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name], [Date]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<(int, string)?>();

            var expected = (Table.Row.Id, Table.Row.Name);

            result.Print();
            result.Should().Be(expected);
        }
    }
}
