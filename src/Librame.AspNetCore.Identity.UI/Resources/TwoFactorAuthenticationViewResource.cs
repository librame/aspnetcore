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
    using AspNetCore.UI;

    /// <summary>
    /// 双因子验证视图资源。
    /// </summary>
    public class TwoFactorAuthenticationViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 无恢复码标题。
        /// </summary>
        public string RecoveryCodesLeft0Title { get; set; }

        /// <summary>
        /// 无恢复码信息。
        /// </summary>
        public string RecoveryCodesLeft0Info { get; set; }

        /// <summary>
        /// 唯一恢复码标题。
        /// </summary>
        public string RecoveryCodesLeft1Title { get; set; }

        /// <summary>
        /// 唯一恢复码信息。
        /// </summary>
        public string RecoveryCodesLeft1Info { get; set; }

        /// <summary>
        /// 少量恢复码标题。
        /// </summary>
        public string RecoveryCodesLeftFewTitle { get; set; }

        /// <summary>
        /// 少量恢复码信息。
        /// </summary>
        public string RecoveryCodesLeftFewInfo { get; set; }

        /// <summary>
        /// 禁用双因子验证。
        /// </summary>
        public string Disable2fa { get; set; }

        /// <summary>
        /// 生成恢复码。
        /// </summary>
        public string GenerateRecoveryCodes { get; set; }

        /// <summary>
        /// 认证器应用。
        /// </summary>
        public string AuthenticatorApp { get; set; }

        /// <summary>
        /// 新增认证器应用。
        /// </summary>
        public string AddAuthenticatorApp { get; set; }

        /// <summary>
        /// 安装认证器应用。
        /// </summary>
        public string SetupAuthenticatorApp { get; set; }

        /// <summary>
        /// 重置认证器应用。
        /// </summary>
        public string ResetAuthenticatorApp { get; set; }
    }
}
