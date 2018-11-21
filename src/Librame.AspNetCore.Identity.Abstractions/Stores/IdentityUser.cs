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
    /// 身份用户。
    /// </summary>
    public class IdentityUser : Microsoft.AspNetCore.Identity.IdentityUser, IId<string>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityRole"/> 实例。
        /// </summary>
        public IdentityUser()
            : base()
        {
        }

        /// <summary>
        /// 构造一个 <see cref="IdentityRole"/> 实例。
        /// </summary>
        /// <param name="userName">给定的用户名称。</param>
        public IdentityUser(string userName)
            : base(userName)
        {
            NormalizedUserName = userName;
        }

        
        /// <summary>
        /// 命名。
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 年龄。
        /// </summary>
        public virtual int Age { get; set; }
    }
}
