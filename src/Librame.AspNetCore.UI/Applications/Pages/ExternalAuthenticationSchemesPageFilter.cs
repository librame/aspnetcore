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
using System.Threading.Tasks;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 外部认证方案集合页过滤器。
    /// </summary>
    public class ExternalAuthenticationSchemesPageFilter : IAsyncPageFilter
    {
        private readonly IUiBuilder _builder;
        private readonly UiBuilderOptions _options;


        /// <summary>
        /// 构造一个 <see cref="ExternalAuthenticationSchemesPageFilter"/>。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="UiBuilderOptions"/>。</param>
        public ExternalAuthenticationSchemesPageFilter(IUiBuilder builder,
            UiBuilderOptions options)
        {
            _builder = builder.NotNull(nameof(builder));
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
            var executedContext = await next();
            if (executedContext.Result is PageResult pageResult)
            {
                dynamic signInManager = context.HttpContext.RequestServices
                    .GetService(typeof(SignInManager<>)
                    .MakeGenericType(_builder.UserType));

                var schemes = await signInManager.GetExternalAuthenticationSchemesAsync();

                pageResult.ViewData[_options.HasExternalAuthenticationSchemesKey]
                    = schemes.IsNotNullOrEmpty();
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
