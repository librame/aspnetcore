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

namespace LibrameStandard.Handlers
{
    /// <summary>
    /// 泛型处理程序接口。
    /// </summary>
    /// <typeparam name="THandlerSettings">指定的处理程序设置类型。</typeparam>
    public interface IHander<THandlerSettings>
        where THandlerSettings : HandlerSettings
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }
        
        /// <summary>
        /// 处理程序设置。
        /// </summary>
        THandlerSettings Settings { get; }


        /// <summary>
        /// 开始处理。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        void OnHandling(IApplicationBuilder app);
    }
}
