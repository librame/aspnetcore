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
        /// 加密字符串。
        /// </summary>
        /// <param name="aa">给定的非对称算法。</param>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回加密字符串。</returns>
        protected virtual string Encrypt(AsymmetricAlgorithm aa, string str)
        {
            //try
            //{
            //    aa.Mode = CipherMode.ECB;
            //    aa.Padding = PaddingMode.PKCS7;

            //    var buffer = EncodeBytes(str);

            //    var ct = aa.CreateEncryptor();
            //    buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);

            //    return ByteConverter.ToString(buffer);
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogWarning(ex.InnerMessage());

            //    return str;
            //}

            return string.Empty;
        }

        /// <summary>
        /// 解密字符串。
        /// </summary>
        /// <param name="aa">给定的非对称算法。</param>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <returns>返回原始字符串。</returns>
        protected virtual string Decrypt(AsymmetricAlgorithm aa, string encrypt)
        {
            //try
            //{
            //    aa.Mode = CipherMode.ECB;
            //    aa.Padding = PaddingMode.PKCS7;

            //    var buffer = ByteConverter.FromString(encrypt);

            //    var ct = aa.CreateDecryptor();
            //    buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);

            //    return DecodeBytes(buffer);
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogWarning(ex.InnerMessage());

            //    return encrypt;
            //}

            return string.Empty;
        }


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

    }
}
