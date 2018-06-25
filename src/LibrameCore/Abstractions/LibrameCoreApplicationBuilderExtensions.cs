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

namespace LibrameCore
{
    /// <summary>
    /// LibrameCore 应用构建器静态扩展。
    /// </summary>
    public static class LibrameCoreApplicationBuilderExtensions
    {

        /// <summary>
        /// 使用 LibrameCore 模块集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <param name="configureModules">给定的模块配置。</param>
        /// <returns>返回 <see cref="IApplicationBuilder"/>。</returns>
        public static IApplicationBuilder UseLibrameCore(this IApplicationBuilder builder,
            Action<ILibrameModuleBuilder> configureModules = null)
        {
            try
            {
                // Use Librame
                var moduleBuilder = new LibrameModuleBuilder(builder);
                configureModules?.Invoke(moduleBuilder);

                return builder;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
