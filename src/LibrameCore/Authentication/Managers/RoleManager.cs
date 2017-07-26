#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    using Models;

    /// <summary>
    /// 角色管理器。
    /// </summary>
    public class RoleManager : AbstractManager, IRoleManager
    {
        /// <summary>
        /// 默认名称。
        /// </summary>
        internal const string DEFAULT_NAME = "Administrator";


        /// <summary>
        /// 构造一个角色管理器实例。
        /// </summary>
        /// <param name="options">给定的认证选项。</param>
        public RoleManager(IOptions<AuthenticationOptions> options)
            : base(options)
        {
        }


        /// <summary>
        /// 异步获取指定用户的角色集合。
        /// </summary>
        /// <param name="username">给定的用户名称。</param>
        /// <returns>返回角色集合。</returns>
        public virtual Task<IEnumerable<string>> GetRoles(string username)
        {
            return Task.FromResult(DEFAULT_NAME.AsEnumerable());
        }

        /// <summary>
        /// 异步获取指定用户的角色集合。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回角色集合。</returns>
        public virtual Task<IEnumerable<string>> GetRoles(IUserModel user)
        {
            return Task.FromResult(DEFAULT_NAME.AsEnumerable());
        }

    }
}
