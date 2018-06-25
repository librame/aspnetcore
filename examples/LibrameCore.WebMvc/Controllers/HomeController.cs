using LibrameStandard.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LibrameCore.WebMvc.Controllers
{
    using Filtration;
    using Filtration.SensitiveWords;
    using Server.StaticPages;
    using WebMvc.Models;

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

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Captcha()
        {
            var drawing = HttpContext.RequestServices.GetRequiredService<ICaptchaDrawing>();
            var buffer = await drawing.DrawBytes(DateTime.Now.ToString("mmss"));

            return File(buffer, "image/png");
        }

        public IActionResult SensitiveWord()
        {
            var filter = HttpContext.RequestServices.GetService<ISensitiveWordFiltration>();

            return View(filter.Options);
        }
        [SensitiveWordActionFilter]
        [HttpPost]
        public IActionResult SensitiveWord(IFormCollection collection)
        {
            return Content(collection["words"]);
        }

        [StaticPageActionFilter]
        public IActionResult StaticPage(string id = "1")
        {
            ViewBag.Id = id;

            return View();
        }
    }
}
