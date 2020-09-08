#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 用户模型。
    /// </summary>
    public class UserModel : AbstractCreationIdentifierModel
    {
        /// <summary>
        /// 用户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电邮。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电邮已确认。
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 电话号码。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 电话号码已确认。
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 访问失败次数。
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 已启用锁定。
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 锁定结束时间。
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }


        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 确认邮件 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string EmailConfirmationUrl { get; set; }


        /// <summary>
        /// 角色列表。
        /// </summary>
        public IReadOnlyList<RoleModel> Roles { get; set; }

        /// <summary>
        /// 用户声明列表。
        /// </summary>
        public IReadOnlyList<UserClaimModel> UserClaims { get; set; }

        /// <summary>
        /// 用户登入列表。
        /// </summary>
        public IReadOnlyList<UserLoginModel> UserLogins { get; set; }

        /// <summary>
        /// 用户令牌列表。
        /// </summary>
        public IReadOnlyList<UserTokenModel> UserTokens { get; set; }
    }
}
