#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 身份用户资源。
    /// </summary>
    public class IdentityUserResource : IResource
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 访问失败次数。
        /// </summary>
        public string AccessFailedCount { get; set; }

        /// <summary>
        /// 密码散列。
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// 标准化用户名。
        /// </summary>
        public string NormalizedUserName { get; set; }
        /// <summary>
        /// 用户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 标准化电邮。
        /// </summary>
        public string NormalizedEmail { get; set; }
        /// <summary>
        /// 电邮。
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 电邮确认。
        /// </summary>
        public string EmailConfirmed { get; set; }

        /// <summary>
        /// 电话号码。
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 电话号码确认。
        /// </summary>
        public string PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 启用锁定。
        /// </summary>
        public string LockoutEnabled { get; set; }
        /// <summary>
        /// 锁定结束日期。
        /// </summary>
        public string LockoutEnd { get; set; }

        /// <summary>
        /// 启用双因子验证。
        /// </summary>
        public string TwoFactorEnabled { get; set; }

        /// <summary>
        /// 并发标志。
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 安全标志。
        /// </summary>
        public string SecurityStamp { get; set; }
    }
}
