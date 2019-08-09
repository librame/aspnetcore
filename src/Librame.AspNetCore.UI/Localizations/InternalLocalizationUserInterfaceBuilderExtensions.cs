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
    /// 内部本地化用户界面构建器静态扩展。
    /// </summary>
    internal static class InternalLocalizationUserInterfaceBuilderExtensions
    {
        /// <summary>
        /// 添加本地化集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUserInterfaceBuilder"/>。</param>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        public static IUserInterfaceBuilder AddLocalizations(this IUserInterfaceBuilder builder)
        {
            builder.Services.AddTransient(typeof(IExpressionHtmlLocalizer<>), typeof(ExpressionHtmlLocalizer<>));

            return builder;
        }

    }
}
