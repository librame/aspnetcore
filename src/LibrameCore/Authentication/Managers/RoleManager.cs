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

namespace LibrameStandard.Authentication.Managers
{
    using Models;
    using Utilities;

    /// <summary>
    /// 角色管理器。
    /// </summary>
    public class RoleManager : AbstractManager, IRoleManager
    {
        /// <summary>
        /// 构造一个角色管理器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public RoleManager(ILibrameBuilder builder)
            : base(builder)
        {
        }


        /// <summary>
        /// 异步获取指定用户的角色集合。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回角色模型集合。</returns>
        public virtual Task<IEnumerable<IRoleModel>> GetRoles(IUserModel user)
        {
            IRoleModel role = new RoleModel();

            return Task.FromResult(role.AsEnumerable());
        }

    }
}
