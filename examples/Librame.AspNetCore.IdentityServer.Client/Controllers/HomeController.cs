using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Client.Controllers
{
    using Extensions;
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
            await HttpContext.SignOutAsync().ConfigureAndWaitAsync(); //IdentityExtensionConfigurator.Defaults.COOKIE_AUTH_SCHEME
            //await HttpContext.SignOutAsync(IdentityExtensionConfigurator.Defaults.OIDC_AUTH_SCHEME);
        }

    }
}
