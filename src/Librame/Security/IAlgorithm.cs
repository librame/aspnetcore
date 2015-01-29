// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;

namespace Librame.Security
{
    /// <summary>
    /// 算法接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface IAlgorithm
    {
        /// <summary>
        /// 获取或设置字符编码。
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// 编码字符串。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回编码后的字符串。</returns>
        string Encode(string str);

        /// <summary>
        /// 计算编码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的字节数组。</returns>
        byte[] ComputeEncoding(byte[] buffer);
    }
}