// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 外部提供程序。
    /// </summary>
    public class ExternalProvider
    {
        /// <summary>
        /// 显示名称。
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 认证方案。
        /// </summary>
        public string AuthenticationScheme { get; set; }
    }
}