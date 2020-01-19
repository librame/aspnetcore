#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 登入 API 模型。
    /// </summary>
    public class LoginApiModel : AbstractApiModel
    {
        /// <summary>
        /// 电邮。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 用户标识。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 令牌。
        /// </summary>
        public string Token { get; set; }
    }
}
