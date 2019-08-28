#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.UI
{
    using Extensions.Core;

    /// <summary>
    /// 用户视图模型资源。
    /// </summary>
    public class UserViewModelResource : IResource
    {
        /// <summary>
        /// 称呼。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电邮。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话。
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 当前密码。
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码。
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认新密码。
        /// </summary>
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// 令牌。
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public string RememberMe { get; set; }

        /// <summary>
        /// 双因子验证码。
        /// </summary>
        public string TwoFactorCode { get; set; }

        /// <summary>
        /// 记住本机。
        /// </summary>
        public string RememberMachine { get; set; }

        /// <summary>
        /// 恢复码。
        /// </summary>
        public string RecoveryCode { get; set; }
    }
}
