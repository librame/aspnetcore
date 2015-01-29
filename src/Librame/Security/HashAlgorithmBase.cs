// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security
{
    /// <summary>
    /// 哈希算法抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class HashAlgorithmBase : AlgorithmBase
    {
        /// <summary>
        /// 构造一个哈希算法抽象基类实例。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认采用 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <param name="encoding">给定的字符编码。</param>
        public HashAlgorithmBase(Encoding encoding = null)
            : base(encoding)
        {
        }


        /// <summary>
        /// 计算编码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的字节数组。</returns>
        public override byte[] ComputeEncoding(byte[] buffer)
        {
            using (var ha = CreateAlgorithm())
            {
                buffer = ha.ComputeHash(buffer);
            }

            return buffer;
        }

        /// <summary>
        /// 创建哈希算法。
        /// </summary>
        /// <returns>返回算法实例。</returns>
        protected abstract HashAlgorithm CreateAlgorithm();
    }
}