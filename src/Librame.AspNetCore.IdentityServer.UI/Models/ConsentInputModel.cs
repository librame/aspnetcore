// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 批准输入模型。
    /// </summary>
    public class ConsentInputModel
    {
        /// <summary>
        /// 按钮。
        /// </summary>
        public string Button { get; set; }

        /// <summary>
        /// 已批准的范围集合。
        /// </summary>
        public IEnumerable<string> ScopesConsented { get; set; }

        /// <summary>
        /// 记住批准。
        /// </summary>
        public bool RememberConsent { get; set; }

        /// <summary>
        /// 返回 URL。
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}