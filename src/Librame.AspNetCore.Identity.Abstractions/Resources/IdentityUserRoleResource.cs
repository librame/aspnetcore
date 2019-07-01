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
    /// 身份用户角色资源。
    /// </summary>
    public class IdentityUserRoleResource : IResource
    {
        /// <summary>
        /// 用户标识。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色标识。
        /// </summary>
        public string RoleId { get; set; }
    }
}
