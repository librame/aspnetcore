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
    using TextCodecs;
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
        /// <param name="symmetry">给定的对称算法接口。</param>
        public RsaAsymmetryKeyGenerator(ILogger<RsaAsymmetryKeyGenerator> logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plaintext, ICipherTextCodec ciphertext, ISymmetryAlgorithm symmetry)
            : base(logger, options, plaintext, ciphertext)
        {
            _defaultPublicKeyString = Options.Algorithm.RsaPublicKeyString.NotEmpty(nameof(AlgorithmOptions.RsaPublicKeyString));
            _defaultPrivateKeyString = Options.Algorithm.RsaPrivateKeyString.NotEmpty(nameof(AlgorithmOptions.RsaPrivateKeyString));

            Symmetry = symmetry.NotNull(nameof(symmetry));

            // 初始化 RSA 实例
            RegenerateRsa();
        }

        
        /// <summary>
        /// 对称算法接口。
        /// </summary>
        public ISymmetryAlgorithm Symmetry { get; }

        /// <summary>
        /// 默认 RSA 实例。
        /// </summary>
        protected RSA DefaultRsa = null;


        /// <summary>
        /// 重新生成 RSA 实例。
        /// </summary>
        public virtual void RegenerateRsa()
        {
            DefaultRsa = RSA.Create();
        }

        /// <summary>
        /// 转换为公钥字符串。
        /// </summary>
        /// <returns>返回公钥字符串。</returns>
        public virtual string ToPublicKeyString()
        {
            var parameters = DefaultRsa.ExportParameters(false);
            var str = RsaParametersDescriptor.ToString(parameters, CipherText.GetString);

            return Symmetry.ToAes(str);
        }

        /// <summary>
        /// 转换为私钥字符串。
        /// </summary>
        /// <returns>返回私钥字符串。</returns>
        public virtual string ToPrivateKeyString()
        {
            var parameters = DefaultRsa.ExportParameters(true);
            var str = RsaParametersDescriptor.ToString(parameters, CipherText.GetString);

            return Symmetry.ToAes(str);
        }


        /// <summary>
        /// 从字符串还原公钥参数。
        /// </summary>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public virtual RSAParameters FromPublicKeyString(string publicKeyString = null, Encoding encoding = null)
        {
            var str = publicKeyString.AsOrDefault(_defaultPublicKeyString);
            str = Symmetry.FromAes(str);

            return RsaParametersDescriptor.FromString(str, CipherText.GetBytes);
        }

        /// <summary>
        /// 从字符串还原私钥参数。
        /// </summary>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        /// <returns>返回参数。</returns>
        public virtual RSAParameters FromPrivateKeyString(string privateKeyString = null, Encoding encoding = null)
        {
            var str = privateKeyString.AsOrDefault(_defaultPrivateKeyString);
            str = Symmetry.FromAes(str);

            return RsaParametersDescriptor.FromString(str, CipherText.GetBytes);
        }

    }
}
