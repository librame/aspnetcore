// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.Web
{
    using AspNetCore.IdentityServer.Web.Models;
    using Extensions;

    /// <summary>
    /// IdentityServer Web 静态扩展。
    /// </summary>
    public static class IdentityServerWebExtensions
    {
        /// <summary>
        /// Checks if the redirect URI is for a native client.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static bool IsNativeClient(this AuthorizationRequest context)
        {
            context.NotNull(nameof(context));

            return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
               && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
        }

        /// <summary>
        /// 加载视图页。
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        [SuppressMessage("Design", "CA1054:URI 参数不应为字符串", Justification = "<挂起>")]
        public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
        {
            controller.NotNull(nameof(controller));

            controller.HttpContext.Response.StatusCode = 200;
            controller.HttpContext.Response.Headers["Location"] = "";
            
            return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
        }

    }
}
