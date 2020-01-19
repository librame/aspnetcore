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
    /// 身份角色声明资源。
    /// </summary>
    public class IdentityRoleClaimResource : IResource
    {
        /// <summary>
        /// 标识。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色标识。
        /// </summary>
        public string RoleId { get; set; }

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
