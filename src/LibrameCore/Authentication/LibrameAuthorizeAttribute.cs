#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authorization;

namespace LibrameCore.Authentication
{
    /// <summary>
    /// Librame 授权属性特性。
    /// </summary>
    public class LibrameAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 构造一个 Librame 授权属性特性实例。
        /// </summary>
        public LibrameAuthorizeAttribute()
        {
            ActiveAuthenticationSchemes = AuthenticationOptions.DEFAULT_SCHEME;
        }

    }
}
