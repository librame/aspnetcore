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
using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.AspNetCore.Identity
{
    /// <summary>
    /// 身份数据库上下文接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public interface IIdentityDbContext<TRole, TUser> : IIdentityDbContext<TRole, TUser, string>
        where TRole : IdentityRole<string>
        where TUser : IdentityUser<string>
    {
    }


    /// <summary>
    /// 身份数据库上下文接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public interface IIdentityDbContext<TRole, TUser, TId> : IIdentityDbContext<TRole, IdentityRoleClaim<TId>, IdentityUserRole<TId>, TUser, TId, IdentityUserClaim<TId>, IdentityUserLogin<TId>, IdentityUserToken<TId>>
        where TRole : IdentityRole<TId>
        where TUser : IdentityUser<TId>
        where TId : IEquatable<TId>
    {
    }


    /// <summary>
    /// 身份数据库上下文接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public interface IIdentityDbContext<TRole, TRoleClaim, TUserRole, TUser, TId, TUserClaim, TUserLogin, TUserToken> : IIdentityUserDbContext<TUser, TId, TUserClaim, TUserLogin, TUserToken>
        where TRole : IdentityRole<TId>
        where TRoleClaim : IdentityRoleClaim<TId>
        where TUserRole : IdentityUserRole<TId>
        where TUser : IdentityUser<TId>
        where TId : IEquatable<TId>
        where TUserClaim : IdentityUserClaim<TId>
        where TUserLogin : IdentityUserLogin<TId>
        where TUserToken : IdentityUserToken<TId>
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
    }
}
