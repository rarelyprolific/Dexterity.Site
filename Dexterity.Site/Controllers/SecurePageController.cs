using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dexterity.Site.Controllers
{
    [Authorize]
    public class SecurePageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var user = User;

            return View();
        }
    }
}
