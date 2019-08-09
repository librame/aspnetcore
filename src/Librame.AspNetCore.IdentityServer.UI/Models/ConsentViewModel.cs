// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 批准视图模型。
    /// </summary>
    public class ConsentViewModel : ConsentInputModel
    {
        /// <summary>
        /// 客户端名称。
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 客户端 URL。
        /// </summary>
        public string ClientUrl { get; set; }

        /// <summary>
        /// 客户端标志 URL。
        /// </summary>
        public string ClientLogoUrl { get; set; }

        /// <summary>
        /// 允许记住批准。
        /// </summary>
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// 身份范围视图集合。
        /// </summary>
        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

        /// <summary>
        /// 资源范围视图集合。
        /// </summary>
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
    }
}
