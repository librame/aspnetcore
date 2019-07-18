#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份 UI 构建器静态扩展。
    /// </summary>
    public static class IdentityUIBuilderExtensions
    {
        /// <summary>
        /// 添加身份 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="themepack">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddIdentityUI(this IIdentityBuilder builder,
            IThemepackInfo themepack, Action<UIBuilderOptions> setupAction = null)
        {
            var applicationContextType = typeof(IdentityApplicationContext<>)
                .MakeGenericType(builder.IdentityCore.UserType);
            var applicationPostConfigureOptionsType = typeof(IdentityApplicationPostConfigureOptions<>)
                .MakeGenericType(builder.IdentityCore.UserType);

            return builder.AddUI(applicationContextType, applicationPostConfigureOptionsType,
                themepack, setupAction);
        }

    }
}
