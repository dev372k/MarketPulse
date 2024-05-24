using Application.Implementations;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class CustomerController : Controller
    {
        private IUserRepo _userRepo;
        private StateHelper _stateHelper;
        private readonly INotyfService _notyf;

        public CustomerController(IUserRepo userRepo,
            INotyfService notyf,
             StateHelper stateHelper)
        {
            _userRepo = userRepo;
            _notyf = notyf;
            _stateHelper = stateHelper;
        }

        [HttpGet("Customers"), Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
