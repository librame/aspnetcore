using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibrameCore.WebPage.Pages
{
    using Authentication;

    [LibrameAuthorize(Roles = "Administrator")]
    public class AdminModel : PageModel
    {
        /// <summary>
        /// 当前用户。
        /// </summary>
        public LibrameIdentity Identity
            => User.AsLibrameIdentity(HttpContext.RequestServices);


        public void OnGet()
        {
        }

    }
}