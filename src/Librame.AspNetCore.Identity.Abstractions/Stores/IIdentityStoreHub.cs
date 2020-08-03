#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Data.Stores;

    /// <summary>
    /// 身份存储中心接口。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登入类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public interface IIdentityStoreHub<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken> : IStoreHub
        where TRole : class
        where TRoleClaim : class
        where TUser : class
        where TUserClaim : class
        where TUserLogin : class
        where TUserRole : class
        where TUserToken : class
    {
        /// <summary>
        /// 角色查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TRole}"/>。</value>
        IQueryable<TRole> Roles { get; }

        /// <summary>
        /// 角色声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TRoleClaim}"/>。</value>
        IQueryable<TRoleClaim> RoleClaims { get; }

        /// <summary>
        /// 用户查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUser}"/>。</value>
        IQueryable<TUser> Users { get; }

        /// <summary>
        /// 用户声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserClaim}"/>。</value>
        IQueryable<TUserClaim> UserClaims { get; }

        /// <summary>
        /// 用户登入查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserLogin}"/>。</value>
        IQueryable<TUserLogin> UserLogins { get; }

        /// <summary>
        /// 用户角色查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserRole}"/>。</value>
        IQueryable<TUserRole> UserRoles { get; }

        /// <summary>
        /// 用户令牌查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserToken}"/>。</value>
        IQueryable<TUserToken> UserTokens { get; }
    }
}
