#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Extensions.Filtration;
using LibrameCore.Extensions.Filtration.SensitiveWords;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace LibrameStandard.Abstractions
{
    /// <summary>
    /// <see cref="IExtensionCollection"/> 过滤静态扩展。
    /// </summary>
    public static class FiltrationExtensionCollectionExtensions
    {

        /// <summary>
        /// 添加过滤扩展。
        /// </summary>
        /// <param name="extensions">给定的 <see cref="IExtensionCollection"/>。</param>
        /// <param name="configureOptions">给定的后置配置选项动作（可选）。</param>
        /// <returns>返回 <see cref="IExtensionCollection"/>。</returns>
        public static IExtensionCollection AddFiltrationExtension(this IExtensionCollection extensions,
            Action<FiltrationExtensionOptions> configureOptions = null)
        {
            extensions.ConfigureOptions(configureOptions);

            // 敏感词
            extensions.Services.TryAddSingleton<ISensitiveWordFiltration, FileSensitiveWordFiltration>();

            return extensions;
        }

    }
}
