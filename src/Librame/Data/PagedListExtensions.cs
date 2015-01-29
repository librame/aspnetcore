// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace System.Collections.Generic
{
    /// <summary>
    /// 分页列表静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class PagedListExtensions
    {
        /// <summary>
        /// 将列表集合转换为分页列表集合。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="rows">给定的列表集合。</param>
        /// <param name="total">给定的总条数。</param>
        /// <param name="skip">给定跳过的数据条数。</param>
        /// <param name="take">给定显示的数据条数。</param>
        /// <returns>返回数据分页对象。</returns>
        public static IPageable<T> ToPagedList<T>(this IList<T> rows, int total, int skip, int take)
        {
            return new PagedList<T>(rows, total, skip, take);
        }

    }
}