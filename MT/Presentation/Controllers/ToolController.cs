using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
