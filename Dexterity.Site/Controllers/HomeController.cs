using Microsoft.AspNetCore.Mvc;

namespace Dexterity.Site.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
