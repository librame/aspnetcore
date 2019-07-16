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

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 主题 UI 构建器静态扩展。
    /// </summary>
    public static class ThemepackUIBuilderExtensions
    {
        /// <summary>
        /// 添加主题集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUIBuilder"/>。</param>
        /// <param name="info">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddThemepacks(this IUIBuilder builder, IThemepackInfo info)
        {
            builder.Services.AddSingleton(info);

            return builder;
        }

    }
}
