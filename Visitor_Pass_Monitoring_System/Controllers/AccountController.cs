using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Visitor_Pass_Monitoring_System.Models;
using Visitor_Pass_Monitoring_System.Repositories;


namespace Visitor_Pass_Monitoring_System.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository _userRepo = new UserRepository();



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.AddUser(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User loginModel)
        {

            if (loginModel.Username == null || loginModel.Password == null)
            {
                ModelState.AddModelError("", "Please enter username and password.");
                return View(loginModel);
            }

            User foundUser = _userRepo.GetUserByCredentials(loginModel.Username, loginModel.Password);

            if (foundUser != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, foundUser.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Visitor");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(loginModel);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}