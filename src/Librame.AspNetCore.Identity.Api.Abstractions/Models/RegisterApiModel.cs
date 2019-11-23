#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;

    /// <summary>
    /// 注册 API 模型。
    /// </summary>
    public class RegisterApiModel : AbstractApiModel
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
        /// 确认邮件 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ConfirmEmailUrl { get; set; }

        /// <summary>
        /// 用户标识。
        /// </summary>
        public string UserId { get; set; }
    }
}
