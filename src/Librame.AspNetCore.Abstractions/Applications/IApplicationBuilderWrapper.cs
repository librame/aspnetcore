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
    /// 应用程序构建器包装接口。
    /// </summary>
    public interface IApplicationBuilderWrapper
    {
        /// <summary>
        /// 应用构建器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationBuilder"/>。
        /// </value>
        IApplicationBuilder Builder { get; }
    }
}
