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

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions;

    /// <summary>
    /// UI 页面集合身份构建器静态扩展。
    /// </summary>
    public static class UIPagesIdentityBuilderExtensions
    {
        /// <summary>
        /// 添加 UI 页面集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="applicationContextType">给定的应用程序上下文类型（需实现 <see cref="IApplicationContext"/> 接口，
        /// 推荐从 <see cref="IdentityApplicationContext{TUser}"/> 派生）。</param>
        /// <param name="postConfigureOptionsType">给定的身份应用程序配置选项类型（需实现 <see cref="IIdentityApplicationPostConfigureOptions"/> 接口，
        /// 推荐从 <see cref="IdentityApplicationPostConfigureOptions{TUser}"/> 派生）。</param>
        /// <param name="themepackName">给定的主题包名称。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddUIPages(this IIdentityBuilder builder, IMvcBuilder mvcBuilder,
            Type applicationContextType = null, Type postConfigureOptionsType = null, string themepackName = null)
        {
            // Configure Options
            if (postConfigureOptionsType.IsNull())
            {
                postConfigureOptionsType = typeof(IdentityApplicationPostConfigureOptions<>);
            }
            else
            {
                typeof(IIdentityApplicationPostConfigureOptions).IsAssignableFromTargetType(postConfigureOptionsType);
            }
            postConfigureOptionsType = postConfigureOptionsType.MakeGenericType(builder.CoreIdentityBuilder.UserType);
            builder.Services.PostConfigureApplicationOptions(postConfigureOptionsType);

            // Add UI
            if (applicationContextType.IsNull())
            {
                applicationContextType = typeof(IdentityApplicationContext<>).MakeGenericType(builder.CoreIdentityBuilder.UserType);
            }
            else
            {
                typeof(IApplicationContext).IsAssignableFromTargetType(applicationContextType);
            }
            builder.AddUI(applicationContextType, themepackName);

            // Add Assemblies Pages（需引用 Microsoft.AspNetCore.Mvc 程序集才能正常被路由解析）
            mvcBuilder.AddRazorRelatedParts(ThemepackHelper.GetInfoAssembly(themepackName),
                typeof(UIPagesIdentityBuilderExtensions).Assembly);

            return builder;
        }

    }
}
