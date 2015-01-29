// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography;
using System.Text;

namespace Librame.Security
{
    /// <summary>
    /// 键控算法抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class KeyedAlgorithmBase : AlgorithmBase
    {
        /// <summary>
        /// 构造一个键控算法抽象基类实例。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认采用 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// key 为空。
        /// </exception>
        /// <param name="key">给定的密钥字节数组。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        public KeyedAlgorithmBase(byte[] key, Encoding encoding = null)
            : base(encoding)
        {
            if (ReferenceEquals(key, null))
            {
                throw new ArgumentNullException("key");
            }

            KeyInfo = new KeyInfo(key);
        }
        /// <summary>
        /// 构造一个键控算法抽象基类实例。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认采用 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// keyId 为空。
        /// </exception>
        /// <param name="keyId">给定的密钥标识。</param>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        protected KeyedAlgorithmBase(KeyIdentity keyId, Encoding encoding = null)
            : base(encoding)
        {
            if (ReferenceEquals(keyId, null))
            {
                throw new ArgumentNullException("keyId");
            }

            KeyInfo = new KeyInfo(keyId.GenerateSecretIdCode());
        }


        /// <summary>
        /// 获取密钥信息。
        /// </summary>
        public KeyInfo KeyInfo { get; private set; }


        /// <summary>
        /// 计算编码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的字节数组。</returns>
        public override byte[] ComputeEncoding(byte[] buffer)
        {
            using (var ha = CreateAlgorithm(KeyInfo.Key))
            {
                buffer = ha.ComputeHash(buffer);
            }

            return buffer;
        }

        /// <summary>
        /// 创建键控哈希算法。
        /// </summary>
        /// <param name="key">给定的密钥字节数组。</param>
        /// <returns>返回算法实例。</returns>
        protected abstract KeyedHashAlgorithm CreateAlgorithm(byte[] key);
    }
}