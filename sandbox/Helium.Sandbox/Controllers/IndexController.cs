using Microsoft.AspNetCore.Mvc;

namespace Helium.Sandbox.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public sealed class IndexController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index() => RedirectToAction("GetItems", "Items");
    }
}
