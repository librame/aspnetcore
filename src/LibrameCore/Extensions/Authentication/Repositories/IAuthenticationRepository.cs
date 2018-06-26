#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Authentication
{
    using Descriptors;
    using Managers;
    
    /// <summary>
    /// 认证仓库接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TRoleId">指定的角色主键类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户主键类型。</typeparam>
    /// <typeparam name="TUserRoleId">指定的用户角色主键类型。</typeparam>
    public interface IAuthenticationRepository<TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId>
        where TRole : class, IRoleDescriptor<TRoleId>
        where TUser : class, IUserDescriptor<TUserId>
        where TUserRole : class, IUserRoleDescriptor<TUserRoleId, TUserId, TRoleId>
    {
        /// <summary>
        /// 认证扩展选项。
        /// </summary>
        AuthenticationExtensionOptions Options { get; }
        
        /// <summary>
        /// 角色仓库（支持读写分离）。
        /// </summary>
        IRepository<DbContext, DbContext, TRole> RoleRepository { get; }

        /// <summary>
        /// 用户仓库（支持读写分离）。
        /// </summary>
        IRepository<DbContext, DbContext, TUser> UserRepository { get; }

        /// <summary>
        /// 用户角色仓库（支持读写分离）。
        /// </summary>
        IRepository<DbContext, DbContext, TUserRole> UserRoleRepository { get; }

        /// <summary>
        /// 密码管理器。
        /// </summary>
        IPasswordManager PasswordManager { get; }


        #region User
        
        /// <summary>
        /// 受保护的用户名集合。
        /// </summary>
        IEnumerable<string> ProtectedUsernames { get; }


        /// <summary>
        /// 异步尝试创建用户。
        /// </summary>
        /// <param name="user">给定的用户。</param>
        /// <returns>返回身份结果。</returns>
        Task<IdentityResult> TryCreateUserAsync(TUser user);


        /// <summary>
        /// 异步验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <returns>返回身份结果和用户。</returns>
        Task<(IdentityResult Result, TUser User)> ValidateUserAsync(string name, string passwd);


        /// <summary>
        /// 验证唯一性。
        /// </summary>
        /// <param name="field">给定的字段。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回表示是否唯一的布尔值。</returns>
        Task<bool> ValidateUserUniquenessAsync(string field, string value);

        #endregion


        #region Role

        /// <summary>
        /// 异步获取指定用户的角色集合。
        /// </summary>
        /// <param name="user">给定的用户。</param>
        /// <returns>返回角色集合。</returns>
        Task<IEnumerable<TRole>> GetRolesAsync(TUser user);

        /// <summary>
        /// 异步获取指定用户的角色名集合。
        /// </summary>
        /// <param name="username">给定的用户名。</param>
        /// <returns>返回角色名集合。</returns>
        Task<IEnumerable<string>> GetRoleNamesAsync(string username);

        #endregion

    }
}
