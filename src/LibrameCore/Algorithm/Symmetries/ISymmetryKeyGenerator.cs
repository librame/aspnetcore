#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameCore.Algorithm.Symmetries
{
    /// <summary>
    /// 对称算法密钥生成器接口。
    /// </summary>
    public interface ISymmetryKeyGenerator : IKeyGenerator
    {
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
