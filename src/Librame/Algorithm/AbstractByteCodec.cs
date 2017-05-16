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
using System.Text;

namespace Librame.Algorithm
{
    using Utility;

    /// <summary>
    /// 抽象字节编解码器。
    /// </summary>
    public abstract class AbstractByteCodec : IByteCodec
    {
        /// <summary>
        /// 构造一个抽象字节编解码器实例。
        /// </summary>
        /// <param name="logger">给定的记录器工厂接口。</param>
        /// <param name="options">给定的选择项。</param>
        public AbstractByteCodec(ILogger logger, IOptions<LibrameOptions> options)
        {
            Options = options.NotNull(nameof(options)).Value;
            Logger = logger.NotNull(nameof(logger));
        }


        /// <summary>
        /// 记录器。
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// 选项项。
        /// </summary>
        public LibrameOptions Options { get; }


        private Encoding _encoding;
        /// <summary>
        /// 字符编码。
        /// </summary>
        public Encoding Encoding
        {
            get
            {
                if (_encoding == null)
                    _encoding = Options.Encoding.AsEncoding();

                return _encoding;
            }
            set
            {
                _encoding = value.NotNull(nameof(value));
            }
        }


        /// <summary>
        /// 将字符串编码为字节序列。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        public virtual byte[] EncodeBytes(string str)
        {
            return Encoding.GetBytes(str.NotEmpty(nameof(str)));
        }

        /// <summary>
        /// 将字节序列解码为字符串。
        /// </summary>
        /// <param name="buffer">给定的字节序列。</param>
        /// <returns>返回字符串。</returns>
        public virtual string DecodeBytes(byte[] buffer)
        {
            buffer.NotNull(nameof(buffer));

            return Encoding.GetString(buffer, 0, buffer.Length);
        }

    }
}
