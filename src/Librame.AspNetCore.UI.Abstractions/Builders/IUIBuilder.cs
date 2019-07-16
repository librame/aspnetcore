#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// UI 构建器接口。
    /// </summary>
    public interface IUIBuilder : IBuilder
    {
        /// <summary>
        /// 应用程序上下文类型。
        /// </summary>
        Type ApplicationContextType { get; }

        /// <summary>
        /// 应用程序后置配置选项类型。
        /// </summary>
        Type ApplicationPostConfigureOptionsType { get; }


        /// <summary>
        /// 添加应用程序上下文类型。
        /// </summary>
        /// <typeparam name="TAppContext">指定的应用程序上下文类型。</typeparam>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        IUIBuilder AddApplicationContext<TAppContext>()
            where TAppContext : class, IApplicationContext;

        /// <summary>
        /// 添加应用程序后置配置选项类型。
        /// </summary>
        /// <typeparam name="TAppPostConfigureOptions">指定的应用程序后置配置选项类型。</typeparam>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        IUIBuilder AddApplicationPostConfigureOptions<TAppPostConfigureOptions>()
            where TAppPostConfigureOptions : class, IApplicationPostConfigureOptions;
    }
}
