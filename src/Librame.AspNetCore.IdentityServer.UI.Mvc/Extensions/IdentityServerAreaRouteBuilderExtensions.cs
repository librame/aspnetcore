﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Routing
{
    /// <summary>
    /// 身份服务器区域路由构建器静态扩展。
    /// </summary>
    public static class IdentityServerAreaRouteBuilderExtensions
    {
        /// <summary>
        /// 映射身份服务器区域路由。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IRouteBuilder"/>。</param>
        /// <returns>返回 <see cref="IRouteBuilder"/>。</returns>
        public static IRouteBuilder MapIdentityServerAreaRoute(this IRouteBuilder builder)
        {
            return builder.MapAreaRoute(
                name: "IdentityServer",
                areaName: "IdentityServer",
                template: "IdentityServer/{controller}/{action}/{id?}"
            );
        }


        /// <summary>
        /// 映射身份服务器区域控制器路由。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IEndpointRouteBuilder"/>。</param>
        /// <returns>返回 <see cref="ControllerActionEndpointConventionBuilder"/>。</returns>
        public static ControllerActionEndpointConventionBuilder MapIdentityServerAreaControllerRoute(this IEndpointRouteBuilder builder)
        {
            return builder.MapAreaControllerRoute(
                name: "IdentityServer",
                areaName: "IdentityServer",
                pattern: "IdentityServer/{controller}/{action}/{id?}"
            );
        }

    }
}
