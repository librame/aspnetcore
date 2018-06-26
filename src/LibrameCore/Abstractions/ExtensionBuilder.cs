#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Builder;
using System;

namespace LibrameCore.Abstractions
{
    /// <summary>
    /// 扩展构建器。
    /// </summary>
    public class ExtensionBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="ExtensionBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        public ExtensionBuilder(IApplicationBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// 应用构建器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationBuilder"/>。
        /// </value>
        public IApplicationBuilder Builder { get; }

        /// <summary>
        /// 服务提供程序。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IServiceProvider"/>。
        /// </value>
        public IServiceProvider Services => Builder.ApplicationServices;
    }
}
