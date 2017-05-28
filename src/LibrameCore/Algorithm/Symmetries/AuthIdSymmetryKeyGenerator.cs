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
using Microsoft.Extensions.Options;
using System.Linq;

namespace LibrameCore.Algorithm.Symmetries
{
    using TextCodecs;
    using Utility;

    /// <summary>
    /// 授权标识对称算法密钥生成器。
    /// </summary>
    public class AuthIdSymmetryKeyGenerator : AbstractSymmetryKeyGenerator
    {
        /// <summary>
        /// 构造一个授权标识对称算法密钥生成器实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plainText">给定的明文编解码器接口。</param>
        /// <param name="cipherText">给定的密文编解码器接口。</param>
        public AuthIdSymmetryKeyGenerator(ILogger<AuthIdSymmetryKeyGenerator> logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText)
            : base(logger, options, plainText, cipherText, options.Value.AuthId)
        {
        }

        /// <summary>
        /// 解析基础字节数组。
        /// </summary>
        /// <param name="keyString">给定的密钥字符串。</param>
        /// <returns>返回基础字节数组。</returns>
        protected override byte[] ParseBaseBytes(string keyString)
        {
            // 授权标识的格式是十六进制字符串
            return keyString.FromHex();
        }


        /// <summary>
        /// 转换基础字节数组为所需的密钥字节数组。
        /// </summary>
        /// <param name="baseBytes">给定的基础字节数组。</param>
        /// <param name="bytesLength">给定要生成的密钥字节数组长度。</param>
        /// <returns>返回密钥字节数组。</returns>
        protected override byte[] ConvertKeyBytes(byte[] baseBytes, int bytesLength)
        {
            return ConvertCore(baseBytes, bytesLength);
        }

        /// <summary>
        /// 转换密钥字节数组为所需的向量字节数组。
        /// </summary>
        /// <param name="key">给定的密钥。</param>
        /// <param name="bytesLength">给定要生成的向量字节数组长度。</param>
        /// <returns>返回向量字节数组。</returns>
        protected override byte[] ConvertIvBytes(byte[] key, int bytesLength)
        {
            return ConvertCore(key.Reverse().ToArray(), bytesLength);
        }


        /// <summary>
        /// 转换核心。
        /// </summary>
        /// <param name="bytes">给定的字节数组。</param>
        /// <param name="length">给定要生成的字节数组长度。</param>
        /// <returns>返回字节数组。</returns>
        protected virtual byte[] ConvertCore(byte[] bytes, int length)
        {
            // 默认授权标识的字节数组长度为 16（128 位）
            switch (length)
            {
                // 最小支持 64 位
                case 8:
                    return bytes.Skip(4).Take(8).ToArray();

                // 基方法虽然有判断长度相同直接返回，但不一定表示长度均为 16。
                case 16:
                    return bytes.Take(16).ToArray();

                case 24:
                    return bytes.Concat(bytes.Take(8).Reverse()).ToArray();

                // 当前最大仅支持 256 位
                case 32:
                    return bytes.Concat(bytes.Reverse()).ToArray();

                default:
                    return bytes;
            }
        }

    }
}
