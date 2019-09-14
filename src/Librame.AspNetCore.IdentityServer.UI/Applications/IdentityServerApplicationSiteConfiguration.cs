#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份服务器应用站点配置。
    /// </summary>
    public class IdentityServerApplicationSiteConfiguration : ApplicationSiteConfigurationWithCookie
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerApplicationSiteConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        public IdentityServerApplicationSiteConfiguration(IApplicationContext context)
            : this(context, "IdentityServer")
        {
        }

        /// <summary>
        /// 构造一个 <see cref="IdentityServerApplicationSiteConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">指定的区域。</param>
        protected IdentityServerApplicationSiteConfiguration(IApplicationContext context, string area)
            : base(context, area)
        {
        }

    }
}
