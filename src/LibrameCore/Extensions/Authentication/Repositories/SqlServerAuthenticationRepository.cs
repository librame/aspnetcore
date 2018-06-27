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
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Authentication.Repositories
{
    using Descriptors;
    using Managers;

    /// <summary>
    /// SQLServer 认证仓库。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    public class SqlServerAuthenticationRepository<TRole, TUser, TUserRole> : SqlServerAuthenticationRepository<TRole, TUser, TUserRole, int, int, int>, IAuthenticationRepository<TRole, TUser, TUserRole>
        where TRole : class, IRoleDescriptor<int>
        where TUser : class, IUserDescriptor<int>
        where TUserRole : class, IUserRoleDescriptor<int, int, int>
    {
        /// <summary>
        /// 构造一个 <see cref="SqlServerAuthenticationRepository{TRole, TUser, TUserRole}"/> 实例。
        /// </summary>
        /// <param name="options">给定的认证选项。</param>
        /// <param name="roleRepository">给定的角色仓库。</param>
        /// <param name="userRepository">给定的用户仓库。</param>
        /// <param name="userRoleRepository">给定的用户角色仓库。</param>
        /// <param name="passwordManager">密码管理器。</param>
        public SqlServerAuthenticationRepository(IOptionsMonitor<AuthenticationExtensionOptions> options,
            ISqlServerRepositoryWriter<TRole> roleRepository,
            ISqlServerRepositoryWriter<TUser> userRepository,
            ISqlServerRepositoryWriter<TUserRole> userRoleRepository,
            IPasswordManager passwordManager)
            : base(options, roleRepository, userRepository, userRoleRepository, passwordManager)
        {
        }

    }


    /// <summary>
    /// SQLServer 认证仓库。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TRoleId">指定的角色主键类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户主键类型。</typeparam>
    /// <typeparam name="TUserRoleId">指定的用户角色主键类型。</typeparam>
    public class SqlServerAuthenticationRepository<TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId>
        : IAuthenticationRepository<TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId>
        where TRole : class, IRoleDescriptor<TRoleId>
        where TUser : class, IUserDescriptor<TUserId>
        where TUserRole : class, IUserRoleDescriptor<TUserRoleId, TUserId, TRoleId>
    {
        /// <summary>
        /// 构造一个 <see cref="SqlServerAuthenticationRepository{TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId}"/> 实例。
        /// </summary>
        /// <param name="options">给定的认证选项。</param>
        /// <param name="roleRepository">给定的角色仓库。</param>
        /// <param name="userRepository">给定的用户仓库。</param>
        /// <param name="userRoleRepository">给定的用户角色仓库。</param>
        /// <param name="passwordManager">密码管理器。</param>
        public SqlServerAuthenticationRepository(IOptionsMonitor<AuthenticationExtensionOptions> options,
            ISqlServerRepositoryWriter<TRole> roleRepository,
            ISqlServerRepositoryWriter<TUser> userRepository,
            ISqlServerRepositoryWriter<TUserRole> userRoleRepository,
            IPasswordManager passwordManager)
        {
            Options = options.NotNull(nameof(options)).CurrentValue;
            RoleRepository = roleRepository.NotNull(nameof(roleRepository));
            UserRepository = userRepository.NotNull(nameof(userRepository));
            UserRoleRepository = userRoleRepository.NotNull(nameof(userRoleRepository));
            PasswordManager = passwordManager.NotNull(nameof(passwordManager));
        }


        /// <summary>
        /// 认证设置。
        /// </summary>
        public AuthenticationExtensionOptions Options { get; }
        
        /// <summary>
        /// 角色仓库（支持读写分离）。
        /// </summary>
        public IRepository<DbContext, DbContext, TRole> RoleRepository { get; }

        /// <summary>
        /// 用户仓库（支持读写分离）。
        /// </summary>
        public IRepository<DbContext, DbContext, TUser> UserRepository { get; }

        /// <summary>
        /// 用户角色仓库（支持读写分离）。
        /// </summary>
        public IRepository<DbContext, DbContext, TUserRole> UserRoleRepository { get; }

        /// <summary>
        /// 密码管理器。
        /// </summary>
        public IPasswordManager PasswordManager { get; }


        #region User
        
        /// <summary>
        /// 受保护的用户名集合。
        /// </summary>
        public IEnumerable<string> ProtectedUsernames => Options.ProtectedUsernames.Split(',').Invoke(n => n.ToLower());


        /// <summary>
        /// 检测是否包含受保护的用户名。
        /// </summary>
        /// <param name="name">给定的用户名。</param>
        /// <returns>返回布尔值。</returns>
        protected virtual bool ContainsProtectedUsernames(string name)
        {
            var lower = name.ToLower();

            foreach (var n in ProtectedUsernames)
            {
                if (lower.Contains(n))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// 异步尝试创建用户。
        /// </summary>
        /// <param name="user">给定的用户。</param>
        /// <returns>返回身份结果。</returns>
        public virtual async Task<IdentityResult> TryCreateUserAsync(TUser user)
        {
            try
            {
                if (ContainsProtectedUsernames(user.Name))
                    return IdentityResultHelper.InvalidName;

                if (await UserRepository.ExistsAsync(p => p.Name == user.Name))
                    return IdentityResultHelper.NameExists;

                await UserRepository.Writer.CreateAsync(user);

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResultHelper.CreateFailed(ex);
            }
        }


        /// <summary>
        /// 异步验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <returns>返回身份结果和用户。</returns>
        public virtual async Task<(IdentityResult Result, TUser User)> ValidateUserAsync(string name, string passwd)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwd))
                return (IdentityResultHelper.InvalidName, null);

            // 查找用户
            var user = await UserRepository.GetAsync(p => p.Name == name);
            if (user == null)
                return (IdentityResultHelper.NameNotExists, user);

            // 验证密码是否正确
            if (!PasswordManager.Validate(user.Passwd, passwd))
                return (IdentityResultHelper.PasswordError, user);

            return (IdentityResult.Success, user);
        }


        /// <summary>
        /// 验证唯一性。
        /// </summary>
        /// <param name="field">给定的字段。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回表示是否唯一的布尔值。</returns>
        public virtual async Task<bool> ValidateUserUniquenessAsync(string field, string value)
        {
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(value))
                return false;

            if (ContainsProtectedUsernames(value))
                return false;

            // 建立表达式
            var predicate = field.AsEqualPropertyExpression<TUser>(typeof(string), value);

            // 不存在表示唯一
            return (!await UserRepository.ExistsAsync(predicate));
        }

        #endregion


        #region Role

        /// <summary>
        /// 异步获取指定用户的角色集合。
        /// </summary>
        /// <param name="user">给定的用户。</param>
        /// <returns>返回角色集合。</returns>
        public virtual async Task<IEnumerable<TRole>> GetRolesAsync(TUser user)
        {
            if (user == null)
                return Enumerable.Empty<TRole>();

            // 查找关联的用户角色集合
            var userRoles = await UserRoleRepository.GetManyAsync(p => p.UserId.Equals(user.Id));
            if (userRoles == null)
                return Enumerable.Empty<TRole>();

            return GetRoleNamesAsync(userRoles, r => r);
        }

        /// <summary>
        /// 异步获取指定用户的角色集合。
        /// </summary>
        /// <param name="username">给定的用户名称。</param>
        /// <returns>返回角色集合。</returns>
        public virtual async Task<IEnumerable<string>> GetRoleNamesAsync(string username)
        {
            // 查找用户
            var user = await UserRepository.GetAsync(p => p.Name == username);

            if (user == null)
                return Enumerable.Empty<string>();

            // 查找关联的用户角色集合
            var userRoles = await UserRoleRepository.GetManyAsync(p => p.UserId.Equals(user.Id));
            if (userRoles == null)
                return Enumerable.Empty<string>();

            return GetRoleNamesAsync(userRoles, r => r.Name);
        }
        
        /// <summary>
        /// 获取角色名称集合。
        /// </summary>
        /// <param name="userRoles">给定的用户角色集合。</param>
        /// <param name="valueFactory">给定的值工厂方法。</param>
        /// <returns>返回一个包含字符串数组的异步任务。</returns>
        protected virtual IEnumerable<TValue> GetRoleNamesAsync<TValue>(IEnumerable<TUserRole> userRoles, Func<TRole, TValue> valueFactory)
        {
            return RoleRepository.Ready(query =>
            {
                var roleIds = userRoles.Select(ur => ur.RoleId);

                return roleIds.Select(id => valueFactory.Invoke(query.Find(id)));
            });
        }

        #endregion

    }
}
