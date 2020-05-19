#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Core.Services;
    using Extensions.Data.Builders;
    using Extensions.Data.Stores;

    /// <summary>
    /// GUID 身份存储标识符生成器。
    /// </summary>
    public class GuidIdentityStoreIdentifierGenerator : GuidStoreIdentifierGenerator, IIdentityStoreIdentifierGenerator<Guid>
    {
        /// <summary>
        /// 构造一个 <see cref="GuidIdentityStoreIdentifierGenerator"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{DataBuilderOptions}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityStoreIdentifierGenerator(IOptions<DataBuilderOptions> options,
            IClockService clock, ILoggerFactory loggerFactory)
            : base(options, clock, loggerFactory)
        {
        }


        /// <summary>
        /// 异步生成角色标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="Guid"/> 的异步操作。</returns>
        public virtual Task<Guid> GenerateRoleIdAsync(CancellationToken cancellationToken = default)
            => GenerateGenericIdAsync("RoleId", cancellationToken);

        /// <summary>
        /// 异步生成用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="Guid"/> 的异步操作。</returns>
        public virtual Task<Guid> GenerateUserIdAsync(CancellationToken cancellationToken = default)
            => GenerateGenericIdAsync("UserId", cancellationToken);


        Task<object> IIdentityStoreIdentifierGenerator.GenerateRoleIdAsync(CancellationToken cancellationToken)
            => GenerateIdAsync("RoleId", cancellationToken);

        Task<object> IIdentityStoreIdentifierGenerator.GenerateUserIdAsync(CancellationToken cancellationToken)
            => GenerateIdAsync("UserId", cancellationToken);
    }
}
