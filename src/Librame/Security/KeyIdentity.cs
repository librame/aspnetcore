// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Security
{
    /// <summary>
    /// 密钥标识。
    /// </summary>
    /// <author>Librame Pang</author>
    public class KeyIdentity : SecurityIdentityBase
    {
        /// <summary>
        /// 构造一个密钥标识实例。
        /// </summary>
        /// <param name="unique">给定的唯一标识。</param>
        /// <param name="maxSize">给定的最大键位大小。</param>
        public KeyIdentity(UniqueIdentity unique, BitSize maxSize)
            : base(unique, maxSize)
        {
        }

    }
}