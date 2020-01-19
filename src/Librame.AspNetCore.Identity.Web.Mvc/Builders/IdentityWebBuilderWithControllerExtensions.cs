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

namespace Librame.AspNetCore.Web.Builders
{
    using Identity.Web.Projects;
    using Projects;

    /// <summary>
    /// 带控制器的身份 Web 构建器静态扩展。
    /// </summary>
    public static class IdentityWebBuilderWithControllerExtensions
    {
        /// <summary>
        /// 添加身份项目控制器。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddIdentityProjectController(this IWebBuilder builder, IMvcBuilder mvcBuilder)
        {
            builder.AddGenericControllersWithUser();

            builder.AddProjectController<IdentityProjectConfiguration, IdentityProjectNavigation>(mvcBuilder,
                typeof(IdentityWebBuilderWithControllerExtensions).Assembly);

            return builder;
        }

    }
}
