#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Filtration;
using LibrameCore.Filtration.SensitiveWord;
using LibrameCore.Filtration.StaticalHtml;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace LibrameStandard
{
    /// <summary>
    /// 过滤 Librame 构建器静态扩展。
    /// </summary>
    public static class FiltrationLibrameBuilderExtensions
    {

        /// <summary>
        /// 注册过滤模块。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器。</param>
        /// <param name="optionsAction">给定的过滤选项动作方法（可选）。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder AddFiltration(this ILibrameBuilder builder,
            Action<FiltrationOptions> optionsAction = null)
        {
            // AddOptions
            builder.AddOptions(FiltrationOptions.Key, optionsAction);

            // 敏感词
            builder.Services.TryAddSingleton<ISensitiveWordFilter, FileSensitiveWordFilter>();

            // 静态化
            builder.Services.TryAddSingleton<IHtmlBuilder, HtmlBuilder>();
            builder.Services.TryAddSingleton<IViewResultStaticizer, ViewResultStaticizer>();

            return builder;
        }

    }
}
