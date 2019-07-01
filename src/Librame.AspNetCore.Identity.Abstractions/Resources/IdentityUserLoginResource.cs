#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;

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
