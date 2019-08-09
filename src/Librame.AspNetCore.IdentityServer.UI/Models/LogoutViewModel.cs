// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Librame.AspNetCore.IdentityServer.UI
{
    /// <summary>
    /// 登出视图模型。
    /// </summary>
    public class LogoutViewModel : LogoutInputModel
    {
        /// <summary>
        /// 显示登出提示。
        /// </summary>
        public bool ShowLogoutPrompt { get; set; }
            = true;
    }
}
