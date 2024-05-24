using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CampaignController : Controller
    {
        [HttpGet("Campaigns"), Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
