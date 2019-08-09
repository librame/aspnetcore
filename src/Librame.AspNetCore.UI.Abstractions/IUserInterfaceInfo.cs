#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 用户界面信息接口。
    /// </summary>
    public interface IUserInterfaceInfo : IApplicationInfo
    {
        /// <summary>
        /// 添加本地化信息集合。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        void AddLocalizers(ConcurrentDictionary<string, IStringLocalizer> localizers, IServiceProvider serviceProvider);

        /// <summary>
        /// 添加导航信息集合。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        void AddNavigations(ConcurrentDictionary<string, List<NavigationDescriptor>> navigations, IServiceProvider serviceProvider);
    }
}
