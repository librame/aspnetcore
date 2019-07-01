#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// UI 构建器静态扩展。
    /// </summary>
    public static class UIBuilderExtensions
    {
        /// <summary>
        /// 添加 UI。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="applicationContextType">给定的应用程序上下文类型。</param>
        /// <param name="themepackName">给定的主题包名称。</param>
        /// <returns>返回 <see cref="IBuilder"/>。</returns>
        public static IBuilder AddUI(this IBuilder builder, Type applicationContextType, string themepackName)
        {
            // Reset MvcDataAnnotations
            builder.Services.TryReplace(typeof(IConfigureOptions<MvcOptions>), typeof(MvcDataAnnotationsMvcOptionsSetup),
                typeof(ResetMvcDataAnnotationsMvcOptionsSetup));
            builder.Services.TryReplace<IValidationAttributeAdapterProvider, ResetValidationAttributeAdapterProvider>();

            // Add ExpressionHtmlLocalizer
            builder.Services.AddTransient(typeof(IExpressionHtmlLocalizer<>), typeof(ExpressionHtmlLocalizer<>));

            // Add ApplicationNavigation
            builder.Services.AddSingleton<IApplicationNavigation, ApplicationNavigation>();

            // Add ApplicationContext
            builder.Services.AddSingleton(typeof(IApplicationContext), applicationContextType);

            // Add ThemepackInfo
            builder.Services.AddSingleton(ThemepackHelper.GetInfo(themepackName));

            return builder;
        }

    }
}
