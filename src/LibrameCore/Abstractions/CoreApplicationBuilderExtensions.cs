#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Abstractions;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/> 核心静态扩展。
    /// </summary>
    public static class CoreApplicationBuilderExtensions
    {

        /// <summary>
        /// 使用 LibrameCore 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <param name="configureBuilder">给定的扩展构建器配置方法。</param>
        /// <returns>返回 <see cref="IApplicationBuilder"/>。</returns>
        public static IApplicationBuilder UseLibrameCore(this IApplicationBuilder builder,
            Action<IExtensionBuilder> configureBuilder)
        {
            try
            {
                var extension = new ExtensionBuilder(builder);
                configureBuilder?.Invoke(extension);

                return extension.Builder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
