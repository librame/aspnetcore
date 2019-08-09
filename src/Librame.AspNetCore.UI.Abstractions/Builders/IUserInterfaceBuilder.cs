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
    /// 用户界面构建器接口。
    /// </summary>
    public interface IUserInterfaceBuilder : IExtensionBuilder
    {
        /// <summary>
        /// 应用后置配置选项类型。
        /// </summary>
        Type ApplicationPostConfigureOptionsType { get; }


        /// <summary>
        /// 添加应用后置配置选项类型。
        /// </summary>
        /// <typeparam name="TAppPostConfigureOptions">指定的应用后置配置选项类型。</typeparam>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        IUserInterfaceBuilder AddApplicationPostConfigureOptions<TAppPostConfigureOptions>()
            where TAppPostConfigureOptions : class, IApplicationPostConfigureOptions;
    }
}
