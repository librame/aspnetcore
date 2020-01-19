// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Librame.AspNetCore.IdentityServer.Web.Models
{
    /// <summary>
    /// 登出输入模型。
    /// </summary>
    public class LogoutInputModel
    {
        /// <summary>
        /// 登出标识。
        /// </summary>
        public string LogoutId { get; set; }
    }
}
