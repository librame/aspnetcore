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

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份服务器 MVC  UI 构建器静态扩展。
    /// </summary>
    public static class IdentityServerMvcUiBuilderExtensions
    {
        /// <summary>
        /// 添加身份服务器控制器集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddIdentityServerControllers(this IUiBuilder builder, IMvcBuilder mvcBuilder)
            => builder.AddSiteControllers<IdentityServerApplicationSiteMvc, IdentityServerApplicationSiteConfiguration>(mvcBuilder, typeof(IdentityServerMvcUiBuilderExtensions).Assembly);

    }
}
