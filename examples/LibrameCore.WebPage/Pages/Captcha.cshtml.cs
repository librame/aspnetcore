using System;
using System.Threading.Tasks;
using LibrameStandard.Drawing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace LibrameCore.WebPage.Pages
{
    public class CaptchaModel : PageModel
    {
        public async Task OnGet()
        {
            var drawing = HttpContext.RequestServices.GetRequiredService<ICaptchaDrawing>();
            var buffer = await drawing.DrawBytes(DateTime.Now.ToString("mmss"));
            
            Response.Headers.Add("Pragma", "No-Cache");
            Response.ContentType = "image/Png";

            await Response.Body.WriteAsync(buffer, 0, buffer.Length);
            return;
        }
    }
}