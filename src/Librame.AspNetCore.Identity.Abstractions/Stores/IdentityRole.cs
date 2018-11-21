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
    using Extensions.Data;

    /// <summary>
    /// 身份角色。
    /// </summary>
    public class IdentityRole : Microsoft.AspNetCore.Identity.IdentityRole, IId<string>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityRole"/> 实例。
        /// </summary>
        public IdentityRole()
            : base()
        {
        }

        /// <summary>
        /// 构造一个 <see cref="IdentityRole"/> 实例。
        /// </summary>
        /// <param name="roleName">给定的角色名称。</param>
        public IdentityRole(string roleName)
            : base(roleName)
        {
            NormalizedName = roleName;
        }

    }
}
