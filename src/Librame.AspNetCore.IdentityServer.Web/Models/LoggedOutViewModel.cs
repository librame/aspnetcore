// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.Web.Models
{
    /// <summary>
    /// 登出视图模型。
    /// </summary>
    public class LoggedOutViewModel
    {
        /// <summary>
        /// 后置登出重定向 URI。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string PostLogoutRedirectUri { get; set; }

        /// <summary>
        /// 客户端名称。
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 签出 Iframe URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string SignOutIframeUrl { get; set; }

        /// <summary>
        /// 自动重定向后签出。
        /// </summary>
        public bool AutomaticRedirectAfterSignOut { get; set; } = false;

        /// <summary>
        /// 登出标识。
        /// </summary>
        public string LogoutId { get; set; }

        /// <summary>
        /// 触发外部登出。
        /// </summary>
        public bool TriggerExternalSignout
            => ExternalAuthenticationScheme != null;

        /// <summary>
        /// 外部认证方案。
        /// </summary>
        public string ExternalAuthenticationScheme { get; set; }
    }
}