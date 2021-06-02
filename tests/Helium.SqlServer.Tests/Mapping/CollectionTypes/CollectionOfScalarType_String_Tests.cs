using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public sealed class CollectionOfScalarType_String_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task ListOfString_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<string>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => x.Name));
        }

        [TestMethod]
        public async Task ListOfString_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<string>>();

            result.Print();
            result.Should().BeEmpty();
        }
    }
}
