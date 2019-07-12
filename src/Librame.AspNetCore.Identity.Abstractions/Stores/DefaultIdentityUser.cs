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
    /// 默认身份用户。
    /// </summary>
    public class DefaultIdentityUser : IdentityUser<string>, IGenId
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityUser"/> 实例。
        /// </summary>
        public DefaultIdentityUser()
            : this(null)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityUser"/> 实例。
        /// </summary>
        /// <param name="userName">给定的用户名称。</param>
        public DefaultIdentityUser(string userName)
            : base(userName)
        {
            // 默认使用空标识符，新增推荐使用服务注入
            Id = GuIdentifier.Empty;
            NormalizedUserName = userName;
        }

    }
}
