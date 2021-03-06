﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 身份用户登入资源。
    /// </summary>
    public class IdentityUserLoginResource : IResource
    {
        /// <summary>
        /// 用户标识。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 登入提供程序。
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// 提供程序密钥。
        /// </summary>
        public string ProviderKey { get; set; }
        /// <summary>
        /// 提供程序显示名称。
        /// </summary>
        public string ProviderDisplayName { get; set; }
    }
}
