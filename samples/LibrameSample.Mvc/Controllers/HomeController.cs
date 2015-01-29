using Microsoft.AspNet.Mvc;

namespace LibrameSample.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
