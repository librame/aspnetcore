#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户标识符服务接口。
    /// </summary>
    public interface IPortalIdentifierService : IIdentifierService
    {
        /// <summary>
        /// 异步获取标签标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        Task<string> GetTagIdAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取标签声明标识。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="string"/>。</returns>
        Task<string> GetTagClaimIdAsync(CancellationToken cancellationToken = default);
    }
}
