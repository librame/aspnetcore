#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Extensions.Server;
using LibrameCore.Extensions.Server.SensitiveWords;
using LibrameCore.Extensions.Server.StaticPages;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace LibrameStandard.Abstractions
{
    /// <summary>
    /// <see cref="IExtensionCollection"/> 服务器静态扩展。
    /// </summary>
    public static class ServerExtensionCollectionExtensions
    {

        /// <summary>
        /// 添加服务器扩展。
        /// </summary>
        /// <param name="extensions">给定的 <see cref="IExtensionCollection"/>。</param>
        /// <param name="configureOptions">给定的后置配置选项动作（可选）。</param>
        /// <returns>返回 <see cref="IExtensionCollection"/>。</returns>
        public static IExtensionCollection AddServerExtension(this IExtensionCollection extensions,
            Action<ServerExtensionOptions> configureOptions = null)
        {
            extensions.ConfigureOptions(configureOptions);

            // 敏感词
            extensions.Services.TryAddSingleton<ISensitiveWordServer, FileSensitiveWordServer>();

            // 静态页
            extensions.Services.TryAddSingleton<IStaticPageReader, StaticPageReader>();
            extensions.Services.TryAddSingleton<IStaticPageWriter, StaticPageWriter>();
            extensions.Services.TryAddSingleton<IStaticPageServer, StaticPageServer>();

            return extensions;
        }

    }
}
