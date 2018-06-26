#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Algorithm;

namespace LibrameCore.Extensions.Authentication.Managers
{
    /// <summary>
    /// 令牌管理器接口。
    /// </summary>
    public interface ITokenManager : IAuthenticationExtensionService
    {
        /// <summary>
        /// 对称算法。
        /// </summary>
        ISymmetryAlgorithm Symmetry { get; }


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="identity">给定的 <see cref="LibrameClaimsIdentity"/>。</param>
        /// <returns>返回令牌字符串。</returns>
        string Encode(LibrameClaimsIdentity identity);


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <returns>返回 <see cref="LibrameClaimsIdentity"/>。</returns>
        LibrameClaimsIdentity Decode(string token);
    }
}
