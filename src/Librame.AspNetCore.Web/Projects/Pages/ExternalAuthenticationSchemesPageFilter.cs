#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Web.Projects
{
    using AspNetCore.Web.Builders;
    using Extensions;

    /// <summary>
    /// 外部认证方案集合页过滤器。
    /// </summary>
    /// <typeparam name="TUser">给定的用户类型。</typeparam>
    public class ExternalAuthenticationSchemesPageFilter<TUser> : IAsyncPageFilter
        where TUser : class
    {
        private readonly WebBuilderOptions _options;


        /// <summary>
        /// 构造一个 <see cref="ExternalAuthenticationSchemesPageFilter{TUser}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="WebBuilderOptions"/>。</param>
        public ExternalAuthenticationSchemesPageFilter(WebBuilderOptions options)
        {
            _options = options.NotNull(nameof(options));
        }


        /// <summary>
        /// 异步执行页处理程序。
        /// </summary>
        /// <param name="context">给定的 <see cref="PageHandlerExecutingContext"/>。</param>
        /// <param name="next">给定的 <see cref="PageHandlerExecutionDelegate"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
            PageHandlerExecutionDelegate next)
        {
            if (next.IsNull())
                return;

            var executedContext = await next.Invoke().ConfigureAwait();
            if (executedContext.Result is PageResult pageResult)
            {
                var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<TUser>>();
                var schemes = await signInManager.GetExternalAuthenticationSchemesAsync().ConfigureAwait();

                pageResult.ViewData[_options.HasExternalAuthenticationSchemesKey] = schemes.IsNotEmpty();
            }
        }


        /// <summary>
        /// 异步选择页处理程序。
        /// </summary>
        /// <param name="context">给定的 <see cref="PageHandlerSelectedContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
            => Task.CompletedTask;
    }
}
