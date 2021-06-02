using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Helium.SqlServer.Tests.Mapping.EntityTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helium.SqlServer.Tests.Mapping.DynamicTypes
{
    [TestClass]
    public sealed class DynamicType_Tests : EntityType_Base_Tests
    {
        [TestMethod]
        public async Task Dynamic_Exists_Test()
        {
            object result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]")
                .ExecuteAsync<dynamic>();

            var expected = new Dictionary<string, object>
            {
                {"Id", Table.Row.Id},
                {"Name", Table.Row.Name},
            };

            result.Print();
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task Dynamic_NotExists_Test()
        {
            object result = await Database
                .Query($@"
                    select [Id], [Name]
                    from [dbo].[{Table.Name}]
                    where [Id] < 0")
                .ExecuteAsync<dynamic>();

            result.Print();
            result.Should().BeNull();
        }
    }
}
