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
using System;

namespace LibrameCore.Algorithm.TextCodecs
{
    using Utility;

    /// <summary>
    /// 十六进制密文编解码器。
    /// </summary>
    public class HexCipherTextCodec : AbstractTextCodec, ICipherTextCodec
    {
        /// <summary>
        /// 构造一个十六进制密文编解码器。
        /// </summary>
        /// <param name="logger">给定的记录器。</param>
        /// <param name="options">给定的选择项。</param>
        public HexCipherTextCodec(ILogger<HexCipherTextCodec> logger, IOptions<LibrameOptions> options)
            : base(logger, options.Value.Encoding.AsEncoding())
        {
        }


        /// <summary>
        /// 将字符串编码为字节序列。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        public override byte[] GetBytes(string str)
        {
            try
            {
                return str.NotEmpty(nameof(str)).FromHex();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.AsInnerMessage());

                throw ex;
            }
        }


        /// <summary>
        /// 将字节序列解码为字符串。
        /// </summary>
        /// <param name="buffer">给定的字节序列。</param>
        /// <returns>返回字符串。</returns>
        public override string GetString(byte[] buffer)
        {
            try
            {
                return buffer.NotNull(nameof(buffer)).AsHex();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.AsInnerMessage());

                throw ex;
            }
        }

    }
}
