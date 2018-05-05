using Dexterity.Login.Configuration;
using Dexterity.Login.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dexterity.Login.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore users;

        public AccountController(TestUserStore user = null)
        {
            // Inject in the test users from our configuration
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
            // Check the username and password fields have been completed
            if (ModelState.IsValid)
            {
                // Validate the username and password
                if (users.ValidateCredentials(model.Username, model.Password))
                {
                    var user = users.FindByUsername(model.Username);

                    AuthenticationProperties props = null;

                    // Sign the user in (i.e. generate the identity token)
                    await HttpContext.SignInAsync(user.SubjectId, user.Username, props);

                    // Send the user back to the client application
                    return Redirect(model.ReturnUrl);
                };
            }

            // Reload the same view if the username and password were not supplied or were incorrect
            //// TODO: Add in logic to tell the user when there is a problem (e.g. username and/or password is incorrect)
            return View(model);
        }
    }
}
