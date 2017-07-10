#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    using Models;

    /// <summary>
    /// 角色管理器接口。
    /// </summary>
    public interface IRoleManager : IManager
    {
        /// <summary>
        /// 异步获取指定用户的角色集合。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回角色集合。</returns>
        Task<IEnumerable<string>> GetRoles(IUserModel user);
    }
}
