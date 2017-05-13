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

namespace Librame.Algorithm.Hashes
{
    using Utility;

    /// <summary>
    /// 十六进制散列算法。
    /// </summary>
    public class HexHashAlgorithm : AbstractHashAlgorithm, IHashAlgorithm
    {
        /// <summary>
        /// 构造一个十六进制散列算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器工厂接口。</param>
        /// <param name="options">给定的选择项。</param>
        public HexHashAlgorithm(ILogger<AbstractByteCodec> logger, IOptions<LibrameOptions> options)
            : base(logger, options)
        {
        }

        /// <summary>
        /// 字节数组转换器。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回十六进制字符串。</returns>
        protected override string ToString(byte[] buffer)
        {
            return buffer.ToHex();
        }

    }
}
