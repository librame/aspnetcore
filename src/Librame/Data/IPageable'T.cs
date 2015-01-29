// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace System.Collections.Generic
{
    /// <summary>
    /// 用于泛类型的可分页接口。
    /// </summary>
    /// <typeparam name="T">列表中元素的类型。</typeparam>
    /// <author>Librame Pang</author>
    public interface IPageable<T> : IEnumerable<T>
    {
        /// <summary>
        /// 获取数据行集合。
        /// </summary>
        /// <value>
        /// 分页查询时的单页数据列表。
        /// </value>
        IList<T> Rows { get; }
        /// <summary>
        /// 获取数据总条数。
        /// </summary>
        /// <value>
        /// 分页查询时的数据总条数。
        /// </value>
        int Total { get; }
        /// <summary>
        /// 获取跳过的数据条数。
        /// </summary>
        int Skip { get; }
        /// <summary>
        /// 获取显示的数据条数。
        /// </summary>
        int Take { get; }

        /// <summary>
        /// 获取开始或默认项。
        /// </summary>
        T First { get; }
        /// <summary>
        /// 获取末尾或默认项。
        /// </summary>
        T Last { get; }
    }
}