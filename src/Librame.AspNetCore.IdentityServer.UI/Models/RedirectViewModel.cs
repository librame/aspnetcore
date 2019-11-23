// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 重定向视图模型。
    /// </summary>
    public class RedirectViewModel
    {
        /// <summary>
        /// 重定向 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string RedirectUrl { get; set; }
    }
}