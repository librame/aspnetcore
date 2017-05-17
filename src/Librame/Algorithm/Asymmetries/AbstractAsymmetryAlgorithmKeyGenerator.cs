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
using System.Security.Cryptography;
using System.Text;

namespace Librame.Algorithm.Asymmetries
{
    using Utility;

    /// <summary>
    /// 抽象非对称算法密钥生成器。
    /// </summary>
    public abstract class AbstractAsymmetryAlgorithmKeyGenerator : IAsymmetryAlgorithmKeyGenerator
    {
        /// <summary>
        /// 构造一个抽象非对称算法密钥生成器实例。
        /// </summary>
        /// <param name="byteConverter">给定的字节转换器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public AbstractAsymmetryAlgorithmKeyGenerator(ICiphertextCodec byteConverter, ILogger logger)
        {
            ByteConverter = byteConverter.NotNull(nameof(byteConverter));
            Logger = logger.NotNull(nameof(logger));
        }


        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        public ICiphertextCodec ByteConverter { get; }

        /// <summary>
        /// 记录器接口。
        /// </summary>
        public ILogger Logger { get; }


        /// <summary>
        /// 生成 RSA 公钥。
        /// </summary>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public abstract RSAParameters GenerateRsaPublicKey(string publicKeyString = null,
            Encoding encoding = null);

        /// <summary>
        /// 生成 RSA 私钥。
        /// </summary>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public abstract RSAParameters GenerateRsaPrivateKey(string privateKeyString = null,
            Encoding encoding = null);
    }
}
