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

namespace Librame.AspNetCore.Web.Builders
{
    using AspNetCore.IdentityServer.Web.Projects;
    using AspNetCore.Web.Projects;

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
            builder.AddGenericControllers();

            builder.AddProjectController<IdentityServerProjectConfiguration, IdentityServerProjectNavigation>(mvcBuilder,
                typeof(IdentityServerWebBuilderWithControllerExtensions).Assembly);

            return builder;
        }

    }
}
