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
using System.Security.Cryptography;

namespace Librame.Algorithm.Asymmetries
{
    using Codecs;
    using Symmetries;
    using Utility;

    /// <summary>
    /// RSA 非对称算法。
    /// </summary>
    public class RsaAsymmetryAlgorithm : AbstractAlgorithm, IRsaAsymmetryAlgorithm
    {
        /// <summary>
        /// 构造一个 RSA 非对称算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plainText">给定的明文编解码器接口。</param>
        /// <param name="cipherText">给定的密文编解码器接口。</param>
        /// <param name="keyGenerator">给定的密钥生成器接口。</param>
        public RsaAsymmetryAlgorithm(ILogger<RsaAsymmetryAlgorithm> logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText,
            IRsaAsymmetryKeyGenerator keyGenerator)
            : base(logger, options, plainText, cipherText)
        {
            KeyGenerator = keyGenerator.NotNull(nameof(keyGenerator));
        }


        /// <summary>
        /// 密钥生成器接口。
        /// </summary>
        public IRsaAsymmetryKeyGenerator KeyGenerator { get; }


        /// <summary>
        /// 对指定字符串进行签名。
        /// </summary>
        /// <param name="str">给定要签名的字符串。</param>
        /// <param name="hashName">给定的散列算法名。</param>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="padding">给定的最优非对称签名填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <returns>返回签名后的字符串。</returns>
        public virtual string Sign(string str, HashAlgorithmName hashName, ISymmetryAlgorithm sa,
            RSASignaturePadding padding = null, string privateKeyString = null)
        {
            var buffer = PlainText.GetBytes(str);

            buffer = Sign(buffer, hashName, sa, padding, privateKeyString);

            return CipherText.GetString(buffer);
        }
        /// <summary>
        /// 对指定字节数组进行签名。
        /// </summary>
        /// <param name="bytes">给定要签名的字节数组。</param>
        /// <param name="hashName">给定的散列算法名。</param>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="padding">给定的最优非对称签名填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <returns>返回签名后的字节数组。</returns>
        public virtual byte[] Sign(byte[] bytes, HashAlgorithmName hashName, ISymmetryAlgorithm sa,
            RSASignaturePadding padding = null, string privateKeyString = null)
        {
            var aa = RSA.Create();

            aa.ImportParameters(KeyGenerator.FromPrivateKeyString(sa, privateKeyString,
                    Options.Encoding.AsEncoding()));

            return aa.SignData(bytes, hashName, padding);
        }

        /// <summary>
        /// 验证指定字符串是否已签名。
        /// </summary>
        /// <param name="str">给定未签名的字符串。</param>
        /// <param name="signedString">给定已签名的字符串。</param>
        /// <param name="hashName">给定的散列算法名。</param>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="padding">给定的最优非对称签名填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        public virtual bool Verify(string str, string signedString, HashAlgorithmName hashName, ISymmetryAlgorithm sa,
            RSASignaturePadding padding = null, string publicKeyString = null)
        {
            var buffer = PlainText.GetBytes(str);
            var signedBuffer = CipherText.GetBytes(signedString);

            return Verify(buffer, signedBuffer, hashName, sa, padding, publicKeyString);
        }
        /// <summary>
        /// 验证指定字节数组是否已签名。
        /// </summary>
        /// <param name="bytes">给定未签名的字节数组。</param>
        /// <param name="signedBytes">给定已签名的字节数组。</param>
        /// <param name="hashName">给定的散列算法名。</param>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="padding">给定的最优非对称签名填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        public virtual bool Verify(byte[] bytes, byte[] signedBytes, HashAlgorithmName hashName, ISymmetryAlgorithm sa,
            RSASignaturePadding padding = null, string publicKeyString = null)
        {
            var aa = RSA.Create();

            aa.ImportParameters(KeyGenerator.FromPublicKeyString(sa, publicKeyString,
                    Options.Encoding.AsEncoding()));

            return aa.VerifyData(bytes, signedBytes, hashName, padding);
        }


        /// <summary>
        /// 转换为 RSA。
        /// </summary>
        /// <param name="str">给定待加密的字符串。</param>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="padding">给定的最优非对称加密填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <returns>返回加密字符串。</returns>
        public virtual string ToRsa(ISymmetryAlgorithm sa, string str, RSAEncryptionPadding padding = null,
            string publicKeyString = null)
        {
            try
            {
                var buffer = PlainText.GetBytes(str);

                var aa = RSA.Create();
                
                aa.ImportParameters(KeyGenerator.FromPublicKeyString(sa, publicKeyString,
                    Options.Encoding.AsEncoding()));

                aa.Encrypt(buffer, padding.AsOrDefault(RSAEncryptionPadding.Pkcs1));
                
                return CipherText.GetString(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.AsInnerMessage());

                return str;
            }
        }

        /// <summary>
        /// 还原 RSA。
        /// </summary>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <param name="sa">给定的对称算法接口。</param>
        /// <param name="padding">给定的最优非对称加密填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <returns>返回原始字符串。</returns>
        public virtual string FromRsa(ISymmetryAlgorithm sa, string encrypt, RSAEncryptionPadding padding = null,
            string privateKeyString = null)
        {
            try
            {
                var buffer = CipherText.GetBytes(encrypt);
                
                var aa = RSA.Create();

                aa.ImportParameters(KeyGenerator.FromPrivateKeyString(sa, privateKeyString,
                    Options.Encoding.AsEncoding()));

                aa.Decrypt(buffer, padding.AsOrDefault(RSAEncryptionPadding.Pkcs1));

                return PlainText.GetString(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.AsInnerMessage());

                return encrypt;
            }
        }

    }
}
