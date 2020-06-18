#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.Builders;
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份服务器 Web 构建器静态扩展。
    /// </summary>
    public static class IdentityServerWebBuilderExtensions
    {
        /// <summary>
        /// 添加身份服务器 Web 扩展。
        /// </summary>
        /// <param name="decorator">给定的 <see cref="IIdentityServerBuilderDecorator"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddIdentityServerWeb(this IIdentityServerBuilderDecorator decorator,
            Action<WebBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, WebBuilderDependency, IWebBuilder> builderFactory = null)
        {
            var builder = decorator.AddWeb(configureDependency, builderFactory);

            builder.AddService<IApplicationPrincipal, IdentityServerApplicationPrincipal>();

            return builder;
        }

    }
}
