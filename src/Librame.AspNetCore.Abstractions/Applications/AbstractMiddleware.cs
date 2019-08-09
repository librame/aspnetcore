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
            // 如果未通过路径验证
            if (!RestrictRequestPath.Equals(PathString.Empty)
                && !context.Request.Path.StartsWithSegments(RestrictRequestPath))
            {
                await _next.Invoke(context);
                return;
            }

            // 如果未通过方法验证
            if (RestrictRequestMethod.IsNotNullOrEmpty()
                && !RestrictRequestMethod.Equals(context.Request.Method, StringComparison.OrdinalIgnoreCase))
            {
                await _next.Invoke(context);
                return;
            }

            await InvokeCore(context);
        }

        /// <summary>
        /// 调用中间件核心。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected abstract Task InvokeCore(HttpContext context);
    }
}
