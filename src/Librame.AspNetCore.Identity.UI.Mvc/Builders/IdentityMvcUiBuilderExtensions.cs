#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 身份 MVC UI 构建器静态扩展。
    /// </summary>
    public static class IdentityMvcUiBuilderExtensions
    {
        /// <summary>
        /// 添加带视图集合的身份界面。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddIdentityInterfaceWithViews(this IUiBuilder builder, IMvcBuilder mvcBuilder)
        {
            builder.AddGenericControllersWithUser();

            builder.AddInterfaceWithViews<IdentityInterfaceConfigurationWithViews, IdentityInterfaceSitemapWithViews>(mvcBuilder,
                typeof(IdentityMvcUiBuilderExtensions).Assembly);

            return builder;
        }

    }
}
