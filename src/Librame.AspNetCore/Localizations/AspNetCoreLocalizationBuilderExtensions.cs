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

namespace Librame.AspNetCore
{
    using Extensions.Core;

    /// <summary>
    /// ASP.NET Core 本地化构建器静态扩展。
    /// </summary>
    public static class AspNetCoreLocalizationBuilderExtensions
    {
        /// <summary>
        /// 注册 ASP.NET Core 本地化集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="ICoreBuilder"/>。</param>
        /// <returns>返回 <see cref="IBuilder"/>。</returns>
        public static ICoreBuilder AddAspNetCoreLocalizations(this ICoreBuilder builder)
        {
            builder.Services.TryReplace<IStringLocalizerFactory, AspNetCoreResourceManagerStringLocalizerFactory>();

            return builder;
        }

    }
}
