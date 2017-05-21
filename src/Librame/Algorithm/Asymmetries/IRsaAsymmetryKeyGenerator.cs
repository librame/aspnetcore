#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Librame.Algorithm.Asymmetries
{
    using Symmetries;

    /// <summary>
    /// RSA 非对称算法密钥生成器接口。
    /// </summary>
    public interface IRsaAsymmetryKeyGenerator : IKeyGenerator
    {
        /// <summary>
        /// 对称算法接口。
        /// </summary>
        ISymmetryAlgorithm Symmetry { get; }


        /// <summary>
        /// 生成公私钥参数。
        /// </summary>
        /// <returns>返回参数。</returns>
        RSAParameters GenerateParameters();


        /// <summary>
        /// 将公私钥参数转换为字符串类型的键值对。
        /// </summary>
        /// <param name="parameters">给定的公私钥参数。</param>
        /// <returns>返回键值对字符串。</returns>
        KeyValuePair<string, string> ToParametersPairString(RSAParameters parameters);


        /// <summary>
        /// 从字符串还原公钥参数。
        /// </summary>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回公钥参数。</returns>
        RSAParameters FromPublicKeyString(string publicKeyString = null, Encoding encoding = null);

        /// <summary>
        /// 从字符串还原私钥参数。
        /// </summary>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回私钥参数。</returns>
        RSAParameters FromPrivateKeyString(string privateKeyString = null, Encoding encoding = null);
    }
}
