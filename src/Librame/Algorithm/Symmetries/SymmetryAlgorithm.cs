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

namespace Librame.Algorithm.Symmetries
{
    using Utility;

    /// <summary>
    /// 对称算法。
    /// </summary>
    public class SymmetryAlgorithm : AbstractAlgorithm, ISymmetryAlgorithm
    {
        /// <summary>
        /// 构造一个对称算法实例。
        /// </summary>
        /// <param name="keyGenerator">给定的密钥生成器接口。</param>
        /// <param name="logger">给定的记录器工厂接口。</param>
        /// <param name="options">给定的选择项。</param>
        public SymmetryAlgorithm(ISymmetryAlgorithmKeyGenerator keyGenerator,
            ILogger<SymmetryAlgorithm> logger, IOptions<LibrameOptions> options)
            : base(logger, options)
        {
            KeyGenerator = keyGenerator.NotNull(nameof(keyGenerator));
        }


        /// <summary>
        /// 密钥生成器接口。
        /// </summary>
        public ISymmetryAlgorithmKeyGenerator KeyGenerator { get; }

        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        public ICiphertextCodec ByteConverter => KeyGenerator.ByteConverter;


        /// <summary>
        /// 加密字符串。
        /// </summary>
        /// <param name="sa">给定的对称算法。</param>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回加密字符串。</returns>
        protected virtual string Encrypt(SymmetricAlgorithm sa, string str)
        {
            try
            {
                var buffer = EncodeBytes(str);

                var ct = sa.CreateEncryptor();
                buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);

                return ByteConverter.AsString(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.AsInnerMessage());

                return str;
            }
        }

        /// <summary>
        /// 解密字符串。
        /// </summary>
        /// <param name="sa">给定的对称算法。</param>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <returns>返回原始字符串。</returns>
        protected virtual string Decrypt(SymmetricAlgorithm sa, string encrypt)
        {
            try
            {
                var buffer = ByteConverter.FromString(encrypt);

                var ct = sa.CreateDecryptor();
                buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);

                return DecodeBytes(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.AsInnerMessage());

                return encrypt;
            }
        }


        /// <summary>
        /// 转换为 AES。
        /// </summary>
        /// <param name="str">给定待加密的字符串。</param>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回加密字符串。</returns>
        public virtual string ToAes(string str, string keyString = null)
        {
            var sa = Aes.Create();

            sa.Key = KeyGenerator.GenerateAesKey(keyString);
            sa.IV = KeyGenerator.GenerateAesIv(sa.Key);

            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.PKCS7;

            return Encrypt(sa, str);
        }

        /// <summary>
        /// 还原 AES。
        /// </summary>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回原始字符串。</returns>
        public virtual string FromAes(string encrypt, string keyString = null)
        {
            var sa = Aes.Create();

            sa.Key = KeyGenerator.GenerateAesKey(keyString);
            sa.IV = KeyGenerator.GenerateAesIv(sa.Key);

            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.PKCS7;

            return Decrypt(sa, encrypt);
        }

    }
}
