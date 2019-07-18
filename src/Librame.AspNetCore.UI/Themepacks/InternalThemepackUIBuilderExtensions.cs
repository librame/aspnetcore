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
    /// 内部主题包 UI 构建器静态扩展。
    /// </summary>
    internal static class InternalThemepackUIBuilderExtensions
    {
        /// <summary>
        /// 添加主题包集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUIBuilder"/>。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddThemepacks(this IUIBuilder builder)
        {
            builder.Services.AddSingleton(builder.Themepack);

            return builder;
        }

    }
}
