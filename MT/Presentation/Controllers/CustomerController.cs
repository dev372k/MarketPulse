using Application.Implementations;
using AspNetCoreHero.ToastNotification.Abstractions;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
