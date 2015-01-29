// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography;
using System.Text;

namespace Librame.Security
{
    /// <summary>
    /// 对称算法抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class SymmetryAlgorithmBase : AlgorithmBase
    {
        /// <summary>
        /// 构造一个对称算法抽象基类实例。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认采用 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// key 和 iv 为空。
        /// </exception>
        /// <param name="key">给定的密钥字节数组。</param>
        /// <param name="iv">给定的向量字节数组。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        public SymmetryAlgorithmBase(byte[] key, byte[] iv, Encoding encoding = null)
            : base(encoding)
        {
            if (ReferenceEquals(key, null))
            {
                throw new ArgumentNullException("key");
            }
            if (ReferenceEquals(iv, null))
            {
                throw new ArgumentNullException("iv");
            }

            KeyIvInfo = new KeyIvInfo(key, iv);
        }
        /// <summary>
        /// 构造一个对称算法抽象基类实例。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认采用 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// keyId 和 ivId 为空。
        /// </exception>
        /// <param name="keyId">给定的密钥标识。</param>
        /// <param name="ivId">给定的向量标识。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        protected SymmetryAlgorithmBase(KeyIdentity keyId, IvIdentity ivId, Encoding encoding = null)
            : base(encoding)
        {
            if (ReferenceEquals(keyId, null))
            {
                throw new ArgumentNullException("keyId");
            }
            if (ReferenceEquals(ivId, null))
            {
                throw new ArgumentNullException("ivId");
            }

            KeyIvInfo = new KeyIvInfo(keyId.GenerateSecretIdCode(), ivId.GenerateSecretIdCode());
        }


        /// <summary>
        /// 获取密钥向量信息。
        /// </summary>
        public KeyIvInfo KeyIvInfo { get; private set; }


        /// <summary>
        /// 解码字符串。
        /// </summary>
        /// <param name="str">给定要解密的字符串。</param>
        /// <returns>返回解码后的字符串。</returns>
        public virtual string Decode(string str)
        {
            var buffer = WebEncoders.Base64UrlDecode(str);

            buffer = ComputeDecoding(buffer);

            // 使用与 AlgorithmBase.Encode() 的 Encoding.GetBytes(str) 对应，以便返回正确的原始字符串
            return Encoding.GetString(buffer);
        }

        /// <summary>
        /// 计算解码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的字节数组。</returns>
        public virtual byte[] ComputeDecoding(byte[] buffer)
        {
            // 解密
            using (var sa = CreateAlgorithm())
            {
                sa.Key = KeyIvInfo.Key;
                sa.IV = KeyIvInfo.Iv;

                using (var ct = sa.CreateDecryptor())
                {
                    buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);
                    //using (var ms = new MemoryStream(buffer))
                    //{
                    //    using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Read))
                    //    {
                    //        using (var sr = new StreamReader(cs))
                    //        {
                    //            str = sr.ReadToEnd();
                    //        }
                    //    }
                    //}
                }
            }

            return buffer;
        }

        /// <summary>
        /// 计算编码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的字节数组。</returns>
        public override byte[] ComputeEncoding(byte[] buffer)
        {
            // 加密
            using (var sa = CreateAlgorithm())
            {
                sa.Key = KeyIvInfo.Key;
                sa.IV = KeyIvInfo.Iv;

                using (var ct = sa.CreateEncryptor())
                {
                    buffer = ct.TransformFinalBlock(buffer, 0, buffer.Length);
                    //using (var ms = new MemoryStream())
                    //{
                    //    using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                    //    {
                    //        using (var sw = new StreamWriter(cs))
                    //        {
                    //            sw.Write(buffer);
                    //        }
                    //    }

                    //    buffer = ms.ToArray();
                    //}
                }
            }

            return buffer;
        }
        
        /// <summary>
        /// 创建对称算法。
        /// </summary>
        /// <returns>返回算法实例。</returns>
        protected abstract SymmetricAlgorithm CreateAlgorithm();
    }
}