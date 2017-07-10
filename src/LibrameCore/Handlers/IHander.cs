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

namespace LibrameCore.Handlers
{
    /// <summary>
    /// 处理程序接口。
    /// </summary>
    public interface IHander
    {
        /// <summary>
        /// 配置处理程序。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        void Configure(IApplicationBuilder app);
    }
}
