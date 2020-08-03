#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Linq;

namespace Librame.AspNetCore.Identity.Stores
{
    using AspNetCore.Identity.Accessors;
    using Extensions.Data.Accessors;
    using Extensions.Data.Stores;

    /// <summary>
    /// 身份存储中心。
    /// </summary>
    public class IdentityStoreHub : IdentityStoreHub<IdentityDbContextAccessor>
    {
        /// <summary>
        /// 构造一个身份存储中心。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public IdentityStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }

    }


    /// <summary>
    /// 身份存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class IdentityStoreHub<TAccessor> : IdentityStoreHub<TAccessor,
        Guid, int, Guid>
        where TAccessor : class, IIdentityAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个身份存储中心。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public IdentityStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }

    }


    /// <summary>
    /// 身份存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityStoreHub<TAccessor, TGenId, TIncremId, TCreatedBy>
        : IdentityStoreHub<TAccessor,
            DefaultIdentityRole<TGenId, TCreatedBy>,
            DefaultIdentityRoleClaim<TGenId, TCreatedBy>,
            DefaultIdentityUser<TGenId, TCreatedBy>,
            DefaultIdentityUserClaim<TGenId, TCreatedBy>,
            DefaultIdentityUserLogin<TGenId, TCreatedBy>,
            DefaultIdentityUserRole<TGenId, TCreatedBy>,
            DefaultIdentityUserToken<TGenId, TCreatedBy>,
            TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IIdentityAccessor<TGenId, TIncremId, TCreatedBy>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份存储中心。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        protected IdentityStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }

    }


    /// <summary>
    /// 身份存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityStoreHub<TAccessor, TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken, TGenId, TIncremId, TCreatedBy>
        : DataStoreHub<TAccessor, TGenId, TIncremId, TCreatedBy>,
        IIdentityStoreHub<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken>
        where TAccessor : class, IIdentityAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TRoleClaim : DefaultIdentityRoleClaim<TGenId, TCreatedBy>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TUserClaim : DefaultIdentityUserClaim<TGenId, TCreatedBy>
        where TUserLogin : DefaultIdentityUserLogin<TGenId, TCreatedBy>
        where TUserRole : DefaultIdentityUserRole<TGenId, TCreatedBy>
        where TUserToken : DefaultIdentityUserToken<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份存储中心。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        protected IdentityStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }


        /// <summary>
        /// 角色查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TRole}"/>。</value>
        public IQueryable<TRole> Roles
            => Accessor.Roles;

        /// <summary>
        /// 角色声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TRoleClaim}"/>。</value>
        public IQueryable<TRoleClaim> RoleClaims
            => Accessor.RoleClaims;

        /// <summary>
        /// 用户查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUser}"/>。</value>
        public IQueryable<TUser> Users
            => Accessor.Users;

        /// <summary>
        /// 用户声明查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserClaim}"/>。</value>
        public IQueryable<TUserClaim> UserClaims
            => Accessor.UserClaims;

        /// <summary>
        /// 用户登入查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserLogin}"/>。</value>
        public IQueryable<TUserLogin> UserLogins
            => Accessor.UserLogins;

        /// <summary>
        /// 用户角色查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserRole}"/>。</value>
        public IQueryable<TUserRole> UserRoles
            => Accessor.UserRoles;

        /// <summary>
        /// 用户令牌查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TUserToken}"/>。</value>
        public IQueryable<TUserToken> UserTokens
            => Accessor.UserTokens;
    }
}
