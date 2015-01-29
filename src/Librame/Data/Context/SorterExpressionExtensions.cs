// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;

namespace Librame.Data.Context
{
    /// <summary>
    /// 排序器表达式静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class SorterExpressionExtensions
    {
        /// <summary>
        /// 建立排序。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="query">给定的查询对象。</param>
        /// <param name="sorter">给定的排序器对象（默认使用集成的排序器查询表达式对象）。</param>
        /// <param name="defaultOrderByFactory">给定的默认排序工厂方法。</param>
        /// <returns>返回查询对象或空。</returns>
        public static IOrderedQueryable<T> Sorting<T>(this IQueryable<T> query, SorterQueryBase sorter,
            Func<IQueryable<T>, IOrderedQueryable<T>> defaultOrderByFactory = null) where T : class
        {
            if (!ReferenceEquals(sorter, null))
            {
                return query.Sorting(new Kendo.SorterExpression(sorter));
            }
            else
            {
                if (!ReferenceEquals(defaultOrderByFactory, null))
                {
                    return defaultOrderByFactory(query);
                }
            }

            return null;
        }
        /// <summary>
        /// 建立排序。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="query">给定的查询对象。</param>
        /// <param name="sorterExpression">给定的排序表达式对象。</param>
        /// <returns>返回查询对象或空。</returns>
        public static IOrderedQueryable<T> Sorting<T>(this IQueryable<T> query, SorterExpressionBase sorterExpression) where T : class
        {
            if (!ReferenceEquals(sorterExpression, null))
            {
                return sorterExpression.Build(query);
            }

            return null;
        }

    }
}