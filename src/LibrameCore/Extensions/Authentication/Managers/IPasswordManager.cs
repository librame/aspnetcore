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
    /// 密码管理器接口。
    /// </summary>
    public interface IPasswordManager : IAuthenticationExtensionService
    {
        /// <summary>
        /// 哈希算法。
        /// </summary>
        IHashAlgorithm Hash { get; }

        /// <summary>
        /// 对称算法。
        /// </summary>
        ISymmetryAlgorithm Symmetry { get; }


        /// <summary>
        /// 编码密码。
        /// </summary>
        /// <param name="original">给定的原始密码。</param>
        /// <returns>返回经过编码的密码字符串。</returns>
        string Encode(string original);


        /// <summary>
        /// 验证密码。
        /// </summary>
        /// <param name="encode">给定的编码密码。</param>
        /// <param name="original">给定的原始密码。</param>
        /// <returns>返回是否有效的布尔值。</returns>
        bool Validate(string encode, string original);
    }
}
