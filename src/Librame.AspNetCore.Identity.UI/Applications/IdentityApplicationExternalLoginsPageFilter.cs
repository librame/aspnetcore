#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 身份应用程序外部登入集合页过滤器。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    internal class IdentityApplicationExternalLoginsPageFilter<TUser> : IAsyncPageFilter
        where TUser : class
    {
        /// <summary>
        /// 异步执行页处理程序。
        /// </summary>
        /// <param name="context">给定的 <see cref="PageHandlerExecutingContext"/>。</param>
        /// <param name="next">给定的 <see cref="PageHandlerExecutionDelegate"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var result = await next();
            if (result.Result is PageResult page)
            {
                var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<TUser>>();
                var schemes = await signInManager.GetExternalAuthenticationSchemesAsync();
                var hasExternalLogins = schemes.Any();

                page.ViewData["ManageNav.HasExternalLogins"] = hasExternalLogins;
            }
        }


        /// <summary>
        /// 异步选择页处理程序。
        /// </summary>
        /// <param name="context">给定的 <see cref="PageHandlerSelectedContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

    }
}
