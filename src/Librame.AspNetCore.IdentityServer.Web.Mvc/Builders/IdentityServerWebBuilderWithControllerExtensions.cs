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

namespace Librame.AspNetCore.Web.Builders
{
    using AspNetCore.IdentityServer.Builders;
    using AspNetCore.IdentityServer.Web.Projects;
    using AspNetCore.Web.Projects;
    using Extensions.Core.Builders;

    /// <summary>
    /// 带控制器的身份服务器 Web 构建器静态扩展。
    /// </summary>
    public static class IdentityServerWebBuilderWithControllerExtensions
    {
        /// <summary>
        /// 添加身份服务器项目控制器。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddIdentityServerProjectController(this IWebBuilder builder, IMvcBuilder mvcBuilder)
        {
            if (!builder.ContainsParentBuilder<IIdentityServerBuilderDecorator>())
                throw new InvalidOperationException("The identityserver web requires register AddIdentityServer(...).");

            builder.AddGenericControllersWithUser();

            builder.AddProjectController<IdentityServerProjectConfiguration, IdentityServerProjectNavigation>(mvcBuilder,
                typeof(IdentityServerWebBuilderWithControllerExtensions).Assembly);

            return builder;
        }

    }
}
