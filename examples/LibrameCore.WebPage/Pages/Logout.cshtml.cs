using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrameCore.WebPage.Pages
{
    using Authentication;
    
    [LibrameClientLogout]
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}