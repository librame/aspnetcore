#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;

    /// <summary>
    /// 注册模型。
    /// </summary>
    public class RegisterModel : AbstractApiModel
    {
        /// <summary>
        /// 邮箱。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 称呼。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

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
