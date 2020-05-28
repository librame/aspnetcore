#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Options
{
    /// <summary>
    /// 主题包选项。
    /// </summary>
    public class ThemepackOptions
    {
        /// <summary>
        /// 公共布局 Cookie 授权可见性（默认可见）。
        /// </summary>
        public bool CommonCookieConsentVisibility { get; set; }
            = true;

        /// <summary>
        /// 公共布局底栏版权可见性（默认可见）。
        /// </summary>
        public bool CommonFooterVisibility { get; set; }
            = true;

        /// <summary>
        /// 公共布局顶栏导航可见性（默认可见）。
        /// </summary>
        public bool CommonHeaderNavigationVisibility { get; set; }
            = true;

        /// <summary>
        /// 公共布局本地化可见性（默认可见）。
        /// </summary>
        public bool CommonLocalizationVisibility { get; set; }
            = true;

        /// <summary>
        /// 公共布局登陆可见性（默认可见）。
        /// </summary>
        public bool CommonLoginVisibility { get; set; }
            = true;


        /// <summary>
        /// 管理布局底栏版权可见性（默认可见）。
        /// </summary>
        public bool ManageFooterVisibility { get; set; }
            = true;

        /// <summary>
        /// 管理布局登陆可见性（默认可见）。
        /// </summary>
        public bool ManageLoginVisibility { get; set; }
            = true;

        /// <summary>
        /// 管理布局侧栏导航可见性（默认可见）。
        /// </summary>
        public bool ManageSidebarVisibility { get; set; }
            = true;
    }
}
