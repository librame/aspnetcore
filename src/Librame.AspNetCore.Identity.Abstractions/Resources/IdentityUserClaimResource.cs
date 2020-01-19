#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 身份用户声明资源。
    /// </summary>
    public class IdentityUserClaimResource : IResource
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户标识。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 声明类型。
        /// </summary>
        public string ClaimType { get; set; }
        /// <summary>
        /// 声明值。
        /// </summary>
        public string ClaimValue { get; set; }
    }
}
