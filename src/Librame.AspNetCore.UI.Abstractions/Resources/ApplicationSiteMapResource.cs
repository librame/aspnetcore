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
    using Extensions.Core;

    /// <summary>
    /// 应用站点地图资源。
    /// </summary>
    public class ApplicationSiteMapResource : IResource
    {
        /// <summary>
        /// 关于。
        /// </summary>
        public string About { get; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        public string AccessDenied { get; set; }

        /// <summary>
        /// 联系。
        /// </summary>
        public string Contact { get; }

        /// <summary>
        /// 首页。
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// 登入。
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// 登出。
        /// </summary>
        public string Logout { get; set; }

        /// <summary>
        /// 管理。
        /// </summary>
        public string Manage { get; set; }

        /// <summary>
        /// 隐私。
        /// </summary>
        public string Privacy { get; }

        /// <summary>
        /// 注册。
        /// </summary>
        public string Register { get; set; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        public string Sitemap { get; }
    }
}
