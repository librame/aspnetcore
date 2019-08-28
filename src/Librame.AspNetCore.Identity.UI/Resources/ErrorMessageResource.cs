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
    /// 错误消息资源。
    /// </summary>
    public class ErrorMessageResource : IResource
    {
        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 密码不正确。
        /// </summary>
        public string PasswordNotCorrect { get; set; }

        /// <summary>
        /// 双因子验证码。
        /// </summary>
        public string TwoFactorCode { get; set; }

        /// <summary>
        /// 电邮地址特性。
        /// </summary>
        public string EmailAddressAttribute { get; set; }

        /// <summary>
        /// 范围特性。
        /// </summary>
        public string RangeAttribute { get; set; }

        /// <summary>
        /// 必填特性。
        /// </summary>
        public string RequiredAttribute { get; set; }

        /// <summary>
        /// 外部提供程序错误。
        /// </summary>
        public string FromExternalProvider { get; set; }
        /// <summary>
        /// 加载外部登入出错。
        /// </summary>
        public string LoadingExternalLogin { get; set; }
        /// <summary>
        /// 在确认时加载外部登入出错。
        /// </summary>
        public string LoadingExternalLoginWhenConfirmation { get; set; }

        /// <summary>
        /// 无效的验证码。
        /// </summary>
        public string InvalidVerificationCode { get; set; }

        /// <summary>
        /// 无效的登入尝试。
        /// </summary>
        public string InvalidLoginAttempt { get; set; }

        /// <summary>
        /// 无效的验证器代码。
        /// </summary>
        public string InvalidAuthenticatorCode { get; set; }

        /// <summary>
        /// 输入的恢复码无效。
        /// </summary>
        public string InvalidRecoveryCodeEntered { get; set; }
    }
}
