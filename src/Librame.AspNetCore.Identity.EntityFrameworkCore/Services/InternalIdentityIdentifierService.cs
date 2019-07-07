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
    using Extensions;
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 内部身份标识符服务。
    /// </summary>
    internal class InternalIdentityIdentifierService : IdentifierServiceBase, IIdentityIdentifierService
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityIdentifierService"/> 实例。
        /// </summary>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public InternalIdentityIdentifierService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }


        /// <summary>
        /// 异步获取角色标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public virtual Task<string> GetRoleIdAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                string roleId = GuIdentifier.New();
                Logger.LogInformation($"Get RoleId: {roleId}");

                return roleId;
            });
        }

        /// <summary>
        /// 异步获取用户标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public virtual Task<string> GetUserIdAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                string userId = GuIdentifier.New();
                Logger.LogInformation($"Get UserId: {userId}");

                return userId;
            });
        }

    }
}
