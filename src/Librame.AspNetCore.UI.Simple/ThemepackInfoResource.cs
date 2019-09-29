#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI.Themepack.Simple
{
    using Extensions.Core;

    /// <summary>
    /// 主题包信息资源。
    /// </summary>
    public class ThemepackInfoResource : IResource
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 隐私和 Cookie 策略。
        /// </summary>
        public string PrivacyAndCookiePolicy { get; set; }
    }
}
