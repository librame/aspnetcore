#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Web.Themepacks.Simple
{
    using AspNetCore.Resources;

    /// <summary>
    /// 主题包信息资源。
    /// </summary>
    public class ThemepackInfoResource : AbstractApplicationInfoResource
    {
        /// <summary>
        /// 隐私和 Cookie 策略。
        /// </summary>
        public string PrivacyAndCookiePolicy { get; set; }

        /// <summary>
        /// 隐私和 Cookie 策略按钮。
        /// </summary>
        public string PrivacyAndCookiePolicyButton { get; set; }
    }
}
