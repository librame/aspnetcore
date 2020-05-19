#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.IdentityServer.Options
{
    /// <summary>
    /// 帐户选项。
    /// </summary>
    public class AccountOptions
    {
        /// <summary>
        /// 允许本地登入。
        /// </summary>
        public bool AllowLocalLogin { get; set; }
            = true;

        /// <summary>
        /// 允许记住登入。
        /// </summary>
        public bool AllowRememberLogin { get; set; }
            = true;

        /// <summary>
        /// 记住我登入的持续间隔。
        /// </summary>
        public TimeSpan RememberMeLoginDuration { get; set; }
            = TimeSpan.FromDays(30);

        /// <summary>
        /// 显示登出提示。
        /// </summary>
        public bool ShowLogoutPrompt { get; set; }
            = true;

        /// <summary>
        /// 自动重定向后登出。
        /// </summary>
        public bool AutomaticRedirectAfterSignOut { get; set; }
            = false;

        /// <summary>
        /// 指定正在使用的 Windows 身份验证方案。
        /// </summary>
        /// <remarks>
        /// 参考 Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme。
        /// </remarks>
        public string WindowsAuthenticationSchemeName { get; set; }

        /// <summary>
        /// 如果用户使用 Windows 认证，是否应该从 Windows 加载组。
        /// </summary>
        public bool IncludeWindowsGroups { get; set; }
            = false;

        /// <summary>
        /// 无效证书的错误消息。
        /// </summary>
        public string InvalidCredentialsErrorMessage { get; set; }
            = "Invalid username or password";
    }
}
