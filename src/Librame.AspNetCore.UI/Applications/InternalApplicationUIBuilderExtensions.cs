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
    /// 内部应用程序 UI 构建器静态扩展。
    /// </summary>
    internal static class InternalApplicationUIBuilderExtensions
    {
        /// <summary>
        /// 添加应用程序集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUIBuilder"/>。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddApplications(this IUIBuilder builder)
        {
            builder.Services.AddSingleton<IApplicationNavigation, InternalApplicationNavigation>();
            builder.Services.AddSingleton(typeof(IApplicationContext), builder.ApplicationContextType);
            builder.Services.ConfigureOptions(builder.ApplicationPostConfigureOptionsType);

            return builder;
        }

    }
}
