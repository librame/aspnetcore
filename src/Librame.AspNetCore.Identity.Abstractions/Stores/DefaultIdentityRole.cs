#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 默认身份角色。
    /// </summary>
    public class DefaultIdentityRole : IdentityRole<string>, IGenId
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityRole"/> 实例。
        /// </summary>
        public DefaultIdentityRole()
            : this(null)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityRole"/> 实例。
        /// </summary>
        /// <param name="roleName">给定的角色名称。</param>
        public DefaultIdentityRole(string roleName)
            : base(roleName)
        {
            // 默认使用空标识符，新增推荐使用服务注入
            Id = UniqueIdentifier.Empty;
            //NormalizedName = roleName;
        }

    }
}
