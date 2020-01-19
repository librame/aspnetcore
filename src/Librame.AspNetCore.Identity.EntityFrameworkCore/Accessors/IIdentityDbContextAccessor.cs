#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.AspNetCore.Identity.Accessors
{
    using Extensions.Data.Accessors;
    using Stores;

    /// <summary>
    /// 身份数据库上下文访问器接口。
    /// </summary>
    public interface IIdentityDbContextAccessor : IIdentityDbContextAccessor<DefaultIdentityRole<string>, DefaultIdentityUser<string>, string>
    {
    }


    /// <summary>
    /// 身份数据库上下文访问器接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public interface IIdentityDbContextAccessor<TRole, TUser, TGenId>
        : IIdentityDbContextAccessor<TRole, DefaultIdentityRoleClaim<TGenId>, DefaultIdentityUserRole<TGenId>,
            TUser, DefaultIdentityUserClaim<TGenId>, DefaultIdentityUserLogin<TGenId>, DefaultIdentityUserToken<TGenId>>
        where TRole : class
        where TUser : class
        where TGenId : IEquatable<TGenId>
    {
    }


    /// <summary>
    /// 身份数据库上下文访问器接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public interface IIdentityDbContextAccessor<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken> : IAccessor
        where TRole : class
        where TRoleClaim : class
        where TUserRole : class
        where TUser : class
        where TUserClaim : class
        where TUserLogin : class
        where TUserToken : class
    {
        /// <summary>
        /// 用户角色数据集。
        /// </summary>
        DbSet<TUserRole> UserRoles { get; set; }

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
        /// 用户令牌数据集。
        /// </summary>
        DbSet<TUserToken> UserTokens { get; set; }
    }
}
