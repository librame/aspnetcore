#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 身份应用控制器静态扩展。
    /// </summary>
    static class IdentityApplicationControllerExtensions
    {
        /// <summary>
        /// 重定向到本地 URL 或身份路由。
        /// </summary>
        /// <param name="controller">给定的 <see cref="ApplicationController"/>。</param>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        public static IActionResult RedirectToLocalUrlOrIdentityIndex(this ApplicationController controller, string returnUrl)
        {
            if (controller.Url.IsLocalUrl(returnUrl))
            {
                return controller.Redirect(returnUrl);
            }
            else
            {
                return controller.Redirect(controller.Application.CurrentInterfaceInfo.Sitemap.Manage.ToRouteString(controller.Url));
            }
        }

    }
}
