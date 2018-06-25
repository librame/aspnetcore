#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Algorithm;

namespace LibrameCore.Extensions.Authentication.Managers
{
    /// <summary>
    /// 令牌管理器接口。
    /// </summary>
    public interface ITokenManager : IAuthenticationExtensionService
    {
        /// <summary>
        /// 算法选项。
        /// </summary>
        AlgorithmOptions AlgorithmOptions { get; }


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="identity">给定的 Librame 身份标识。</param>
        /// <returns>返回令牌字符串。</returns>
        string Encode(LibrameIdentity identity);


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <returns>返回 Librame 身份标识。</returns>
        LibrameIdentity Decode(string token);
    }
}
