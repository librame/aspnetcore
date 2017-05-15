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

namespace Librame.Algorithm.Asymmetries
{
    /// <summary>
    /// 非对称算法密钥生成器接口。
    /// </summary>
    public interface IAsymmetryAlgorithmKeyGenerator : IKeyGenerator
    {
        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        IByteConverter ByteConverter { get; }

        /// <summary>
        /// 记录器接口。
        /// </summary>
        ILogger Logger { get; }
    }
}
