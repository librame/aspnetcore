﻿#region License

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
    /// 本地化 UI 构建器静态扩展。
    /// </summary>
    public static class LocalizationUIBuilderExtensions
    {
        /// <summary>
        /// 添加本地化集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUIBuilder"/>。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddLocalizations(this IUIBuilder builder)
        {
            builder.Services.AddTransient(typeof(IExpressionHtmlLocalizer<>), typeof(ExpressionHtmlLocalizer<>));

            return builder;
        }

    }
}
