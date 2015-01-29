// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Security
{
    /// <summary>
    /// 键控算法接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface IKeyedAlgorithm : IAlgorithm
    {
        /// <summary>
        /// 获取密钥信息。
        /// </summary>
        KeyInfo KeyInfo { get; }
    }
}