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
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 身份存储标识符。
    /// </summary>
    public class IdentityStoreIdentifier : StoreIdentifierBase
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityStoreIdentifier"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityStoreIdentifier(IClockService clock, ILoggerFactory loggerFactory)
            : base(clock, loggerFactory)
        {
        }


        /// <summary>
        /// 异步获取角色标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public virtual Task<string> GetRoleIdAsync(CancellationToken cancellationToken = default)
            => GenerateCombGuidAsync("RoleId", cancellationToken);

        /// <summary>
        /// 异步获取用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public virtual Task<string> GetUserIdAsync(CancellationToken cancellationToken = default)
            => GenerateCombGuidAsync("UserId", cancellationToken);
    }
}
