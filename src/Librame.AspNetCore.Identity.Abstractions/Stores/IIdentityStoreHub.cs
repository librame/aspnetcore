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
    using Extensions.Data;
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


        /// <summary>
        /// 尝试创建角色集合。
        /// </summary>
        /// <param name="roles">给定的 <typeparamref name="TRole"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TRole[] roles);

        /// <summary>
        /// 尝试创建角色声明集合。
        /// </summary>
        /// <param name="roleClaims">给定的 <typeparamref name="TRoleClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TRoleClaim[] roleClaims);

        /// <summary>
        /// 尝试创建用户集合。
        /// </summary>
        /// <param name="users">给定的 <typeparamref name="TUser"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUser[] users);

        /// <summary>
        /// 尝试创建用户声明集合。
        /// </summary>
        /// <param name="userClaims">给定的 <typeparamref name="TUserClaim"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUserClaim[] userClaims);

        /// <summary>
        /// 尝试创建用户登入集合。
        /// </summary>
        /// <param name="userLogins">给定的 <typeparamref name="TUserLogin"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUserLogin[] userLogins);

        /// <summary>
        /// 尝试创建用户角色集合。
        /// </summary>
        /// <param name="userRoles">给定的 <typeparamref name="TUserRole"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUserRole[] userRoles);

        /// <summary>
        /// 尝试创建用户令牌集合。
        /// </summary>
        /// <param name="userTokens">给定的 <typeparamref name="TUserToken"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TUserToken[] userTokens);
    }
}
