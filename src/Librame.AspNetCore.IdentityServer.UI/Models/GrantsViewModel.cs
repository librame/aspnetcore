// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 授予集合视图模型。
    /// </summary>
    public class GrantsViewModel
    {
        /// <summary>
        /// 授予集合。
        /// </summary>
        public IEnumerable<GrantViewModel> Grants { get; set; }
    }


    /// <summary>
    /// 授予视图模型。
    /// </summary>
    public class GrantViewModel
    {
        /// <summary>
        /// 客户端标识。
        /// </summary>
        public string ClientId { get; set; }

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
        /// 创建时间。
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// 过期时间。
        /// </summary>
        public DateTime? Expires { get; set; }

        /// <summary>
        /// 身份授予名称集合。
        /// </summary>
        public IEnumerable<string> IdentityGrantNames { get; set; }

        /// <summary>
        /// API 授予名称集合。
        /// </summary>
        public IEnumerable<string> ApiGrantNames { get; set; }
    }
}