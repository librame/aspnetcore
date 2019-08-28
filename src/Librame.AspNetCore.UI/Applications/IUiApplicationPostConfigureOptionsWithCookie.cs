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

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 带 Cookie 的用户界面应用后置配置选项接口。
    /// </summary>
    public interface IUiApplicationPostConfigureOptionsWithCookie : IUiApplicationPostConfigureOptions,
        IPostConfigureOptions<CookieAuthenticationOptions>
    {
    }
}
