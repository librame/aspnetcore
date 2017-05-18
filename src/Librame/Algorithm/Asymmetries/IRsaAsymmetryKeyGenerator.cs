#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Security.Cryptography;
using System.Text;

namespace Librame.Algorithm.Asymmetries
{
    /// <summary>
    /// RSA 非对称算法密钥生成器接口。
    /// </summary>
    public interface IRsaAsymmetryKeyGenerator : IKeyGenerator
    {
        /// <summary>
        /// 生成 RSA 公钥。
        /// </summary>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        RSAParameters GenerateRsaPublicKey(string publicKeyString = null, Encoding encoding = null);

        /// <summary>
        /// 生成 RSA 私钥。
        /// </summary>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        RSAParameters GenerateRsaPrivateKey(string privateKeyString = null, Encoding encoding = null);
    }
}
