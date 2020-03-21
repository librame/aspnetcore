#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.Builders
{
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
        /// <param name="dependencyAction">给定的选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddIdentityServerWeb(this IIdentityServerBuilderDecorator decorator,
            Action<WebBuilderDependency> dependencyAction = null,
            Func<IExtensionBuilder, WebBuilderDependency, IWebBuilder> builderFactory = null)
            => decorator.AddWeb(dependencyAction, builderFactory)
                .AddUser(decorator?.UserType);
    }
}
