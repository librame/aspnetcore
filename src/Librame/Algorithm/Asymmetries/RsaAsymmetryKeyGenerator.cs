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
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Cryptography;

namespace Librame.Algorithm.Asymmetries
{
    using Codecs;
    using Symmetries;
    using Utility;

    /// <summary>
    /// RSA 非对称算法密钥生成器。
    /// </summary>
    public class RsaAsymmetryKeyGenerator : AbstractKeyGenerator, IRsaAsymmetryKeyGenerator
    {
        private readonly string _defaultPublicKeyString;
        private readonly string _defaultPrivateKeyString;

        /// <summary>
        /// 构造一个 RSA 非对称算法密钥生成器实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plaintext">给定的明文编解码器接口。</param>
        /// <param name="ciphertext">给定的密文编解码器接口。</param>
        public RsaAsymmetryKeyGenerator(ILogger<RsaAsymmetryKeyGenerator> logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plaintext, ICipherTextCodec ciphertext)
            : base(logger, options, plaintext, ciphertext)
        {
            _defaultPublicKeyString = Options.Algorithm.RsaPublicKeyString.NotEmpty(nameof(AlgorithmOptions.RsaPublicKeyString));
            _defaultPrivateKeyString = Options.Algorithm.RsaPrivateKeyString.NotEmpty(nameof(AlgorithmOptions.RsaPrivateKeyString));

            RegenerateKeys();
        }


        private RSA _defaultRsa = null;
        /// <summary>
        /// 重新生成键集合。
        /// </summary>
        public virtual void RegenerateKeys()
        {
            _defaultRsa = RSA.Create();
        }

        /// <summary>
        /// 转换为公钥字符串。
        /// </summary>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <returns>返回公钥字符串。</returns>
        public virtual string ToPublicKeyString(ISymmetryAlgorithm sa)
        {
            sa.NotNull(nameof(sa));

            var parameters = _defaultRsa.ExportParameters(false);
            var str = RsaParametersDescriptor.ToString(parameters, CipherText.GetString);

            return sa.ToAes(str);
        }

        /// <summary>
        /// 转换为私钥字符串。
        /// </summary>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <returns>返回私钥字符串。</returns>
        public virtual string ToPrivateKeyString(ISymmetryAlgorithm sa)
        {
            sa.NotNull(nameof(sa));

            var parameters = _defaultRsa.ExportParameters(true);
            var str = RsaParametersDescriptor.ToString(parameters, CipherText.GetString);

            return sa.ToAes(str);
        }


        /// <summary>
        /// 从字符串还原公钥参数。
        /// </summary>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public virtual RSAParameters FromPublicKeyString(ISymmetryAlgorithm sa, string publicKeyString = null,
            Encoding encoding = null)
        {
            sa.NotNull(nameof(sa));

            var str = publicKeyString.AsOrDefault(_defaultPublicKeyString);
            str = sa.FromAes(str);

            return RsaParametersDescriptor.FromString(str, CipherText.GetBytes);
        }

        /// <summary>
        /// 从字符串还原私钥参数。
        /// </summary>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public virtual RSAParameters FromPrivateKeyString(ISymmetryAlgorithm sa, string privateKeyString = null,
            Encoding encoding = null)
        {
            sa.NotNull(nameof(sa));

            var str = privateKeyString.AsOrDefault(_defaultPrivateKeyString);
            str = sa.FromAes(str);

            return RsaParametersDescriptor.FromString(str, CipherText.GetBytes);
        }

    }
}
