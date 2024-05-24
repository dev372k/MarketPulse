using Application.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Helpers;
using System.Security.Claims;
using Application.Implementations;
using Presentation.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepo _userRepo;
        private StateHelper _stateHelper;
        private readonly INotyfService _notyf;

        public AccountController(IUserRepo userRepo,
            INotyfService notyf,
             StateHelper stateHelper)
        {
            _userRepo = userRepo;
            _notyf = notyf;
            _stateHelper = stateHelper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterDTO model)
        {
            try
            {
                _userRepo.Add(model);
                _notyf.Success("User registered successfully.");
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _notyf.Error(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO model)
        {
            var user = _userRepo.Get(model.Email);

            if (user == null)
            {
                _notyf.Error("User not found");
                return View();
            }
            else if (user.IsDeleted)
            {
                _notyf.Error("User is locked. Please contact Administrator");
                return View();
            }
            else if (!SecurityHelper.ValidateHash(model.Password, user.Password))
            {
                _notyf.Error("User credentials are wrong");
                return View();
            }
            else
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Email),
                    new Claim(ClaimTypes.Name, model.Email),
                    //new Claim(ClaimTypes.Role, ((enRole)user.Role).ToString()),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user))
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            return View(_stateHelper.User());
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateUserDTO request)
        {
            return Ok(new ResponseModel { Message = "Profile updated successfully." });
        }

        [HttpGet("Logout"), Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
