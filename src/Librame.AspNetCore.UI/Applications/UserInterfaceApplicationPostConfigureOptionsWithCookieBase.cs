#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication.Cookies;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 带 Cookie 的用户界面应用后置配置选项基类。
    /// </summary>
    public class UserInterfaceApplicationPostConfigureOptionsWithCookieBase : UserInterfaceApplicationPostConfigureOptionsBase,
        IUserInterfaceApplicationPostConfigureOptionsWithCookie
    {
        /// <summary>
        /// 构造一个 <see cref="UserInterfaceApplicationPostConfigureOptionsBase"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="areaName">指定的区域名称。</param>
        protected UserInterfaceApplicationPostConfigureOptionsWithCookieBase(IApplicationContext context, string areaName)
            : base(context, areaName)
        {
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="CookieAuthenticationOptions"/>。</param>
        public virtual void PostConfigure(string name, CookieAuthenticationOptions options)
        {
        }

    }
}
