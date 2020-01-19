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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 默认用户存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class DefaultUserStore<TAccessor> : UserStore<DefaultIdentityUser<string>,
        DefaultIdentityRole<string>, TAccessor, string,
        DefaultIdentityUserClaim<string>, DefaultIdentityUserRole<string>, DefaultIdentityUserLogin<string>,
        DefaultIdentityUserToken<string>, DefaultIdentityRoleClaim<string>>
        where TAccessor : DbContext
    {
        /// <summary>
        /// 构造一个默认用户存储。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultUserStore(IClockService clock, TAccessor accessor, IdentityErrorDescriber describer = null)
            : base(accessor, describer)
        {
            Clock = clock.NotNull(nameof(clock));
        }


        /// <summary>
        /// 时钟服务。
        /// </summary>
        protected IClockService Clock { get; }


        /// <summary>
        /// 创建用户声明。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{String}"/>。</param>
        /// <param name="claim">给定的 <see cref="Claim"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserClaim{String}"/>。</returns>
        protected override DefaultIdentityUserClaim<string> CreateUserClaim(DefaultIdentityUser<string> user, Claim claim)
        {
            var userClaim = base.CreateUserClaim(user, claim);
            userClaim.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
            userClaim.CreatedBy = GetType().GetDisplayNameWithNamespace();

            return userClaim;
        }

        /// <summary>
        /// 创建用户登入。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{String}"/>。</param>
        /// <param name="login">给定的 <see cref="UserLoginInfo"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserLogin{String}"/>。</returns>
        protected override DefaultIdentityUserLogin<string> CreateUserLogin(DefaultIdentityUser<string> user, UserLoginInfo login)
        {
            var userLogin = base.CreateUserLogin(user, login);
            userLogin.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
            userLogin.CreatedBy = GetType().GetDisplayNameWithNamespace();

            return userLogin;
        }

        /// <summary>
        /// 创建用户角色。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{String}"/>。</param>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{String}"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserRole{String}"/>。</returns>
        protected override DefaultIdentityUserRole<string> CreateUserRole(DefaultIdentityUser<string> user, DefaultIdentityRole<string> role)
        {
            var userRole = base.CreateUserRole(user, role);
            userRole.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
            userRole.CreatedBy = GetType().GetDisplayNameWithNamespace();

            return userRole;
        }

        /// <summary>
        /// 创建用户令牌。
        /// </summary>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{String}"/>。</param>
        /// <param name="loginProvider">给定的登入提供程序。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回 <see cref="DefaultIdentityUserToken{String}"/>。</returns>
        protected override DefaultIdentityUserToken<string> CreateUserToken(DefaultIdentityUser<string> user, string loginProvider, string name, string value)
        {
            var userToken = base.CreateUserToken(user, loginProvider, name, value);
            userToken.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
            userToken.CreatedBy = GetType().GetDisplayNameWithNamespace();

            return userToken;
        }

    }
}
