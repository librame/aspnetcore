#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份应用站点配置。
    /// </summary>
    public class IdentityApplicationSiteConfiguration : ApplicationSiteConfigurationWithCookie
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityApplicationSiteConfiguration"/> 实例。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        public IdentityApplicationSiteConfiguration(IApplicationContext context)
            : this(context, "Identity")
        {
        }

        /// <summary>
        /// 构造一个 <see cref="IdentityApplicationSiteConfiguration"/> 实例。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">指定的区域。</param>
        protected IdentityApplicationSiteConfiguration(IApplicationContext context, string area)
            : base(context, area)
        {
        }

    }
}
