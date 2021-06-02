using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.CollectionTypes
{
    [TestClass]
    public sealed class CollectionOfDynamicType_Tests : CollectionType_Base_Tests
    {
        [TestMethod]
        public async Task ListOfDynamic_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    order by [Id] asc")
                .ExecuteAsync<List<dynamic>>();

            result.Print();
            result.Should().BeEquivalentTo(
                Table.Rows.Select(x => new Dictionary<string, object>
                {
                    {"Id", x.Id},
                    {"Name", x.Name},
                }));
        }

        [TestMethod]
        public async Task ListOfDynamic_Empty_Test()
        {
            var result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<List<dynamic>>();

            result.Print();
            result.Should().BeEmpty();
        }
    }
}
