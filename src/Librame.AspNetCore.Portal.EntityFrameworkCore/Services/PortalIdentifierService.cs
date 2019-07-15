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

namespace Librame.AspNetCore.Portal
{
    using Extensions;
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 门户标识符服务。
    /// </summary>
    public class PortalIdentifierService : IdentifierServiceBase, IPortalIdentifierService
    {
        /// <summary>
        /// 构造一个 <see cref="PortalIdentifierService"/> 实例。
        /// </summary>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public PortalIdentifierService(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }


        /// <summary>
        /// 异步获取标签标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public virtual Task<string> GetTagIdAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                string tagId = UniqueIdentifier.New();
                Logger.LogInformation($"Get TagId: {tagId}");

                return tagId;
            });
        }

        /// <summary>
        /// 异步获取标签声明标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        public virtual Task<string> GetTagClaimIdAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                string tagClaimId = UniqueIdentifier.New();
                Logger.LogInformation($"Get TagClaimId: {tagClaimId}");

                return tagClaimId;
            });
        }

    }
}
