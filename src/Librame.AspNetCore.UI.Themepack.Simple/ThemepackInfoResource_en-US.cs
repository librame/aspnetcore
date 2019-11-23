#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.UI.Themepack.Simple
{
    using Extensions.Core;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class ThemepackInfoResource_en_US : ResourceDictionary
    {
        public ThemepackInfoResource_en_US()
            : base()
        {
            AddOrUpdate("Name", nameof(Simple), (key, value) => nameof(Simple));
            AddOrUpdate("PrivacyAndCookiePolicy", PrivacyAndCookiePolicy, (key, value) => PrivacyAndCookiePolicy);
            AddOrUpdate("PrivacyAndCookiePolicyButton", "Accept", (key, value) => "Accept");
        }


        private static string PrivacyAndCookiePolicy
            => "We are committed to protecting your privacy. Read our customer privacy policy to learn how we collect, use, disclose, transmit and store your information.";
    }
}
