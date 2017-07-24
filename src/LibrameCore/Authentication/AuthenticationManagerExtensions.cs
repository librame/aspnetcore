#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http.Authentication;
using System.Threading.Tasks;

namespace LibrameCore.Authentication
{
    /// <summary>
    /// 认证管理器静态扩展。
    /// </summary>
    public static class AuthenticationManagerExtensions
    {

        /// <summary>
        /// 异步登出。
        /// </summary>
        /// <param name="manager">给定的认证管理器。</param>
        /// <returns>返回异步操作。</returns>
        public static Task SignOutAsync(this AuthenticationManager manager)
        {
            return manager.SignOutAsync(AuthenticationOptions.DEFAULT_SCHEME);
        }

    }
}
