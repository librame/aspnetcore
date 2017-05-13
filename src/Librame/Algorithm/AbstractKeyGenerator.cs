#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.Algorithm
{
    using Utility;

    ///// <summary>
    ///// 抽象密钥生成器。
    ///// </summary>
    //public abstract class AbstractKeyGenerator : IKeyGenerator
    //{
    //    /// <summary>
    //    /// 解析字节数组。
    //    /// </summary>
    //    /// <param name="authId">给定的授权编号。</param>
    //    /// <returns>返回字节数组。</returns>
    //    protected abstract byte[] ParseBytes(string authId);


    //    /// <summary>
    //    /// 生成密钥。
    //    /// </summary>
    //    /// <param name="authId">给定的授权编号。</param>
    //    /// <param name="bitSize">给定要生成的密钥比特大小。</param>
    //    /// <returns>返回字节数组。</returns>
    //    public virtual byte[] GenerateKey(string authId, int bitSize)
    //    {
    //        var baseBytes = ParseBytes(authId);

    //        if (bitSize % 8 != 0)
    //            throw new ArgumentException("bitSize must be in multiples of 8.");

    //        var length = bitSize / 8;

    //        if (baseBytes.Length == length)
    //            return baseBytes;
    //    }


    //    /// <summary>
    //    /// 生成向量。
    //    /// </summary>
    //    /// <param name="authId">给定的授权编号。</param>
    //    /// <param name="bitSize">给定要生成的密钥比特大小。</param>
    //    /// <returns>返回字节数组。</returns>
    //    public virtual byte[] GenerateIv(string authId, int bitSize)
    //    {
    //        var baseKey = ParseBytes(authId);

    //        return GenerateBytes(authId, bitSize);
    //    }

    //}
}
