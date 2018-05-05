using Dexterity.Login.Configuration;
using Dexterity.Login.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
//using IdentityModel;
//using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dexterity.Login.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore users;
        private readonly IIdentityServerInteractionService interaction;

        public AccountController(
            IIdentityServerInteractionService interaction,
            TestUserStore user = null)
        {
            this.interaction = interaction;
            this.users = users ?? new TestUserStore(TestUserConfiguration.Get());
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                if (users.ValidateCredentials(model.Username, model.Password))
                {
                    var user = users.FindByUsername(model.Username);

                    AuthenticationProperties props = null;

                    await HttpContext.SignInAsync(user.SubjectId, user.Username, props);
                    //Redirect("https://www.google.co.uk");

                    return Redirect(model.ReturnUrl);
                };

                //var discoveryClient = new DiscoveryClient("http://localhost:64831");
                //var doc = await discoveryClient.GetAsync();

                /// TODO: Successfully getting an access token here but can't set an Authorization header yet.

                //var secret = "secret".ToSha256();
                //var client = new TokenClient(doc.TokenEndpoint, "Dexterity.Login", "secret");
                //var response = await client.RequestResourceOwnerPasswordAsync("zikes", "password");

                //var client = new TokenClient(doc.TokenEndpoint, "Dexterity.Login", "secret");
                //var response = await client.RequestClientCredentialsAsync();

                //var token = response.AccessToken;

                //HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");

                //Request.HttpContext.Response.Headers.Add("Authorization", $"Bearer {token}");
                //await HttpContext.SignInAsync(new ClaimsPrincipal());
                //// TODO: Implement login
                //Response.Cookies.Append("Authorization", $"Bearer {token}");
                //Response.Headers.Append(new KeyValuePair<string, StringValues>("Authorization", $"Bearer {token}"));
                //return Redirect("http://localhost:64826/SecurePage");
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
