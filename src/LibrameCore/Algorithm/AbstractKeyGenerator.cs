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

namespace LibrameCore.Algorithm
{
    using TextCodecs;

    /// <summary>
    /// 抽象密钥生成器。
    /// </summary>
    public abstract class AbstractKeyGenerator : AbstractAlgorithm, IKeyGenerator
    {
        /// <summary>
        /// 构造一个抽象密钥生成器实例。
        /// </summary>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="options">给定的选择项。</param>
        /// <param name="plainText">给定的明文编解码器接口。</param>
        /// <param name="cipherText">给定的密文编解码器接口。</param>
        public AbstractKeyGenerator(ILogger logger, IOptions<LibrameOptions> options,
            IPlainTextCodec plainText, ICipherTextCodec cipherText)
            : base(logger, options, plainText, cipherText)
        {
        }

    }
}
