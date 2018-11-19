using Microsoft.AspNetCore.Mvc;

namespace Librame.AspNetCore.Identity.Pages.Controllers
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