// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Librame.Data
{
    /// <summary>
    /// 跟踪状态。
    /// </summary>
    /// <author>Librame Pang</author>
    public enum TrackState
    {
        /// <summary>
        /// 未知。
        /// </summary>
        [Description("未知")]
        None = 0,
        //Unknown = 1,
        /// <summary>
        /// 无变化。
        /// </summary>
        [Description("无变化")]
        Unchanged = 2,
        /// <summary>
        /// 已删除。
        /// </summary>
        [Description("已删除")]
        Deleted = 4,
        //Modified = 8,
        /// <summary>
        /// 已增加。
        /// </summary>
        [Description("已增加")]
        Added = 16
    }
}