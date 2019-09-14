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
    /// 应用站点接口。
    /// </summary>
    public interface IApplicationSite
    {
        /// <summary>
        /// 站点导航。
        /// </summary>
        IApplicationSiteNavigation Navs { get; }
    }
}
