// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// 用于泛类型的可分页列表。
    /// </summary>
    /// <typeparam name="T">列表中元素的类型。</typeparam>
    /// <author>Librame Pang</author>
    public class PagedList<T> : IPageable<T>
    {
        /// <summary>
        /// 构造一个 <see cref="PagedList{T}"/> 对象。
        /// </summary>
        /// <param name="rows">给定的数据集合。</param>
        /// <param name="total">给定的数据总条数。</param>
        /// <param name="skip">给定跳过的数据条数。</param>
        /// <param name="take">给定显示的数据条数。</param>
        public PagedList(IList<T> rows, int total, int skip, int take)
        {
            Rows = rows;
            Total = total;
            Skip = skip;
            Take = take;
        }

        /// <summary>
        /// 获取数据行集合。
        /// </summary>
        /// <value>
        /// 分页查询时的单页数据列表。
        /// </value>
        public IList<T> Rows { get; private set; }
        /// <summary>
        /// 获取数据总条数。
        /// </summary>
        /// <value>
        /// 分页查询时的数据总条数。
        /// </value>
        public int Total { get; private set; }
        /// <summary>
        /// 获取跳过的数据条数。
        /// </summary>
        public int Skip { get; private set; }
        /// <summary>
        /// 获取显示的数据条数。
        /// </summary>
        public int Take { get; private set; }

        /// <summary>
        /// 获取开始或默认项。
        /// </summary>
        public T First
        {
            get { return Rows.FirstOrDefault(); }
        }
        /// <summary>
        /// 获取末尾或默认项。
        /// </summary>
        public T Last
        {
            get { return Rows.LastOrDefault(); }
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举数。
        /// </summary>
        /// <returns>返回枚举数。</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return (ReferenceEquals(Rows, null) ? Rows.GetEnumerator() : null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //private static IQueryable<T> Paged(IQueryable<T> queryable, int size, int index)
        //{
        //    if (size > 0)
        //    {
        //        if (index > 1)
        //            queryable = queryable.Skip((index - 1) * size);

        //        return queryable.Take(size);
        //    }
        //    else
        //        return queryable;
        //}
    }
}