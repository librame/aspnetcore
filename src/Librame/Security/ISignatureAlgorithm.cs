// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Security
{
    /// <summary>
    /// 签名算法接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface ISignatureAlgorithm : IAlgorithm
    {
        /// <summary>
        /// 获取或设置用于签名哈希算法的名称。
        /// </summary>
        /// <value>
        /// 支持如“SHA1”；如果为空则表示不签名。
        /// </value>
        string SignHashAlgoName { get; set; }

        /// <summary>
        /// 验证签名。
        /// </summary>
        /// <param name="rgbHash">用 rgbSignature 签名的数据的哈希值。</param>
        /// <param name="rgbSignature">要为 rgbData 验证的签名。</param>
        /// <returns>如果 rgbSignature 与使用指定的哈希算法和密钥在 rgbHash 上计算出的签名匹配，则为 true；否则为 false。</returns>
        bool VerifySign(byte[] rgbHash, byte[] rgbSignature);
    }
}