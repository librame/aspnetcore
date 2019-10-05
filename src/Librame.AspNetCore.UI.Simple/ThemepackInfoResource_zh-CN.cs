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

    class ThemepackInfoResource_zh_CN : ResourceDictionary
    {
        public ThemepackInfoResource_zh_CN()
            : base()
        {
            AddOrUpdate("Name", "简约", (key, value) => "简约");
            AddOrUpdate("PrivacyAndCookiePolicy", PrivacyAndCookiePolicy, (key, value) => PrivacyAndCookiePolicy);
            AddOrUpdate("PrivacyAndCookiePolicyButton", "接受", (key, value) => "接受");
        }


        private static string PrivacyAndCookiePolicy
            => "我们致力于保护您的隐私。阅读我们的客户隐私政策，了解我们如何收集、使用、披露、传输和存储您的个人信息。";
    }
}
