#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;

namespace Librame.AspNetCore.Identity.Accessors
{
    using Extensions.Data.Accessors;

    /// <summary>
    /// 身份数据库上下文访问器接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public interface IIdentityDbContextAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken>
        : IAccessor // 接口不强制继承 IDbContextAccessor<TAudit, TAuditProperty, TEntity, TMigration, TTenant>
        where TRole : class
        where TRoleClaim : class
        where TUser : class
        where TUserClaim : class
        where TUserLogin : class
        where TUserRole : class
        where TUserToken : class
    {
        /// <summary>
        /// 角色数据集。
        /// </summary>
        DbSet<TRole> Roles { get; set; }

        /// <summary>
        /// 角色声明数据集。
        /// </summary>
        DbSet<TRoleClaim> RoleClaims { get; set; }


        /// <summary>
        /// 用户数据集。
        /// </summary>
        DbSet<TUser> Users { get; set; }

        /// <summary>
        /// 用户声明数据集。
        /// </summary>
        DbSet<TUserClaim> UserClaims { get; set; }

        /// <summary>
        /// 用户登陆数据集。
        /// </summary>
        DbSet<TUserLogin> UserLogins { get; set; }

        /// <summary>
        /// 用户角色数据集。
        /// </summary>
        DbSet<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// 用户令牌数据集。
        /// </summary>
        DbSet<TUserToken> UserTokens { get; set; }
    }
}
