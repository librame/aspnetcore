#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Algorithm.Symmetries
{
    /// <summary>
    /// 对称算法接口。
    /// </summary>
    public interface ISymmetryAlgorithm
    {
        /// <summary>
        /// 密钥生成器接口。
        /// </summary>
        ISymmetryAlgorithmKeyGenerator KeyGenerator { get; }

        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        ICiphertextCodec ByteConverter { get; }


        /// <summary>
        /// 转换为 AES。
        /// </summary>
        /// <param name="str">给定待加密的字符串。</param>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回加密字符串。</returns>
        string ToAes(string str, string keyString = null);

        /// <summary>
        /// 还原 AES。
        /// </summary>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <param name="keyString">给定的密钥字符串（可选）。</param>
        /// <returns>返回原始字符串。</returns>
        string FromAes(string encrypt, string keyString = null);
    }
}
