// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;

namespace Librame.Data.Context
{
    /// <summary>
    /// 过滤器表达式静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class FilterExpressionExtensions
    {
        /// <summary>
        /// 过滤筛选查询。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="query">给定的查询对象。</param>
        /// <param name="filter">给定的过滤器对象（默认使用集成的过滤器查询表达式对象）。</param>
        /// <returns>返回查询对象。</returns>
        public static IQueryable<T> Filtration<T>(this IQueryable<T> query, FilterQueryBase filter) where T : class
        {
            if (!ReferenceEquals(filter, null))
            {
                return query.Filtration(new Kendo.FilterExpression(filter));
            }

            return query;
        }
        /// <summary>
        /// 过滤筛选查询。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="query">给定的查询对象。</param>
        /// <param name="filterExpression">给定的过滤表达式对象。</param>
        /// <returns>返回查询对象。</returns>
        public static IQueryable<T> Filtration<T>(this IQueryable<T> query, FilterExpressionBase filterExpression) where T : class
        {
            if (!ReferenceEquals(filterExpression, null))
            {
                query = filterExpression.BuildExpression(query);
            }

            return query;
        }

    }
}