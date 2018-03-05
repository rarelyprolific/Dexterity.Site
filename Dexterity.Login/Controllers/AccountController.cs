using Dexterity.Login.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dexterity.Login.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                //// TODO: Implement login
            }

            return View(model);
        }

        //// TODO: Example local login sign in user:
        //// public async Task SignInUser(string username)
        //// {
        ////     var claims = new List<Claim> {
        ////         new Claim(ClaimTypes.NameIdentifier, username),
        ////         new Claim("name", username)
        ////     };
        ////     var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ////     var principal = new ClaimsPrincipal(identity);
        ////     await HttpContext.SignInAsync(principal);
        //// }
    }
}
