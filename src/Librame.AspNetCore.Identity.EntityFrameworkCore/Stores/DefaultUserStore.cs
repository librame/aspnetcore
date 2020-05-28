#region License

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
using System;
using System.Security.Claims;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions;
    using Extensions.Core.Services;
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认用户存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的访问器类型。</typeparam>
    public class DefaultUserStore<TDbContext> : DefaultUserStore<TDbContext, Guid>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 构造一个默认用户存储。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultUserStore(IClockService clock, TDbContext context, IdentityErrorDescriber describer = null)
            : base(clock, context, describer)
        {
        }
    }


    /// <summary>
    /// 默认用户存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class DefaultUserStore<TDbContext, TGenId> : UserStore<DefaultIdentityUser<TGenId>,
        DefaultIdentityRole<TGenId>, TDbContext, TGenId,
        DefaultIdentityUserClaim<TGenId>, DefaultIdentityUserRole<TGenId>, DefaultIdentityUserLogin<TGenId>,
        DefaultIdentityUserToken<TGenId>, DefaultIdentityRoleClaim<TGenId>>
        where TDbContext : DbContext
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 构造一个默认用户存储。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultUserStore(IClockService clock, TDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
            Clock = clock.NotNull(nameof(clock));
        }


        /// <summary>
        /// 时钟服务。
        /// </summary>
        public IClockService Clock { get; }

        /// <summary>
        /// 当前类型名称。
        /// </summary>
        protected string CurrentTypeName
            => EntityPopulator.FormatTypeName(GetType());


        /// <summary>
        /// 创建用户声明。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId}"/>。</param>
        /// <param name="claim">给定的 <see cref="Claim"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserClaim{TGenId}"/>。</returns>
        protected override DefaultIdentityUserClaim<TGenId> CreateUserClaim(DefaultIdentityUser<TGenId> user, Claim claim)
        {
            var userClaim = base.CreateUserClaim(user, claim);

            userClaim.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
            userClaim.CreatedTimeTicks = userClaim.CreatedTime.Ticks;
            userClaim.CreatedBy = CurrentTypeName;

            return userClaim;
        }

        /// <summary>
        /// 创建用户登入。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId}"/>。</param>
        /// <param name="login">给定的 <see cref="UserLoginInfo"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserLogin{TGenId}"/>。</returns>
        protected override DefaultIdentityUserLogin<TGenId> CreateUserLogin(DefaultIdentityUser<TGenId> user, UserLoginInfo login)
        {
            var userLogin = base.CreateUserLogin(user, login);

            userLogin.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
            userLogin.CreatedTimeTicks = userLogin.CreatedTime.Ticks;
            userLogin.CreatedBy = CurrentTypeName;

            return userLogin;
        }

        /// <summary>
        /// 创建用户角色。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId}"/>。</param>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{TGenId}"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserRole{TGenId}"/>。</returns>
        protected override DefaultIdentityUserRole<TGenId> CreateUserRole(DefaultIdentityUser<TGenId> user, DefaultIdentityRole<TGenId> role)
        {
            var userRole = base.CreateUserRole(user, role);

            userRole.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
            userRole.CreatedTimeTicks = userRole.CreatedTime.Ticks;
            userRole.CreatedBy = CurrentTypeName;

            return userRole;
        }

        /// <summary>
        /// 创建用户令牌。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId}"/>。</param>
        /// <param name="loginProvider">给定的登入提供程序。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserToken{TGenId}"/>。</returns>
        protected override DefaultIdentityUserToken<TGenId> CreateUserToken(DefaultIdentityUser<TGenId> user, string loginProvider, string name, string value)
        {
            var userToken = base.CreateUserToken(user, loginProvider, name, value);

            userToken.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
            userToken.CreatedTimeTicks = userToken.CreatedTime.Ticks;
            userToken.CreatedBy = CurrentTypeName;

            return userToken;
        }

    }
}
