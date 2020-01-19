// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Librame.AspNetCore.IdentityServer.Web.Models
{
    /// <summary>
    /// 范围视图模型。
    /// </summary>
    public class ScopeViewModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称。
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 强调。
        /// </summary>
        public bool Emphasize { get; set; }

        /// <summary>
        /// 必填。
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 检查。
        /// </summary>
        public bool Checked { get; set; }
    }
}
