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
    /// 界面信息接口。
    /// </summary>
    public interface IInterfaceInfo : IApplicationInfo
    {
        /// <summary>
        /// 站点地图。
        /// </summary>
        /// <value>返回 <see cref="IInterfaceSitemap"/>。</value>
        IInterfaceSitemap Sitemap { get; }
    }
}
