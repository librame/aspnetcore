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

namespace LibrameCore.Abstractions
{
    /// <summary>
    /// 扩展构建器接口。
    /// </summary>
    public interface IExtensionBuilder
    {
        /// <summary>
        /// 应用构建器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationBuilder"/>。
        /// </value>
        IApplicationBuilder Builder { get; }

        /// <summary>
        /// 服务提供程序。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IServiceProvider"/>。
        /// </value>
        IServiceProvider Services { get; }
    }
}
