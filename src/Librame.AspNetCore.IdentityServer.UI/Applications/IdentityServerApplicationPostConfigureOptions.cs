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
    /// 身份服务器应用后置配置选项。
    /// </summary>
    public class IdentityServerApplicationPostConfigureOptions : UiApplicationPostConfigureOptionsWithCookieBase
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerApplicationPostConfigureOptions"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        public IdentityServerApplicationPostConfigureOptions(IApplicationContext context)
            : this(context, "IdentityServer")
        {
        }

        /// <summary>
        /// 构造一个 <see cref="IdentityServerApplicationPostConfigureOptions"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="areaName">指定的区域名称。</param>
        protected IdentityServerApplicationPostConfigureOptions(IApplicationContext context, string areaName)
            : base(context, areaName)
        {
        }

    }
}
