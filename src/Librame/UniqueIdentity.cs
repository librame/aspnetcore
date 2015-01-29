// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Librame
{
    /// <summary>
    /// 唯一标识。
    /// </summary>
    /// <author>Librame Pang</author>
    public class UniqueIdentity
    {
        /// <summary>
        /// 构造一个动态的唯一标识实例。
        /// </summary>
        public UniqueIdentity()
            : this(Guid.Empty)
        {
        }
        /// <summary>
        /// 构造一个唯一标识实例。
        /// </summary>
        /// <param name="guid">给定的 GUID 字符串。</param>
        public UniqueIdentity(string guid)
            : this(Guid.Parse(guid))
        {
        }
        /// <summary>
        /// 构造一个唯一标识实例。
        /// </summary>
        /// <param name="guid">给定的 <see cref="System.Guid"/>。</param>
        public UniqueIdentity(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
            }

            Guid = guid;
        }

        /// <summary>
        /// 获取 <see cref="System.Guid"/>。
        /// </summary>
        public Guid Guid { get; private set; }
    }
}