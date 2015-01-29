// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Security
{
    /// <summary>
    /// 对称算法接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface ISymmetryAlgorithm : IAlgorithm
    {
        /// <summary>
        /// 获取密钥向量信息。
        /// </summary>
        KeyIvInfo KeyIvInfo { get; }

        /// <summary>
        /// 解码字符串。
        /// </summary>
        /// <param name="str">给定要解密的字符串。</param>
        /// <returns>返回解码后的字符串。</returns>
        string Decode(string str);

        /// <summary>
        /// 计算解码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的原始字符串。</returns>
        byte[] ComputeDecoding(byte[] buffer);
    }
}