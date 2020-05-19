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
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认角色存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    public class DefaultRoleStore<TDbContext> : DefaultRoleStore<TDbContext, Guid>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 构造一个默认角色存储。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultRoleStore(IClockService clock, TDbContext context, IdentityErrorDescriber describer = null)
            : base(clock, context, describer)
        {
        }
    }


    /// <summary>
    /// 默认角色存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class DefaultRoleStore<TDbContext, TGenId> : RoleStore<DefaultIdentityRole<TGenId>,
        TDbContext, TGenId,
        DefaultIdentityUserRole<TGenId>, DefaultIdentityRoleClaim<TGenId>>
        where TDbContext : DbContext
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 构造一个默认角色存储。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultRoleStore(IClockService clock, TDbContext context, IdentityErrorDescriber describer = null)
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
        /// 创建身份角色。
        /// </summary>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{TGenId}"/>。</param>
        /// <param name="claim">给定的 <see cref="Claim"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityRoleClaim{TGenId}"/>。</returns>
        protected override DefaultIdentityRoleClaim<TGenId> CreateRoleClaim(DefaultIdentityRole<TGenId> role, Claim claim)
        {
            var roleClaim = base.CreateRoleClaim(role, claim);

            roleClaim.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
            roleClaim.CreatedTimeTicks = roleClaim.CreatedTime.Ticks;
            roleClaim.CreatedBy = CurrentTypeName;

            return roleClaim;
        }

    }
}
