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
using Microsoft.Extensions.Localization;

namespace Librame.Extensions.Core
{
    using AspNetCore;

    /// <summary>
    /// ASP.NET Core 本地化构建器静态扩展。
    /// </summary>
    public static class CoreLocalizationBuilderExtensions
    {
        /// <summary>
        /// 注册 ASP.NET Core 本地化集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <returns>返回 <see cref="IBuilder"/>。</returns>
        public static IBuilder AddCoreLocalizations(this IBuilder builder)
        {
            builder.Services.TryReplace<IStringLocalizerFactory, CoreResourceManagerStringLocalizerFactory>();

            return builder;
        }

    }
}
