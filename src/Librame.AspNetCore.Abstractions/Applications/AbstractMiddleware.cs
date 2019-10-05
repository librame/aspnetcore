#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// 抽象应用中间件。
    /// </summary>
    public abstract class AbstractApplicationMiddleware : IApplicationMiddleware
    {
        private readonly RequestDelegate _next;


        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationMiddleware"/> 中间件。
        /// </summary>
        /// <param name="next">给定的 <see cref="RequestDelegate"/>。</param>
        public AbstractApplicationMiddleware(RequestDelegate next)
        {
            _next = next.NotNull(nameof(next));
        }


        /// <summary>
        /// 限定的请求路径。
        /// </summary>
        public virtual PathString RestrictRequestPath
            => PathString.Empty;

        /// <summary>
        /// 限定的请求方法。
        /// </summary>
        public virtual string RestrictRequestMethod
            => string.Empty;


        /// <summary>
        /// 调用中间件。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        public async Task Invoke(HttpContext context)
        {
            context.NotNull(nameof(context));

            // 如果未通过路径验证
            if (!RestrictRequestPath.Equals(PathString.Empty, StringComparison.OrdinalIgnoreCase)
                && !context.Request.Path.StartsWithSegments(RestrictRequestPath, StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(context).ConfigureAndWaitAsync();
                return;
            }

            // 如果未通过方法验证
            if (RestrictRequestMethod.IsNotEmpty()
                && !RestrictRequestMethod.Equals(context.Request.Method, StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(context).ConfigureAndWaitAsync();
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
