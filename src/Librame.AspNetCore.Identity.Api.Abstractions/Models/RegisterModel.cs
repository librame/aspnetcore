#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Api.Models
{
    /// <summary>
    /// 注册模型。
    /// </summary>
    public class RegisterModel
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
        /// 确认邮件 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ConfirmEmailUrl { get; set; }
    }
}
