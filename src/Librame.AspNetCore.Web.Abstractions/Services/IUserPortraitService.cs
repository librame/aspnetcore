#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Web.Services
{
    /// <summary>
    /// 用户头像服务接口。
    /// </summary>
    public interface IUserPortraitService
    {
        /// <summary>
        /// 获取头像路径。
        /// </summary>
        /// <param name="user">给定的用户。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回头像路径。</returns>
        Task<string> GetPortraitPathAsync(dynamic user, CancellationToken cancellationToken = default);
    }
}
