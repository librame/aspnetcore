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
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    /// <summary>
    /// 认证服务提供程序静态扩展。
    /// </summary>
    public static class AuthenticationServiceProviderExtensions
    {

        /// <summary>
        /// 获取 HTTP 上下文。
        /// </summary>
        /// <param name="serviceProvider">给定的服务提供程序。</param>
        /// <returns>返回 HTTP 上下文。</returns>
        public static HttpContext GetHttpContext(this IServiceProvider serviceProvider)
        {
            var accessor = serviceProvider.GetService<IHttpContextAccessor>();

            return accessor.HttpContext;
        }

    }
}
