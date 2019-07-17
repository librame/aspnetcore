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

namespace Librame.AspNetCore
{
    /// <summary>
    /// 本地化应用程序配置器静态扩展。
    /// </summary>
    public static class LocalizationApplicationConfiguratorExtensions
    {
        /// <summary>
        /// 使用本地化应用程序。
        /// </summary>
        /// <param name="configurator">给定的 <see cref="IApplicationConfigurator"/>。</param>
        /// <returns>返回 <see cref="IApplicationConfigurator"/>。</returns>
        public static IApplicationConfigurator UseLocalization(this IApplicationConfigurator configurator)
        {
            // var requestLocalizationOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();
            configurator.Builder.UseRequestLocalization(); //requestLocalizationOptions.Value

            return configurator;
        }

    }
}
