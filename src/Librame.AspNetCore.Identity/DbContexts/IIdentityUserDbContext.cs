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
    using Extensions.Data;

    /// <summary>
    /// 身份用户数据库上下文接口。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public interface IIdentityUserDbContext<TUser> : IIdentityUserDbContext<TUser, string>
        where TUser : IdentityUser<string>
    {
    }


    /// <summary>
    /// 身份用户数据库上下文接口。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public interface IIdentityUserDbContext<TUser, TUserId> : IIdentityUserDbContext<TUser, TUserId, IdentityUserClaim<TUserId>, IdentityUserLogin<TUserId>, IdentityUserToken<TUserId>>
        where TUser : IdentityUser<TUserId>
        where TUserId : IEquatable<TUserId>
    {
    }


    /// <summary>
    /// 身份用户数据库上下文接口。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public interface IIdentityUserDbContext<TUser, TUserId, TUserClaim, TUserLogin, TUserToken> : IDbContext<IdentityBuilderOptions>
        where TUser : IdentityUser<TUserId>
        where TUserId : IEquatable<TUserId>
        where TUserClaim : IdentityUserClaim<TUserId>
        where TUserLogin : IdentityUserLogin<TUserId>
        where TUserToken : IdentityUserToken<TUserId>
    {
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
