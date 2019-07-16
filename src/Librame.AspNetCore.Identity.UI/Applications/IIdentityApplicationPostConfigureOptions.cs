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
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 身份应用程序后置配置选项接口。
    /// </summary>
    public interface IIdentityApplicationPostConfigureOptions : IApplicationPostConfigureOptionsBase, IPostConfigureOptions<CookieAuthenticationOptions>
    {
    }
}
