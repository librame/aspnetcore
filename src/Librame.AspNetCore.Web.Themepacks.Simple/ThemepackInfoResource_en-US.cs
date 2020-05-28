#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Themepacks.Simple
{
    using Extensions.Core.Localizers;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class ThemepackInfoResource_en_US : ResourceDictionary
    {
        public ThemepackInfoResource_en_US()
            : base()
        {
            AddOrUpdate("DisplayName", nameof(Simple), (key, value) => nameof(Simple));
            AddOrUpdate("PrivacyAndCookiePolicy", PrivacyAndCookiePolicy, (key, value) => PrivacyAndCookiePolicy);
            AddOrUpdate("PrivacyAndCookiePolicyButton", "Accept", (key, value) => "Accept");
        }


        private static string PrivacyAndCookiePolicy
            => "We are committed to protecting your privacy. Read our customer privacy policy to learn how we collect, use, disclose, transmit and store your information.";
    }
}
