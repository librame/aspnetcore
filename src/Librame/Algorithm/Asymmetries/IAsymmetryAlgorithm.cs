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
        IByteConverter ByteConverter { get; }

        
        /// <summary>
        /// 对指定字节数组进行签名。
        /// </summary>
        /// <param name="privateKey">给定的私钥。</param>
        /// <param name="bytes">给定要签名的字节数组。</param>
        /// <returns>返回签名后的字节数组。</returns>
        byte[] Signature(string privateKey, byte[] bytes);
    }
}
