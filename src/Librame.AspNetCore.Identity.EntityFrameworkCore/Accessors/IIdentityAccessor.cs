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
using System;

namespace Librame.AspNetCore.Identity.Accessors
{
    using AspNetCore.Identity.Stores;
    using Extensions.Data;
    using Extensions.Data.Accessors;

    /// <summary>
    /// 身份访问器接口。
    /// </summary>
    public interface IIdentityAccessor
        : IIdentityAccessor<Guid, int, Guid>
    {
    }


    /// <summary>
    /// 身份访问器接口。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public interface IIdentityAccessor<TGenId, TIncremId, TCreatedBy>
        : IIdentityAccessor<DefaultIdentityRole<TGenId, TCreatedBy>,
            DefaultIdentityUser<TGenId, TCreatedBy>,
            TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
    }


    /// <summary>
    /// 身份访问器接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public interface IIdentityAccessor<TRole, TUser, TGenId, TIncremId, TCreatedBy>
        : IIdentityAccessor<TRole,
            DefaultIdentityRoleClaim<TGenId, TCreatedBy>,
            TUser,
            DefaultIdentityUserClaim<TGenId, TCreatedBy>,
            DefaultIdentityUserLogin<TGenId, TCreatedBy>,
            DefaultIdentityUserRole<TGenId, TCreatedBy>,
            DefaultIdentityUserToken<TGenId, TCreatedBy>>
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
    }


    /// <summary>
    /// 身份访问器接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登入类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public interface IIdentityAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken>
        : IAccessor // 接口不强制继承 IAccessor<TAudit, TAuditProperty, TEntity, TMigration, TTenant>
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


        /// <summary>
        /// 角色数据集管理器。
        /// </summary>
        DbSetManager<TRole> RolesManager { get; }

        /// <summary>
        /// 角色声明数据集管理器。
        /// </summary>
        DbSetManager<TRoleClaim> RoleClaimsManager { get; }

        /// <summary>
        /// 用户数据集管理器。
        /// </summary>
        DbSetManager<TUser> UsersManager { get; }

        /// <summary>
        /// 用户声明数据集管理器。
        /// </summary>
        DbSetManager<TUserClaim> UserClaimsManager { get; }

        /// <summary>
        /// 用户登入数据集管理器。
        /// </summary>
        DbSetManager<TUserLogin> UserLoginsManager { get; }

        /// <summary>
        /// 用户角色数据集管理器。
        /// </summary>
        DbSetManager<TUserRole> UserRolesManager { get; }

        /// <summary>
        /// 用户令牌数据集管理器。
        /// </summary>
        DbSetManager<TUserToken> UserTokensManager { get; }
    }
}
