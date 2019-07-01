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
    public class DefaultIdentityUser : IdentityUser<string>, IId<string>
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityRole"/> 实例。
        /// </summary>
        public DefaultIdentityUser()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            Id = GuIdentifier.Empty;
        }

        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityRole"/> 实例。
        /// </summary>
        /// <param name="userName">给定的用户名称。</param>
        public DefaultIdentityUser(string userName)
            : base(userName)
        {
            NormalizedUserName = userName;
            UserName = userName;
        }
    }
}
