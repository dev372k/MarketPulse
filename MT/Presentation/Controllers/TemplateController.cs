using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit([FromQuery]int? campaignId)
        {
            return View();
        }
    }
}
