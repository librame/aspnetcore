#region License

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
    /// 身份区域路由构建器静态扩展。
    /// </summary>
    public static class IdentityAreaRouteBuilderExtensions
    {
        /// <summary>
        /// 映射身份区域路由。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IRouteBuilder"/>。</param>
        /// <returns>返回 <see cref="IRouteBuilder"/>。</returns>
        public static IRouteBuilder MapIdentityAreaRoute(this IRouteBuilder builder)
        {
            return builder.MapAreaRoute(
                name: "Identity",
                areaName: "Identity",
                template: "Identity/{controller}/{action}/{id?}",
                defaults: new { controller = "Manage", action = "Index" }
            );
        }


        /// <summary>
        /// 映射身份区域控制器路由。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IEndpointRouteBuilder"/>。</param>
        /// <returns>返回 <see cref="ControllerActionEndpointConventionBuilder"/>。</returns>
        public static ControllerActionEndpointConventionBuilder MapIdentityAreaControllerRoute(this IEndpointRouteBuilder builder)
        {
            return builder.MapAreaControllerRoute(
                name: "Identity",
                areaName: "Identity",
                pattern: "Identity/{controller}/{action}/{id?}",
                defaults: new { controller = "Manage", action = "Index" }
            );
        }

    }
}
