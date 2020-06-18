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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Builders
{
    using AspNetCore.Identity.Web.Projects;
    using AspNetCore.Web.Projects;

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
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public static IWebBuilder AddIdentityProjectController(this IWebBuilder builder, IMvcBuilder mvcBuilder)
        {
            builder.AddGenericControllers();

            builder.AddProjectController<IdentityProjectConfiguration, IdentityProjectNavigation>(mvcBuilder,
                typeof(IdentityWebBuilderWithControllerExtensions).Assembly);

            return builder;
        }

    }
}
