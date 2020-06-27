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
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Security.Claims;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Core.Services;
    using Extensions.Data.Stores;

    /// <summary>
    /// 默认角色存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    public class DefaultRoleStore<TDbContext> : DefaultRoleStore<TDbContext, Guid, Guid>
        where TDbContext : DbContext
    {
        /// <summary>
        /// 构造一个默认角色存储。
        /// </summary>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultRoleStore(TDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

    }


    /// <summary>
    /// 默认角色存储。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class DefaultRoleStore<TDbContext, TGenId, TCreatedBy> : RoleStore<DefaultIdentityRole<TGenId, TCreatedBy>,
        TDbContext, TGenId,
        DefaultIdentityUserRole<TGenId, TCreatedBy>, DefaultIdentityRoleClaim<TGenId, TCreatedBy>>
        where TDbContext : DbContext
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个默认角色存储。
        /// </summary>
        /// <param name="context">给定的 <typeparamref name="TDbContext"/>。</param>
        /// <param name="describer">给定的 <see cref="IdentityErrorDescriber"/>（可选）。</param>
        public DefaultRoleStore(TDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
            Clock = context.GetService<IClockService>();
        }


        /// <summary>
        /// 时钟服务。
        /// </summary>
        protected IClockService Clock { get; }


        /// <summary>
        /// 创建身份角色。
        /// </summary>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{TGenId, TCreatedBy}"/>。</param>
        /// <param name="claim">给定的 <see cref="Claim"/>。</param>
        /// <returns>返回 <see cref="DefaultIdentityRoleClaim{TGenId, TCreatedBy}"/>。</returns>
        protected override DefaultIdentityRoleClaim<TGenId, TCreatedBy> CreateRoleClaim
            (DefaultIdentityRole<TGenId, TCreatedBy> role, Claim claim)
        {
            var roleClaim = base.CreateRoleClaim(role, claim);

            roleClaim.PopulateCreationAsync(Clock);

            return roleClaim;
        }

    }
}
