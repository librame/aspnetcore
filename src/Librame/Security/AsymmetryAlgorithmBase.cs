// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;

namespace Librame.Security
{
    /// <summary>
    /// 非对称算法抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class AsymmetryAlgorithmBase : AlgorithmBase
    {
        /// <summary>
        /// 构造一个非对称算法抽象基类实例。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认采用 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <param name="encoding">给定的字符编码。</param>
        public AsymmetryAlgorithmBase(Encoding encoding = null)
            : base(encoding)
        {
        }

        /// <summary>
        /// 获取或设置是否要包括私有参数。
        /// </summary>
        /// <value>
        /// True 表示公私参数对；False 表示公有参数（默认为 False）。
        /// </value>
        public bool IncludePrivateParameters { get; set; }
            = false;

        /// <summary>
        /// 获取或设置算法参数。
        /// </summary>
        public object Parameters { get; set; }

        /// <summary>
        /// 解码字符串。
        /// </summary>
        /// <param name="str">给定要解密的字符串。</param>
        /// <returns>返回解码后的字符串。</returns>
        public virtual string Decode(string str)
        {
            var buffer = Encoding.GetBytes(str);

            buffer = ComputeDecoding(buffer);

            return WebEncoders.Base64UrlEncode(buffer);
        }

        /// <summary>
        /// 计算编码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的字节数组。</returns>
        public abstract byte[] ComputeDecoding(byte[] buffer);
    }
}