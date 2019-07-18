#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;
using System;

namespace Librame.AspNetCore
{
    /// <summary>
    /// 内部本地化应用程序配置器静态扩展。
    /// </summary>
    internal static class InternalLocalizationApplicationConfiguratorExtensions
    {
        /// <summary>
        /// 使用本地化应用程序。
        /// </summary>
        /// <param name="configurator">给定的 <see cref="IApplicationConfigurator"/>。</param>
        /// <param name="optionsAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApplicationConfigurator"/>。</returns>
        public static IApplicationConfigurator UseLocalization(this IApplicationConfigurator configurator,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            configurator.Builder.UseRequestLocalization(optionsAction ?? (_ => { }));

            return configurator;
        }

    }
}
