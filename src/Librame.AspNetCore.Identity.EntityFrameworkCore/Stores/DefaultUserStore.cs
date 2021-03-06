﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Security.Claims;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Core.Services;
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认用户存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的访问器类型。</typeparam>
    public class DefaultUserStore<TDbContext> : DefaultUserStore<TDbContext, Guid, Guid>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 构造一个默认用户存储。
        /// </summary>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultUserStore(TDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

    }


    /// <summary>
    /// 默认用户存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class DefaultUserStore<TDbContext, TGenId, TCreatedBy> : UserStore<DefaultIdentityUser<TGenId, TCreatedBy>,
        DefaultIdentityRole<TGenId, TCreatedBy>,
        TDbContext, TGenId,
        DefaultIdentityUserClaim<TGenId, TCreatedBy>,
        DefaultIdentityUserRole<TGenId, TCreatedBy>,
        DefaultIdentityUserLogin<TGenId, TCreatedBy>,
        DefaultIdentityUserToken<TGenId, TCreatedBy>,
        DefaultIdentityRoleClaim<TGenId, TCreatedBy>>
        where TDbContext : DbContext
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个默认用户存储。
        /// </summary>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultUserStore(TDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
            Clock = context.GetService<IClockService>();
        }


        /// <summary>
        /// 时钟服务。
        /// </summary>
        public IClockService Clock { get; }


        /// <summary>
        /// 创建用户声明。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId, TCreatedBy}"/>。</param>
        /// <param name="claim">给定的 <see cref="Claim"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserClaim{TGenId, TCreatedBy}"/>。</returns>
        protected override DefaultIdentityUserClaim<TGenId, TCreatedBy> CreateUserClaim
            (DefaultIdentityUser<TGenId, TCreatedBy> user, Claim claim)
        {
            var userClaim = base.CreateUserClaim(user, claim);

            userClaim.PopulateCreation(Clock);

            return userClaim;
        }

        /// <summary>
        /// 创建用户登入。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId, TCreatedBy}"/>。</param>
        /// <param name="login">给定的 <see cref="UserLoginInfo"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserLogin{TGenId, TCreatedBy}"/>。</returns>
        protected override DefaultIdentityUserLogin<TGenId, TCreatedBy> CreateUserLogin
            (DefaultIdentityUser<TGenId, TCreatedBy> user, UserLoginInfo login)
        {
            var userLogin = base.CreateUserLogin(user, login);

            userLogin.PopulateCreation(Clock);

            return userLogin;
        }

        /// <summary>
        /// 创建用户角色。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId, TCreatedBy}"/>。</param>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{TGenId, TCreatedBy}"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserRole{TGenId, TCreatedBy}"/>。</returns>
        protected override DefaultIdentityUserRole<TGenId, TCreatedBy> CreateUserRole
            (DefaultIdentityUser<TGenId, TCreatedBy> user, DefaultIdentityRole<TGenId, TCreatedBy> role)
        {
            var userRole = base.CreateUserRole(user, role);

            userRole.PopulateCreation(Clock);

            return userRole;
        }

        /// <summary>
        /// 创建用户令牌。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId, TCreatedBy}"/>。</param>
        /// <param name="loginProvider">给定的登入提供程序。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserToken{TGenId, TCreatedBy}"/>。</returns>
        protected override DefaultIdentityUserToken<TGenId, TCreatedBy> CreateUserToken
            (DefaultIdentityUser<TGenId, TCreatedBy> user, string loginProvider, string name, string value)
        {
            var userToken = base.CreateUserToken(user, loginProvider, name, value);

            userToken.PopulateCreation(Clock);

            return userToken;
        }

    }
}
