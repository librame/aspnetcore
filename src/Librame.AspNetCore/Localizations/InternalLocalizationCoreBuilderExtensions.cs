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
    /// 内部本地化核心构建器静态扩展。
    /// </summary>
    internal static class InternalLocalizationCoreBuilderExtensions
    {
        /// <summary>
        /// 添加本地化集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="ICoreBuilder"/>。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLocalizations(this ICoreBuilder builder)
        {
            builder.Services.TryReplace<IStringLocalizerFactory, AspNetCoreResourceManagerStringLocalizerFactory>();

            return builder;
        }

    }
}
