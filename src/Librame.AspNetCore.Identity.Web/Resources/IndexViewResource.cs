#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 首页视图资源。
    /// </summary>
    public class IndexViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 发送按钮文本。
        /// </summary>
        public string SendButtonText { get; set; }

        /// <summary>
        /// 改变帐户设定。
        /// </summary>
        public string ChangeAccountSettings { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 修改密码。
        /// </summary>
        public string ChangePassword { get; set; }

        /// <summary>
        /// 设定密码。
        /// </summary>
        public string SetPassword { get; set; }

        /// <summary>
        /// 外部登入集合。
        /// </summary>
        public string ExternalLogins { get; set; }

        /// <summary>
        /// 管理外部登入集合。
        /// </summary>
        public string ManageLogins { get; set; }

        /// <summary>
        /// 手机号码。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 无手机号码。
        /// </summary>
        public string NonePhoneNumber { get; set; }

        /// <summary>
        /// 添加手机号码。
        /// </summary>
        public string AddPhoneNumber { get; set; }

        /// <summary>
        /// 修改手机号码。
        /// </summary>
        public string ChangePhoneNumber { get; set; }

        /// <summary>
        /// 移除手机号码。
        /// </summary>
        public string RemovePhoneNumber { get; set; }

        /// <summary>
        /// 双因子认证。
        /// </summary>
        public string TwoFactorAuthentication { get; set; }

        /// <summary>
        /// 启用双因子认证。
        /// </summary>
        public string EnableTwoFactor { get; set; }

        /// <summary>
        /// 禁用双因子认证。
        /// </summary>
        public string DisableTwoFactor { get; set; }

        /// <summary>
        /// 认证应用。
        /// </summary>
        public string AuthenticationApp { get; set; }

        /// <summary>
        /// 重置认证器密钥。
        /// </summary>
        public string ResetAuthenticatorKey { get; set; }

        /// <summary>
        /// 生成恢复码。
        /// </summary>
        public string GenerateRecoveryCode { get; set; }

        /// <summary>
        /// 您的密钥是。
        /// </summary>
        public string YourKeyIs { get; set; }


        /// <summary>
        /// 修改密码成功。
        /// </summary>
        public string ChangePasswordSuccess { get; set; }

        /// <summary>
        /// 设定密码成功。
        /// </summary>
        public string SetPasswordSuccess { get; set; }

        /// <summary>
        /// 设定双因子认证成功。
        /// </summary>
        public string SetTwoFactorSuccess { get; set; }

        /// <summary>
        /// 添加手机号码成功。
        /// </summary>
        public string AddPhoneSuccess { get; set; }

        /// <summary>
        /// 移除手机号码成功。
        /// </summary>
        public string RemovePhoneSuccess { get; set; }

        /// <summary>
        /// 异常。
        /// </summary>
        public string Error { get; set; }
    }
}
