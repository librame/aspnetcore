using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrameCore.WebPage.Pages
{
    using Extensions.Authentication;
    
    [LibrameClientLogout]
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}