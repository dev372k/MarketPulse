using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
