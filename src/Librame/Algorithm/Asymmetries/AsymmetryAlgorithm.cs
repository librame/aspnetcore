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
    using Utility;

    /// <summary>
    /// 非对称算法。
    /// </summary>
    public class AsymmetryAlgorithm : AbstractByteCodec, IAsymmetryAlgorithm
    {
        /// <summary>
        /// 构造一个对称算法实例。
        /// </summary>
        /// <param name="keyGenerator">给定的密钥生成器接口。</param>
        /// <param name="logger">给定的记录器工厂接口。</param>
        /// <param name="options">给定的选择项。</param>
        public AsymmetryAlgorithm(IAsymmetryAlgorithmKeyGenerator keyGenerator,
            ILogger<AsymmetryAlgorithm> logger, IOptions<LibrameOptions> options)
            : base(logger, options)
        {
            KeyGenerator = keyGenerator.NotNull(nameof(keyGenerator));
        }


        /// <summary>
        /// 密钥生成器接口。
        /// </summary>
        public IAsymmetryAlgorithmKeyGenerator KeyGenerator { get; }

        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        public IByteConverter ByteConverter => KeyGenerator.ByteConverter;
        
        
        /// <summary>
        /// 对指定字节数组进行签名。
        /// </summary>
        /// <param name="privateKey">给定的私钥。</param>
        /// <param name="bytes">给定要签名的字节数组。</param>
        /// <returns>返回签名后的字节数组。</returns>
        public virtual byte[] Signature(string privateKey, byte[] bytes)
        {
            //RSA.Create();

            //RSACryptoServiceProvider rsp = new RSACryptoServiceProvider();
            //rsp.FromXmlString(strPrivateKey);
            //RSAPKCS1SignatureFormatter rf = new RSAPKCS1SignatureFormatter(rsp);
            //rf.SetHashAlgorithm("MD5");
            //byte[] signature = rf.CreateSignature(hv);
            //return Convert.ToBase64String(signature);

            return null;
        }


        /// <summary>
        /// 转换为 RSA。
        /// </summary>
        /// <param name="str">给定待加密的字符串。</param>
        /// <param name="padding">给定的最优非对称加密填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <returns>返回加密字符串。</returns>
        public virtual string ToRsa(string str, RSAEncryptionPadding padding = null,
            string publicKeyString = null)
        {
            try
            {
                var buffer = EncodeBytes(str);

                var aa = RSA.Create();

                aa.ImportParameters(KeyGenerator.GenerateRsaPublicKey(publicKeyString,
                    Options.Encoding.AsEncoding()));

                aa.Encrypt(buffer, padding.As(RSAEncryptionPadding.Pkcs1));
                
                return ByteConverter.AsString(buffer);
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
        /// <param name="padding">给定的最优非对称加密填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <returns>返回原始字符串。</returns>
        public virtual string FromRsa(string encrypt, RSAEncryptionPadding padding = null,
            string privateKeyString = null)
        {
            try
            {
                var buffer = ByteConverter.FromString(encrypt);

                var aa = RSA.Create();

                aa.ImportParameters(KeyGenerator.GenerateRsaPrivateKey(privateKeyString,
                    Options.Encoding.AsEncoding()));

                aa.Decrypt(buffer, padding.As(RSAEncryptionPadding.Pkcs1));

                return DecodeBytes(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.AsInnerMessage());

                return encrypt;
            }
        }

    }
}
