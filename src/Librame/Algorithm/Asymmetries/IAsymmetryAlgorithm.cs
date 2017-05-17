#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Algorithm.Asymmetries
{
    /// <summary>
    /// 非对称算法接口。
    /// </summary>
    public interface IAsymmetryAlgorithm
    {
        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        ICiphertextCodec ByteConverter { get; }
    }
}
