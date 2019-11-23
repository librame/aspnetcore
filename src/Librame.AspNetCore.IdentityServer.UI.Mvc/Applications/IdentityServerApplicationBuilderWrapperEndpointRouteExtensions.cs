#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore;
using Librame.Extensions;
using Microsoft.AspNetCore.Routing;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 身份服务器 <see cref="IApplicationBuilderDecorator"/> 端点路由静态扩展。
    /// </summary>
    public static class IdentityServerApplicationBuilderWrapperEndpointRouteExtensions
    {
        /// <summary>
        /// 使用身份服务器端点路由。
        /// </summary>
        /// <param name="decorator">给定的 <see cref="IApplicationBuilderDecorator"/>。</param>
        /// <param name="configureRoutes">给定的 <see cref="IEndpointRouteBuilder"/> 配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApplicationBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "decorator")]
        public static IApplicationBuilderDecorator UseIdentityServerEndpointRoute(this IApplicationBuilderDecorator decorator,
            Action<IEndpointRouteBuilder> configureRoutes = null)
        {
            decorator.NotNull(nameof(decorator));

            decorator.Source.UseEndpoints(configureRoutes ?? (routes =>
            {
                routes.MapIdentityServerAreaControllerRoute();
                routes.MapDefaultControllerRoute();
            }));

            return decorator;
        }

    }
}
