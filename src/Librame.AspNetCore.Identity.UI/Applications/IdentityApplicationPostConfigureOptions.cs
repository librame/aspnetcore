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
    /// 身份应用后置配置选项。
    /// </summary>
    public class IdentityApplicationPostConfigureOptions : UiApplicationPostConfigureOptionsWithCookieBase
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityApplicationPostConfigureOptions"/> 实例。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        public IdentityApplicationPostConfigureOptions(IApplicationContext context)
            : this(context, "Identity")
        {
        }

        /// <summary>
        /// 构造一个 <see cref="IdentityApplicationPostConfigureOptions"/> 实例。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="areaName">指定的区域名称。</param>
        protected IdentityApplicationPostConfigureOptions(IApplicationContext context, string areaName)
            : base(context, areaName)
        {
        }

    }
}
