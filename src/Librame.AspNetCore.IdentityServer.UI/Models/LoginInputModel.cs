// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 登入输入模型。
    /// </summary>
    public class LoginInputModel
    {
        /// <summary>
        /// 用户名。
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 记住登入。
        /// </summary>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// 返回 URL。
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}