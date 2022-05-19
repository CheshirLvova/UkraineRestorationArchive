using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UkraineRestorationArchive.BLL;
using UkraineRestorationArchive.WEB.Models;

namespace UkraineRestorationArchive.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManager _accountManagement;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger, IAccountManager accountManagement)
        {

            _accountManagement = accountManagement;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel userModel)
        {
            if (_accountManagement.UserAlreadyExist(userModel.UserName))
            {
                _logger.LogInformation("User tried to register with existing username {username}", userModel.UserName);
                ModelState.AddModelError("UserName", "User already exist");
            }
            else if (_accountManagement.EmailAlreadyInUse(userModel.EmailAddres))
            {
                _logger.LogInformation("User tried to register with existing email {email}", userModel.EmailAddres);
                ModelState.AddModelError("EmailAddres", "Email addres has already been used");
            }
            else
            {
                _accountManagement.AddUser(userModel.UserName, userModel.EmailAddres, userModel.Password);
                _logger.LogInformation("New user registered {user}", userModel.UserName);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        private async Task Authenticate(string username, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "Authorization", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authorize(LoginModel user)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            try
            {
                bool verify = _accountManagement.verifyUser(user.Login, user.Password);
                if (verify)
                {
                    string role = _accountManagement.getRole(user.Login);
                    string username = _accountManagement.getUsername(user.Login);
                    await Authenticate(username, role);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogWarning("Attempt to login with invalid login or password {usedLogin}, {usedPassword}", user.Login, user.Password);
                    TempData["Error"] = "Error. Username or Password is invalid";
                    return View("Login");
                }

            }
            catch (System.Exception e)
            {
                _logger.LogError("Error occured \n {error}", e);
                return View("Error");
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string user = User.Identity.Name.ToString();

            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Console.WriteLine("here");
            return Redirect("/");
        }   
    }
}
