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

namespace Librame.Algorithm.Symmetries
{
    /// <summary>
    /// 对称算法密钥生成器接口。
    /// </summary>
    public interface ISymmetryAlgorithmKeyGenerator : IKeyGenerator
    {
        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        IByteConverter ByteConverter { get; }

        /// <summary>
        /// 记录器接口。
        /// </summary>
        ILogger Logger { get; }


        /// <summary>
        /// 生成 AES 密钥。
        /// </summary>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回字节数组。</returns>
        byte[] GenerateAesKey(string keyString = null);

        /// <summary>
        /// 生成 AES 向量。
        /// </summary>
        /// <param name="key">给定的密钥字节数组。</param>
        /// <returns>返回字节数组。</returns>
        byte[] GenerateAesIv(byte[] key);
    }
}
