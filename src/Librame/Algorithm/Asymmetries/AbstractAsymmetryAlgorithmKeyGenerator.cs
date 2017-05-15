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
using System.Linq;

namespace Librame.Algorithm.Asymmetries
{
    using Utility;

    /// <summary>
    /// 抽象非对称算法密钥生成器。
    /// </summary>
    public abstract class AbstractAsymmetryAlgorithmKeyGenerator : IAsymmetryAlgorithmKeyGenerator
    {
        private readonly string _defaultKeyString;

        /// <summary>
        /// 构造一个抽象非对称算法密钥生成器实例。
        /// </summary>
        /// <param name="converter">给定的字节转换器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        /// <param name="keyString">给定的密钥字符串。</param>
        public AbstractAsymmetryAlgorithmKeyGenerator(IByteConverter converter, ILogger logger,
            string keyString)
        {
            ByteConverter = converter.NotNull(nameof(converter));
            Logger = logger.NotNull(nameof(logger));

            _defaultKeyString = keyString.NotNullOrEmpty(nameof(keyString));
        }


        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        public IByteConverter ByteConverter { get; }

        /// <summary>
        /// 记录器接口。
        /// </summary>
        public ILogger Logger { get; }
    }
}
