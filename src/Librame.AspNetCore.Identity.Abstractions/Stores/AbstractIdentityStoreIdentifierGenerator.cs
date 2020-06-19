﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Core.Identifiers;
    using Extensions.Core.Services;
    using Extensions.Data.Stores;

    /// <summary>
    /// 抽象身份存储标识符生成器。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public abstract class AbstractIdentityStoreIdentifierGenerator<TId>
        : AbstractDataStoreIdentifierGenerator<TId>, IIdentityStoreIdentifierGenerator<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractIdentityStoreIdentifierGenerator{TId}"/>。
        /// </summary>
        /// <param name="generator">给定的 <see cref="IIdentifierGenerator{TId}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractIdentityStoreIdentifierGenerator(IClockService clock,
            IIdentifierGenerator<TId> generator, ILoggerFactory loggerFactory)
            : base(clock, generator, loggerFactory)
        {
        }


        /// <summary>
        /// 异步生成角色标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateRoleIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync("RoleId", cancellationToken);

        /// <summary>
        /// 异步生成用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TId"/> 的异步操作。</returns>
        public virtual Task<TId> GenerateUserIdAsync(CancellationToken cancellationToken = default)
            => GenerateIdAsync("UserId", cancellationToken);
    }
}