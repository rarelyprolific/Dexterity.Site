using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dexterity.FrameworkSite.Controllers
{
    [Authorize]
    public class SecurePageController : Controller
    {
        // GET: SecurePage
        public ActionResult Index()
        {
            var user = User;

            return View();
        }
    }
}