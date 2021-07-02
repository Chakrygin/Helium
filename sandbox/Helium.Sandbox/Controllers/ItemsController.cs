using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

using Helium.Sandbox.DataAccess;

using Microsoft.AspNetCore.Mvc;

namespace Helium.Sandbox.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public sealed class ItemsController : ControllerBase
    {
        private readonly DbClient _client;

        public ItemsController(DbClient client)
        {
            _client = client;
        }

        [HttpGet("items")]
        public async Task<List<Item>> GetItems(CancellationToken cancellationToken)
        {
            var result = await _client
                .Query($"select * from item order by id asc")
                .ExecuteAsync<List<Item>>(cancellationToken);

            return result;
        }

        [HttpGet("items/{itemId:long:min(1)}")]
        public async Task<Item> GetItem(CancellationToken cancellationToken,
            [FromRoute] long itemId)
        {
            var result = await _client
                .Query($@"select * from item where id = {itemId}")
                .ExecuteAsync<Item>(cancellationToken);

            return result;
        }
    }
}
