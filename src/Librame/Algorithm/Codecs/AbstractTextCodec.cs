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
using System;
using System.Text;

namespace Librame.Algorithm.Codecs
{
    using Utility;

    /// <summary>
    /// 抽象文本编解码器。
    /// </summary>
    public abstract class AbstractTextCodec : ITextCodec
    {
        /// <summary>
        /// 构造一个抽象文本编解码器。
        /// </summary>
        /// <param name="logger">给定的记录器。</param>
        /// <param name="encoding">给定的字符编码。</param>
        public AbstractTextCodec(ILogger logger, Encoding encoding)
        {
            Logger = logger.NotNull(nameof(logger));
            Encoding = encoding.NotNull(nameof(encoding));
        }


        /// <summary>
        /// 记录器。
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// 字符编码。
        /// </summary>
        public Encoding Encoding { get; }


        /// <summary>
        /// 将字符串编码为字节序列。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        public virtual byte[] EncodeBytes(string str)
        {
            try
            {
                return EncodeBytesCore(str);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将字符串编码为字节序列。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        protected abstract byte[] EncodeBytesCore(string str);


        /// <summary>
        /// 将字节序列解码为字符串。
        /// </summary>
        /// <param name="buffer">给定的字节序列。</param>
        /// <returns>返回字符串。</returns>
        public virtual string DecodeBytes(byte[] buffer)
        {
            buffer.NotNull(nameof(buffer));

            try
            {
                return DecodeBytesCore(buffer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将字节序列解码为字符串。
        /// </summary>
        /// <param name="buffer">给定的字节序列。</param>
        /// <returns>返回字符串。</returns>
        protected abstract string DecodeBytesCore(byte[] buffer);

    }
}
