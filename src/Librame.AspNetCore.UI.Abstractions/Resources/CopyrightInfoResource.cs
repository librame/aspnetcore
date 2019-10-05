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
    /// 版权信息资源。
    /// </summary>
    public class CopyrightInfoResource : IResource
    {
        /// <summary>
        /// 版权所有。
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// 技术支持。
        /// </summary>
        public string PoweredBy { get; set; }

        /// <summary>
        /// 语言。
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// 应用。
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// 主题。
        /// </summary>
        public string Themepack { get; set; }

        /// <summary>
        /// 在 NuGet 查找。
        /// </summary>
        public string SearchInNuget { get; set; }

        /// <summary>
        /// 跳转到微软官方站点。
        /// </summary>
        public string GotoMicrosoft { get; set; }
    }
}
