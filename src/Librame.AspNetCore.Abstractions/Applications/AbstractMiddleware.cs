#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Applications
{
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 抽象应用中间件。
    /// </summary>
    public abstract class AbstractApplicationMiddleware : IApplicationMiddleware
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationMiddleware"/> 中间件。
        /// </summary>
        /// <param name="next">给定的 <see cref="RequestDelegate"/>。</param>
        protected AbstractApplicationMiddleware(RequestDelegate next)
        {
            Next = next.NotNull(nameof(next));
        }


        /// <summary>
        /// 下一步方法委托。
        /// </summary>
        protected RequestDelegate Next { get; }


        /// <summary>
        /// 限定的请求路径。
        /// </summary>
        public virtual PathString RestrictRequestPath
            => PathString.Empty;

        /// <summary>
        /// 限定的请求方法列表（如：get、post...等）。
        /// </summary>
        public virtual IReadOnlyList<string> RestrictRequestMethods { get; }


        /// <summary>
        /// 调用中间件。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public virtual async Task Invoke(HttpContext context)
        {
            context.NotNull(nameof(context));

            // 如果未通过路径验证
            if (!RestrictRequestPath.HasValue
                || !context.Request.Path.StartsWithSegments(RestrictRequestPath, StringComparison.OrdinalIgnoreCase))
            {
                await Next.Invoke(context).ConfigureAndWaitAsync();
                return;
            }

            // 如果未通过方法验证
            if (RestrictRequestMethods.IsEmpty()
                || !RestrictRequestMethods.Any(method => context.Request.Method.Equals(method, StringComparison.OrdinalIgnoreCase)))
            {
                if (!context.Request.ContentLength.HasValue)
                {
                    var now = await context.RequestServices.GetRequiredService<IClockService>().GetNowOffsetAsync()
                        .ConfigureAndResultAsync();

                    var welcome = $"Welcome to Librame.AspNetCore.ApplicationMiddleware at {now}.";
                    await context.Response.WriteAsync(welcome, ExtensionSettings.Preference.DefaultEncoding).ConfigureAndWaitAsync();
                    return;
                }

                await Next.Invoke(context).ConfigureAndWaitAsync();
                return;
            }

            await InvokeCore(context).ConfigureAndWaitAsync();
        }

        /// <summary>
        /// 调用中间件核心。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected abstract Task InvokeCore(HttpContext context);
    }
}
