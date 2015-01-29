// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Librame.Data
{
    /// <summary>
    /// 数据范围。
    /// </summary>
    /// <author>Librame Pang</author>
    public enum DataScope
    {
        /// <summary>
        /// 无。
        /// </summary>
        [Description("无")]
        None = -2,
        /// <summary>
        /// 已删除。
        /// </summary>
        [Description("已删除")]
        Deleted = -1,
        /// <summary>
        /// 已撤下。
        /// </summary>
        [Description("已撤下")]
        Removed = 0,
        /// <summary>
        /// 公开的。
        /// </summary>
        [Description("公开的")]
        Public = 1,
        /// <summary>
        /// 全部的。
        /// </summary>
        [Description("全部的")]
        Entire = 2,
        /// <summary>
        /// 已锁定。
        /// </summary>
        [Description("已锁定")]
        Locked = 4,
        /// <summary>
        /// 已授权。
        /// </summary>
        [Description("已授权")]
        Authorised = 8
    }
}