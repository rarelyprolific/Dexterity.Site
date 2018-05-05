using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dexterity.Site.Controllers
{
    // We can use the following Authorize attribute to lock this route down to user with the "Administrator" role
    // [Authorize(Roles = ("Administrator"))]

    [Authorize]
    public class SecurePageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
