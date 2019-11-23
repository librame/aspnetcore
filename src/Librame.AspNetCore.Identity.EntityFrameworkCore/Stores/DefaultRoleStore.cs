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

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 默认角色存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class DefaultRoleStore<TAccessor> : RoleStore<DefaultIdentityRole<string>,
        TAccessor, string,
        DefaultIdentityUserRole<string>, DefaultIdentityRoleClaim<string>>
        where TAccessor : DbContext
    {
        /// <summary>
        /// 构造一个默认角色存储。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultRoleStore(IClockService clock, TAccessor accessor, IdentityErrorDescriber describer = null)
            : base(accessor, describer)
        {
            Clock = clock.NotNull(nameof(clock));
        }


        /// <summary>
        /// 时钟服务。
        /// </summary>
        protected IClockService Clock { get; }


        /// <summary>
        /// 创建身份角色。
        /// </summary>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{String}"/>。</param>
        /// <param name="claim">给定的 <see cref="Claim"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityRoleClaim{String}"/>。</returns>
        protected override DefaultIdentityRoleClaim<string> CreateRoleClaim(DefaultIdentityRole<string> role, Claim claim)
        {
            var roleClaim = base.CreateRoleClaim(role, claim);
            roleClaim.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
            roleClaim.CreatedBy = GetType().GetSimpleFullName();

            return roleClaim;
        }

    }
}
