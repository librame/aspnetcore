#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 应用站点导航接口。
    /// </summary>
    public interface IApplicationSiteNavigation
    {
        /// <summary>
        /// 关于。
        /// </summary>
        NavigationDescriptor About { get; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        NavigationDescriptor AccessDenied { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        NavigationDescriptor Contact { get; }

        /// <summary>
        /// 首页。
        /// </summary>
        NavigationDescriptor Index { get; }

        /// <summary>
        /// 登入。
        /// </summary>
        NavigationDescriptor Login { get; }

        /// <summary>
        /// 登出。
        /// </summary>
        NavigationDescriptor Logout { get; }

        /// <summary>
        /// 管理。
        /// </summary>
        NavigationDescriptor Manage { get; }

        /// <summary>
        /// 隐私。
        /// </summary>
        NavigationDescriptor Privacy { get; }

        /// <summary>
        /// 注册。
        /// </summary>
        NavigationDescriptor Register { get; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        NavigationDescriptor Sitemap { get; }
    }
}
