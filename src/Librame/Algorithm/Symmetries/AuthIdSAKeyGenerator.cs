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

namespace Librame.Algorithm.Symmetries
{
    /// <summary>
    /// 授权标识对称算法密钥生成器。
    /// </summary>
    public class AuthIdSAKeyGenerator : AbstractSAKeyGenerator
    {
        /// <summary>
        /// 构造一个授权标识对称算法密钥生成器实例。
        /// </summary>
        /// <param name="converter">给定的字节转换器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        public AuthIdSAKeyGenerator(IByteConverter converter,
            ILogger<AuthIdSAKeyGenerator> logger, IOptions<LibrameOptions> options)
            : base(converter, logger, options?.Value.AuthId)
        {
        }

        /// <summary>
        /// 转换核心。
        /// </summary>
        /// <param name="bytes">给定的字节数组。</param>
        /// <param name="length">给定要生成的字节数组长度。</param>
        /// <returns>返回字节数组。</returns>
        protected virtual byte[] ConvertCore(byte[] bytes, int length)
        {
            // 默认授权标识为 GUID 格式编码生成，标准字节数组长度为 16（128 位）

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

    }
}
