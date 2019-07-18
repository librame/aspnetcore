using Microsoft.AspNetCore.Mvc;

namespace Librame.AspNetCore.Identity.UI.Pages.Examples.Controllers
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