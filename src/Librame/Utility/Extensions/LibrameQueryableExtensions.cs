// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{
    /// <summary>
    /// Librame 可查询静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameQueryableExtensions
    {
        /// <summary>
        /// 统一查询。
        /// </summary>
        /// <remarks>
        /// 支持过滤、排序和可分页查询。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// query 为空。
        /// </exception>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="query">给定的可查询对象。</param>
        /// <param name="total">输出总条数。</param>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <param name="skip">给定跳过的数据条数。</param>
        /// <param name="take">给定显示的数据条数。</param>
        /// <returns>返回列表集合。</returns>
        public static IList<T> Query<T>(this IQueryable<T> query, out int total,
            Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null,
            int skip = 0, int take = 0)
        {
            if (ReferenceEquals(query, null))
                throw new ArgumentNullException("query");

            IQueryable<T> _queryCopy = query;
            // 如果使用过滤
            if (!ReferenceEquals(filterFactory, null))
            {
                _queryCopy = filterFactory(_queryCopy);
            }

            IOrderedQueryable<T> _orderedQuery = null;
            // 如果使用排序
            if (!ReferenceEquals(sorterFactory, null))
            {
                _orderedQuery = sorterFactory(_queryCopy);
            }

            // 默认总数为0
            total = 0;
            IList<T> rows = null;

            // 如果进行分页查询
            if (take > 0)
            {
                // 获取未分页时的数据总数
                total = _queryCopy.Count();
                // 使用 orderedQuery 计数会跑出异常（某列无效，该列没有包含在聚合函数或 GROUP BY 子句中。）
                //total = _orderedQuery.Count();

                // 如果已排序（因排序查询对象可能会为空）
                if (!ReferenceEquals(_orderedQuery, null))
                {
                    rows = _orderedQuery.Skip(skip).Take(take).ToList();
                    // 如果没有数据，则读取第一页数据
                    if (skip > 0 && rows.Count == 0)
                    {
                        rows = _orderedQuery.Take(take).ToList();
                    }
                }
                else
                {
                    // A query containing the Skip operator must include at least one OrderBy operation.
                    rows = _queryCopy.Take(take).ToList();
                }
            }
            else
            {
                // 如果已排序（因排序查询对象可能会为空）
                if (!ReferenceEquals(_orderedQuery, null))
                {
                    rows = _orderedQuery.ToList();
                }
                else
                {
                    rows = _queryCopy.ToList();
                }

                // 未分页时，总数等于数据条数
                total = rows.Count;
            }

            return rows;
        }

        /// <summary>
        /// 异步统一查询（异步方法不支持 OUT 参数）。
        /// </summary>
        /// <remarks>
        /// 支持过滤、排序和可分页查询。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// query 为空。
        /// </exception>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="query">给定的可查询对象。</param>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <param name="skip">给定跳过的数据条数。</param>
        /// <param name="take">给定显示的数据条数。</param>
        /// <returns>返回列表集合。</returns>
        public static async Task<IList<T>> QueryAsync<T>(this IQueryable<T> query,
            Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null,
            int skip = 0, int take = 0)
        {
            if (ReferenceEquals(query, null))
                throw new ArgumentNullException("query");

            IQueryable<T> _queryCopy = query;
            // 如果使用过滤
            if (!ReferenceEquals(filterFactory, null))
            {
                _queryCopy = filterFactory(_queryCopy);
            }

            IOrderedQueryable<T> _orderedQuery = null;
            // 如果使用排序
            if (!ReferenceEquals(sorterFactory, null))
            {
                _orderedQuery = sorterFactory(_queryCopy);
            }

            IList<T> rows = null;

            // 如果进行分页查询
            if (take > 0)
            {
                // 如果已排序（因排序查询对象可能会为空）
                if (!ReferenceEquals(_orderedQuery, null))
                {
                    rows = await _orderedQuery.Skip(skip).Take(take).ToListAsync();
                    // 如果没有数据，则读取第一页数据
                    if (skip > 0 && rows.Count == 0)
                    {
                        rows = await _orderedQuery.Take(take).ToListAsync();
                    }
                }
                else
                {
                    // A query containing the Skip operator must include at least one OrderBy operation.
                    rows = await _queryCopy.Take(take).ToListAsync();
                }
            }
            else
            {
                // 如果已排序（因排序查询对象可能会为空）
                if (!ReferenceEquals(_orderedQuery, null))
                {
                    rows = await _orderedQuery.ToListAsync();
                }
                else
                {
                    rows = await _queryCopy.ToListAsync();
                }
            }

            return rows;
        }
        /// <summary>
        /// 异步统一查询总条数（异步方法不支持 OUT 参数）。
        /// </summary>
        /// <remarks>
        /// 支持过滤、排序和可分页查询。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// query 为空。
        /// </exception>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="query">给定的可查询对象。</param>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <param name="skip">给定跳过的数据条数。</param>
        /// <param name="take">给定显示的数据条数。</param>
        /// <returns>返回总条数。</returns>
        public static async Task<int> QueryTotalAsync<T>(this IQueryable<T> query,
            Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null)
        {
            if (ReferenceEquals(query, null))
                throw new ArgumentNullException("query");

            IQueryable<T> _queryCopy = query;
            // 如果使用过滤
            if (!ReferenceEquals(filterFactory, null))
            {
                _queryCopy = filterFactory(_queryCopy);
            }

            IOrderedQueryable<T> _orderedQuery = null;
            // 如果使用排序
            if (!ReferenceEquals(sorterFactory, null))
            {
                _orderedQuery = sorterFactory(_queryCopy);
            }

            int total = 0;
            // 如果已排序（因排序查询对象可能会为空）
            if (!ReferenceEquals(_orderedQuery, null))
            {
                // 获取未分页时的数据总数
                total = await _orderedQuery.CountAsync();
            }
            else
            {
                // 获取未分页时的数据总数
                total = await _queryCopy.CountAsync();
            }

            return total;
        }

    }
}