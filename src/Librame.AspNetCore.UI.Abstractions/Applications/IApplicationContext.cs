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
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 应用上下文接口。
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// 服务工厂。
        /// </summary>
        /// <value>
        /// 返回 <see cref="ServiceFactoryDelegate"/>。
        /// </value>
        ServiceFactoryDelegate ServiceFactory { get; }

        /// <summary>
        /// 应用当事人。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationPrincipal"/>。
        /// </value>
        IApplicationPrincipal Principal { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        IHostingEnvironment Environment { get; }


        /// <summary>
        /// 主题包信息集合。
        /// </summary>
        ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }

        /// <summary>
        /// 用户界面信息集合。
        /// </summary>
        ConcurrentDictionary<string, IUiInfo> Uis { get; }


        /// <summary>
        /// 本地化定位器集合。
        /// </summary>
        ConcurrentDictionary<string, IStringLocalizer> Localizers { get; }

        /// <summary>
        /// 导航集合。
        /// </summary>
        ConcurrentDictionary<string, List<NavigationDescriptor>> Navigations { get; }
    }
}
