#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 界面站点地图接口。
    /// </summary>
    public interface IInterfaceSitemap
    {
        /// <summary>
        /// 区域。
        /// </summary>
        string Area { get; }


        /// <summary>
        /// 首页。
        /// </summary>
        NavigationDescriptor Index { get; }


        /// <summary>
        /// 拒绝访问。
        /// </summary>
        NavigationDescriptor AccessDenied { get; }

        /// <summary>
        /// 隐私。
        /// </summary>
        NavigationDescriptor Privacy { get; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        NavigationDescriptor Sitemap { get; }


        /// <summary>
        /// 登入。
        /// </summary>
        NavigationDescriptor Login { get; }

        /// <summary>
        /// 登出。
        /// </summary>
        NavigationDescriptor Logout { get; }

        /// <summary>
        /// 注册。
        /// </summary>
        NavigationDescriptor Register { get; }


        /// <summary>
        /// 管理。
        /// </summary>
        NavigationDescriptor Manage { get; }


        /// <summary>
        /// 公共顶栏导航列表。
        /// </summary>
        List<NavigationDescriptor> CommonHeader { get; }

        /// <summary>
        /// 公共侧边栏导航列表。
        /// </summary>
        List<NavigationDescriptor> CommonSidebar { get; }

        /// <summary>
        /// 公共底栏导航列表。
        /// </summary>
        List<NavigationDescriptor> CommonFootbar { get; }


        /// <summary>
        /// 管理顶栏导航列表。
        /// </summary>
        List<NavigationDescriptor> ManageHeader { get; }

        /// <summary>
        /// 管理侧边栏导航列表。
        /// </summary>
        List<NavigationDescriptor> ManageSidebar { get; }

        /// <summary>
        /// 管理底栏导航列表。
        /// </summary>
        List<NavigationDescriptor> ManageFootbar { get; }
    }
}
