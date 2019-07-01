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
    /// 身份用户令牌资源。
    /// </summary>
    public class IdentityUserTokenResource : IResource
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
        /// 名称。
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值。
        /// </summary>
        public string Value { get; set; }
    }
}
