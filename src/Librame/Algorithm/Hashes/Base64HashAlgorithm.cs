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
    /// BASE64 散列算法。
    /// </summary>
    public class Base64HashAlgorithm : AbstractHashAlgorithm, IHashAlgorithm
    {
        /// <summary>
        /// 构造一个 BASE64 散列算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器工厂接口。</param>
        /// <param name="options">给定的选择项。</param>
        public Base64HashAlgorithm(ILogger<AbstractByteCodec> logger, IOptions<LibrameOptions> options)
            : base(logger, options)
        {
        }

        /// <summary>
        /// 字节数组转换器。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回 BASE64 字符串。</returns>
        protected override string ToString(byte[] buffer)
        {
            return buffer.ToBase64();
        }

    }
}
