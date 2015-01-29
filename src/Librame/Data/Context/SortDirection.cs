// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel;

namespace Librame.Data.Context
{
    /// <summary>
    /// 排序方向。
    /// </summary>
    /// <author>Librame Pang</author>
    public enum SortDirection
    {
        /// <summary>
        /// 升序。
        /// </summary>
        [Description("升序")]
        Asc,
        /// <summary>
        /// 降序。
        /// </summary>
        [Description("降序")]
        Desc
    }
}