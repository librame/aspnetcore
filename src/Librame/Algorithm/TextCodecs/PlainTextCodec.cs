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

namespace Librame.Algorithm.TextCodecs
{
    using Utility;

    /// <summary>
    /// 明文编解码器。
    /// </summary>
    public class PlainTextCodec : AbstractTextCodec, IPlainTextCodec
    {
        /// <summary>
        /// 构造一个明文编解码器。
        /// </summary>
        /// <param name="logger">给定的记录器。</param>
        /// <param name="options">给定的选择项。</param>
        public PlainTextCodec(ILogger<IPlainTextCodec> logger, IOptions<LibrameOptions> options)
            : base(logger, options.Value.Encoding.AsEncoding())
        {
        }


        /// <summary>
        /// 将字符串编码为字节序列。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        protected override byte[] EncodeBytesCore(string str)
        {
            return str.NotEmpty(nameof(str)).EncodeBytes(Encoding);
        }


        /// <summary>
        /// 将字节序列解码为字符串。
        /// </summary>
        /// <param name="buffer">给定的字节序列。</param>
        /// <returns>返回字符串。</returns>
        protected override string DecodeBytesCore(byte[] buffer)
        {
            return buffer.NotNull(nameof(buffer)).DecodeBytes(Encoding);
        }

    }
}
