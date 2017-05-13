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
    /// 抽象对称算法。
    /// </summary>
    public abstract class AbstractSymmetryAlgorithm : AbstractByteCodec, ISymmetryAlgorithm
    {
        /// <summary>
        /// 构造一个抽象对称算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器工厂接口。</param>
        /// <param name="options">给定的选择项。</param>
        public AbstractSymmetryAlgorithm(ILogger<AbstractByteCodec> logger, IOptions<LibrameOptions> options)
            : base(logger, options)
        {
        }

        /// <summary>
        /// 将字节数组转换为字符串。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回字符串。</returns>
        protected abstract string ToString(byte[] buffer);
        /// <summary>
        /// 将加密字符串还原为字节数组。
        /// </summary>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <returns>返回字节数组。</returns>
        protected abstract byte[] FromString(string encrypt);
        
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
                sa.Mode = CipherMode.ECB;
                sa.Padding = PaddingMode.PKCS7;

                var buffer = EncodeBytes(str);

                var ct = sa.CreateEncryptor();
                buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);

                return ToString(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.InnerMessage());

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
                sa.Mode = CipherMode.ECB;
                sa.Padding = PaddingMode.PKCS7;

                var buffer = FromString(encrypt);

                var ct = sa.CreateDecryptor();
                buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);

                return DecodeBytes(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.InnerMessage());

                return encrypt;
            }
        }



        //public virtual string ToAes(string str, string authId = null)
        //{
        //    if (string.IsNullOrEmpty(authId))
        //        authId = Options.AuthId;

        //    var sa = Aes.Create();
        //    //sa.Key = ?;
        //    //sa.IV = ?;

        //    return Encrypt(sa, str);
        //}

    }
}
