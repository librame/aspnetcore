using LibrameStandard.Abstractions;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace LibrameCore.IdentityClient.Controllers
{
    using Models;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(IdentityExtensionConfigurator.Defaults.COOKIE_AUTH_SCHEME);
            await HttpContext.SignOutAsync(IdentityExtensionConfigurator.Defaults.OIDC_AUTH_SCHEME);
        }

    }
}
