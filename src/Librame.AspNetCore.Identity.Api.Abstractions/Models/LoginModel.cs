#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Models
{
    /// <summary>
    /// 登入模型。
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 用户。
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
