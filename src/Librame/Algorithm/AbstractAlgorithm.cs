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
    using Codecs;
    using Utility;

    /// <summary>
    /// 抽象算法。
    /// </summary>
    public abstract class AbstractAlgorithm : IAlgorithm
    {
        /// <summary>
        /// 构造一个抽象算法实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plainText">给定的明文编解码器接口。</param>
        /// <param name="cipherText">给定的密文编解码器接口。</param>
        public AbstractAlgorithm(ILogger logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText)
        {
            Options = options.NotNull(nameof(options)).Value;
            Logger = logger.NotNull(nameof(logger));

            PlainText = plainText.NotNull(nameof(plainText));
            CipherText = cipherText.NotNull(nameof(cipherText));
        }


        /// <summary>
        /// 记录器。
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// 选项项。
        /// </summary>
        public LibrameOptions Options { get; }
        
        /// <summary>
        /// 密文编解码器。
        /// </summary>
        public ITextCodec CipherText { get; }

        /// <summary>
        /// 明文编解码器。
        /// </summary>
        public ITextCodec PlainText { get; }


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

    }
}
