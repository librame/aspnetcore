#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 应用上下文接口。
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// 服务提供程序。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IServiceProvider"/>。
        /// </value>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        IHostingEnvironment Environment { get; }

        /// <summary>
        /// 用户界面集合。
        /// </summary>
        ConcurrentDictionary<string, IUserInterfaceInfo> UserInterfaces { get; }

        /// <summary>
        /// 主题包集合。
        /// </summary>
        ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }

        /// <summary>
        /// 本地化定位器集合。
        /// </summary>
        ConcurrentDictionary<string, IStringLocalizer> Localizers { get; }

        /// <summary>
        /// 导航集合。
        /// </summary>
        ConcurrentDictionary<string, List<NavigationDescriptor>> Navigations { get; }


        /// <summary>
        /// 获取指定路由的用户界面信息。
        /// </summary>
        /// <param name="routeDescriptor">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="IUserInterfaceInfo"/>。</returns>
        IUserInterfaceInfo GetUserInterface(RouteDescriptor routeDescriptor);
    }
}
