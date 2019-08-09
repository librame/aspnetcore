// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 登入视图模型。
    /// </summary>
    public class LoginViewModel : LoginInputModel
    {
        /// <summary>
        /// 允许记住登入。
        /// </summary>
        public bool AllowRememberLogin { get; set; }
            = true;

        /// <summary>
        /// 启用本地登入。
        /// </summary>
        public bool EnableLocalLogin { get; set; }
            = true;

        /// <summary>
        /// 外部身份提供程序。
        /// </summary>
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }

        /// <summary>
        /// 可见的外部身份提供程序。
        /// </summary>
        public IEnumerable<ExternalProvider> VisibleExternalProviders
            => ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        /// <summary>
        /// 仅单个外部登入。
        /// </summary>
        public bool IsExternalLoginOnly
            => EnableLocalLogin == false && ExternalProviders?.Count() == 1;

        /// <summary>
        /// 外部登入方案。
        /// </summary>
        public string ExternalLoginScheme
            => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}