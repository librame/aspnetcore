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
    /// <see cref="IApplicationConfigurator"/> 静态扩展。
    /// </summary>
    public static class ApplicationConfiguratorExtensions
    {
        /// <summary>
        /// 使用 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <param name="optionsAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApplicationConfigurator"/>。</returns>
        public static IApplicationConfigurator UseLibrameCore(this IApplicationBuilder builder,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            var loader = new InternalApplicationConfigurator(builder);

            return loader.UseLocalization(optionsAction);
        }

    }
}
