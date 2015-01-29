// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Librame.Security
{
    /// <summary>
    /// 位大小。
    /// </summary>
    /// <author>Librame Pang</author>
    public enum BitSize
    {
        /// <summary>
        /// 64 位（8 字节）。
        /// </summary>
        [Description("64 bit")]
        _64 = 8,

        /// <summary>
        /// 128 位（16 字节）。
        /// </summary>
        [Description("128 bit")]
        _128 = 16,

        /// <summary>
        /// 160 位（20 字节）。
        /// </summary>
        [Description("160 bit")]
        _160 = 20,

        /// <summary>
        /// 192 位（24 字节）。
        /// </summary>
        [Description("192 bit")]
        _192 = 24,

        /// <summary>
        /// 256 位（32 字节）。
        /// </summary>
        [Description("256 bit")]
        _256 = 32,

        /// <summary>
        /// 384 位（48 字节）。
        /// </summary>
        [Description("384 bit")]
        _384 = 48,

        /// <summary>
        /// 512 位（64 字节）。
        /// </summary>
        [Description("512 bit")]
        _512 = 64,

        /// <summary>
        /// 768 位（96 字节）。
        /// </summary>
        [Description("768 bit")]
        _768 = 96,

        /// <summary>
        /// 1024 位（128 字节）。
        /// </summary>
        [Description("1024 bit")]
        _1024 = 128
    }
}