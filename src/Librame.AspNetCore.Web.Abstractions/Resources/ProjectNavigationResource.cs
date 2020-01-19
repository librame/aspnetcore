#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 区域导航资源。
    /// </summary>
    public class ProjectNavigationResource : IResource
    {
        /// <summary>
        /// 首页。
        /// </summary>
        public string Index { get; set; }


        /// <summary>
        /// 关于。
        /// </summary>
        public string About { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        public string Contact { get; }

        /// <summary>
        /// 隐私。
        /// </summary>
        public string Privacy { get; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        public string Sitemap { get; }

        /// <summary>
        /// 项目库。
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// 反馈。
        /// </summary>
        public string Issues { get; set; }

        /// <summary>
        /// 授权。
        /// </summary>
        public string Licenses { get; set; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        public string AccessDenied { get; set; }


        /// <summary>
        /// 注册。
        /// </summary>
        public string Register { get; set; }

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
    }
}
