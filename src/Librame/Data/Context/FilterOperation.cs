// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Librame.Data.Context
{
    /// <summary>
    /// 过滤运算方式。
    /// </summary>
    /// <author>Librame Pang</author>
    public enum FilterOperation
    {
        /// <summary>
        /// 等于。
        /// </summary>
        [Description("等于")]
        Equals,
        /// <summary>
        /// 不等于。
        /// </summary>
        [Description("不等于")]
        NotEquals,
        /// <summary>
        /// 大于。
        /// </summary>
        [Description("大于")]
        GreaterThan,
        /// <summary>
        /// 大于或等于。
        /// </summary>
        [Description("大于或等于")]
        GreaterThanOrEquals,
        /// <summary>
        /// 小于。
        /// </summary>
        [Description("小于")]
        LessThan,
        /// <summary>
        /// 小于或等于。
        /// </summary>
        [Description("小于或等于")]
        LessThanOrEquals,
        /// <summary>
        /// 以...开始。
        /// </summary>
        [Description("以...开始")]
        StartsWith,
        /// <summary>
        /// 以...结束。
        /// </summary>
        [Description("以...结束")]
        EndsWith,
        /// <summary>
        /// 包含。
        /// </summary>
        [Description("包含")]
        Contains,
        /// <summary>
        /// 不包含。
        /// </summary>
        [Description("不包含")]
        NotContains
    }
}