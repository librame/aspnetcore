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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

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
        }

        
        /// <summary>
        /// 对称算法接口。
        /// </summary>
        public ISymmetryAlgorithm Symmetry { get; }


        /// <summary>
        /// 生成公私钥参数。
        /// </summary>
        /// <returns>返回参数。</returns>
        public virtual RSAParameters GenerateParameters()
        {
            return RSA.Create().ExportParameters(true);
        }


        /// <summary>
        /// 将公私钥参数转换为字符串类型的键值对。
        /// </summary>
        /// <param name="parameters">给定的公私钥参数。</param>
        /// <returns>返回键值对字符串。</returns>
        public virtual KeyValuePair<string, string> ToParametersPairString(RSAParameters parameters)
        {
            var key = ToPublicKeyString(parameters);
            var value = ToPrivateKeyString(parameters);

            return new KeyValuePair<string, string>(key, value);
        }

        /// <summary>
        /// 转换为公钥字符串。
        /// </summary>
        /// <param name="parameters">给定的公钥或私钥参数。</param>
        /// <returns>返回公钥字符串。</returns>
        protected virtual string ToPublicKeyString(RSAParameters parameters)
        {
            if (parameters.Exponent == null || parameters.Modulus == null)
                throw new ArgumentException("Invalid public or private key parameters.");
            
            var str = RsaParametersDescriptor.ToString(new RSAParameters()
            {
                Exponent = parameters.Exponent,
                Modulus = parameters.Modulus
            },
            Cipher.GetString);

            return Symmetry.ToAes(str);
        }

        /// <summary>
        /// 转换为私钥字符串。
        /// </summary>
        /// <param name="parameters">给定的私钥参数。</param>
        /// <returns>返回私钥字符串。</returns>
        protected virtual string ToPrivateKeyString(RSAParameters parameters)
        {
            var pis = typeof(RSAParameters).GetTypeInfo().GetProperties();
            var values = pis.Select(pi => pi.GetValue(parameters));

            foreach (var v in values)
            {
                if (v == null)
                    throw new ArgumentException("Invalid private key parameters.");
            }
            
            var str = RsaParametersDescriptor.ToString(parameters, Cipher.GetString);

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

            return RsaParametersDescriptor.FromString(str, Cipher.GetBytes);
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

            return RsaParametersDescriptor.FromString(str, Cipher.GetBytes);
        }

    }
}
