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

namespace Librame.AspNetCore.Identity.Builders
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.Builders;
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份 Web 构建器静态扩展。
    /// </summary>
    public static class IdentityWebBuilderExtensions
    {
        /// <summary>
        /// 添加身份 Web 扩展。
        /// </summary>
        /// <param name="decorator">给定的 <see cref="IIdentityBuilderDecorator"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddIdentityWeb(this IIdentityBuilderDecorator decorator,
            Action<IdentityWebBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, IdentityWebBuilderDependency, IWebBuilder> builderFactory = null)
        {
            var builder = decorator.AddWeb(configureDependency, builderFactory);

            builder.AddService<IApplicationPrincipal, IdentityApplicationPrincipal>();

            return builder;
        }

    }
}
