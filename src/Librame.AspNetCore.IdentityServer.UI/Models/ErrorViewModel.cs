// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 错误视图模型。
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 错误消息。
        /// </summary>
        public ErrorMessage Error { get; set; }
    }
}