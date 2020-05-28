#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 状态消息资源。
    /// </summary>
    public class StatusMessageResource : IResource
    {
        /// <summary>
        /// 改变密码。
        /// </summary>
        public string ChangePassword { get; set; }

        /// <summary>
        /// 禁用双因子验证。
        /// </summary>
        public string DisableTwoFactor { get; set; }

        /// <summary>
        /// 启用验证器。
        /// </summary>
        public string EnableAuthenticator { get; set; }

        /// <summary>
        /// 外部登入已移除。
        /// </summary>
        public string ExternalLoginRemoved { get; set; }
        /// <summary>
        /// 外部登入已增加。
        /// </summary>
        public string ExternalLoginAdded { get; set; }

        /// <summary>
        /// 生成恢复码。
        /// </summary>
        public string GenerateRecoveryCodes { get; set; }

        /// <summary>
        /// 个人资料已更新。
        /// </summary>
        public string ProfileUpdated { get; set; }

        /// <summary>
        /// 验证电邮已发送。
        /// </summary>
        public string VerificationEmailSent { get; set; }

        /// <summary>
        /// 验证短信已发送。
        /// </summary>
        public string VerificationSmsSent { get; set; }

        /// <summary>
        /// 重置验证器。
        /// </summary>
        public string ResetAuthenticator { get; set; }

        /// <summary>
        /// 设置密码。
        /// </summary>
        public string SetPassword { get; set; }

        /// <summary>
        /// 双因子验证。
        /// </summary>
        public string TwoFactorAuthentication { get; set; }
    }
}
