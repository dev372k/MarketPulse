using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
