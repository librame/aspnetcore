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
    using TextCodecs;
    using Utility;

    /// <summary>
    /// 对称算法。
    /// </summary>
    public class SymmetryAlgorithm : AbstractAlgorithm, ISymmetryAlgorithm
    {
        /// <summary>
        /// 构造一个对称算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plainText">给定的明文编解码器接口。</param>
        /// <param name="cipherText">给定的密文编解码器接口。</param>
        /// <param name="keyGenerator">给定的密钥生成器接口。</param>
        public SymmetryAlgorithm(ILogger<SymmetricAlgorithm> logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText,
            ISymmetryKeyGenerator keyGenerator)
            : base(logger, options, plainText, cipherText)
        {
            KeyGenerator = keyGenerator.NotNull(nameof(keyGenerator));
        }


        /// <summary>
        /// 密钥生成器接口。
        /// </summary>
        public ISymmetryKeyGenerator KeyGenerator { get; }


        /// <summary>
        /// 加密字符串。
        /// </summary>
        /// <param name="sym">给定的对称算法。</param>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回加密字符串。</returns>
        protected virtual string Encrypt(SymmetricAlgorithm sym, string str)
        {
            var buffer = Plain.GetBytes(str);

            try
            {
                var ct = sym.CreateEncryptor();

                buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.AsInnerMessage());

                throw ex;
            }

            return Cipher.GetString(buffer);
        }

        /// <summary>
        /// 解密字符串。
        /// </summary>
        /// <param name="sym">给定的对称算法。</param>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <returns>返回原始字符串。</returns>
        protected virtual string Decrypt(SymmetricAlgorithm sym, string encrypt)
        {
            var buffer = Cipher.GetBytes(encrypt);

            try
            {
                var ct = sym.CreateDecryptor();

                buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.AsInnerMessage());

                throw ex;
            }

            return Plain.GetString(buffer);
        }


        /// <summary>
        /// 转换为 AES。
        /// </summary>
        /// <param name="str">给定待加密的字符串。</param>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回加密字符串。</returns>
        public virtual string ToAes(string str, string keyString = null)
        {
            var sym = Aes.Create();

            sym.Key = KeyGenerator.GenerateAesKey(keyString);
            sym.IV = KeyGenerator.GenerateAesIv(sym.Key);

            sym.Mode = CipherMode.ECB;
            sym.Padding = PaddingMode.PKCS7;

            return Encrypt(sym, str);
        }

        /// <summary>
        /// 还原 AES。
        /// </summary>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回原始字符串。</returns>
        public virtual string FromAes(string encrypt, string keyString = null)
        {
            var sym = Aes.Create();

            sym.Key = KeyGenerator.GenerateAesKey(keyString);
            sym.IV = KeyGenerator.GenerateAesIv(sym.Key);

            sym.Mode = CipherMode.ECB;
            sym.Padding = PaddingMode.PKCS7;

            return Decrypt(sym, encrypt);
        }

    }
}
