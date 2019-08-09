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

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份页面集合用户界面构建器静态扩展。
    /// </summary>
    public static class IdentityPagesUserInterfaceBuilderExtensions
    {
        /// <summary>
        /// 添加身份页面集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUserInterfaceBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        public static IUserInterfaceBuilder AddIdentityPages(this IUserInterfaceBuilder builder, IMvcBuilder mvcBuilder)
        {
            // Add Assemblies Pages（需引用 Microsoft.AspNetCore.Mvc 程序集才能正常被路由解析）
            mvcBuilder.AddRazorRelatedParts(builder.Themepack.Assembly,
                typeof(IdentityPagesUserInterfaceBuilderExtensions).Assembly);

            return builder;
        }

    }
}
