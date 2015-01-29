// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;

namespace Librame.Security
{
    /// <summary>
    /// 算法抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class AlgorithmBase
    {
        /// <summary>
        /// 构造一个算法抽象基类实例。
        /// </summary>
        /// <remarks>
        /// 如果 encoding 为空，则默认采用 <see cref="Encoding.UTF8"/>。
        /// </remarks>
        /// <param name="encoding">给定的字符编码（可选）。</param>
        public AlgorithmBase(Encoding encoding = null)
        {
            Encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        /// 获取或设置字符编码。
        /// </summary>
        public Encoding Encoding { get; set; }
        

        /// <summary>
        /// 编码字符串。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回编码后的字符串。</returns>
        public virtual string Encode(string str)
        {
            var buffer = Encoding.GetBytes(str);

            buffer = ComputeEncoding(buffer);

            return WebEncoders.Base64UrlEncode(buffer);
            // Encoding.GetString 可能会包括很多方块和问号
            // return Encoding.GetString(buffer);
            // Convert.ToBase64String 生成的全部是 ASCII 字符
            // return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// 计算编码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的字节数组。</returns>
        public abstract byte[] ComputeEncoding(byte[] buffer);
    }
}