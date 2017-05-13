﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System.Text;

namespace Librame.Algorithm
{
    /// <summary>
    /// 字节编解码器接口。
    /// </summary>
    public interface IByteCodec
    {
        /// <summary>
        /// 选项项。
        /// </summary>
        LibrameOptions Options { get; }

        /// <summary>
        /// 记录器。
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 字符编码。
        /// </summary>
        Encoding Encoding { get; set; }


        /// <summary>
        /// 将字符串编码为字节序列。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回字节数组。</returns>
        byte[] EncodeBytes(string str);

        /// <summary>
        /// 将字节序列解码为字符串。
        /// </summary>
        /// <param name="buffer">给定的字节序列。</param>
        /// <returns>返回字符串。</returns>
        string DecodeBytes(byte[] buffer);
    }
}
