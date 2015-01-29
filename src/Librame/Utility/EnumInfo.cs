// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Utility
{
    /// <summary>
    /// 枚举信息。
    /// </summary>
    /// <author>Librame Pang</author>
    public class EnumInfo
    {
        /// <summary>
        /// 构造一个枚举信息。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="value">给定的常数值。</param>
        /// <param name="description">给定的描述。</param>
        public EnumInfo(string name, int value, string description = "")
        {
            Name = name;
            Value = value;
            Description = description;
        }

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 获取常数值。
        /// </summary>
        public int Value { get; private set; }
        /// <summary>
        /// 获取描述。
        /// </summary>
        public string Description { get; private set; }
    }
}