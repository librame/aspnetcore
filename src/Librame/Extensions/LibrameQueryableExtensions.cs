#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>
    /// Librame 查询静态扩展。
    /// </summary>
    public static class LibrameQueryableExtensions
    {
        /// <summary>
        /// 取得单条数据。
        /// </summary>
        /// <typeparam name="T">指定的元素类型。</typeparam>
        /// <param name="query">给定的查询接口。</param>
        /// <param name="predicate">用于测试每个元素是否满足条件的函数（可选；默认查询所有）。</param>
        /// <param name="isUnique">是否要求唯一性（如果为 True，则表示查询出多条数据将会抛出异常）。</param>
        /// <returns>返回类型实例。</returns>
        public static T FirstOrSingleOrDefault<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate = null,
            bool isUnique = true)
        {
            // 唯一性约束
            if (isUnique)
            {
                if (ReferenceEquals(predicate, null))
                    return query.SingleOrDefault();

                return query.SingleOrDefault(predicate);
            }

            // 第一条
            if (ReferenceEquals(predicate, null))
                return query.FirstOrDefault();

            return query.FirstOrDefault(predicate);
        }


        /// <summary>
        /// 基于谓词筛选值序列。
        /// </summary>
        /// <typeparam name="T">指定的元素类型。</typeparam>
        /// <param name="query">给定的查询接口。</param>
        /// <param name="predicate">用于测试每个元素是否满足条件的函数（可选；默认查询所有）。</param>
        /// <returns>返回一个查询接口。</returns>
        public static IQueryable<T> WhereOrDefault<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate = null)
        {
            query.NotNull(nameof(query));

            if (ReferenceEquals(predicate, null))
                return query;
            
            return query.Where(predicate);
        }


        /// <summary>
        /// 将序列中的每个元素的指定属性投影到新表中。
        /// </summary>
        /// <typeparam name="T">指定的元素类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="query">给定的查询接口。</param>
        /// <param name="selector">给定的属性选择器。</param>
        /// <param name="removeDuplicates">是否移除重复项（可选；默认移除）。</param>
        /// <returns>返回一个查询接口。</returns>
        public static IQueryable<TProperty> SelectProperties<T, TProperty>(this IQueryable<T> query,
            Expression<Func<T, TProperty>> selector, bool removeDuplicates = true)
        {
            var selectQuery = query.NotNull(nameof(query))
                .Select(selector.NotNull(nameof(selector)));

            if (removeDuplicates)
                selectQuery = selectQuery.Distinct();

            return selectQuery;
        }


        /// <summary>
        /// 对查询结果进行排序。
        /// </summary>
        /// <typeparam name="T">指定的元素类型。</typeparam>
        /// <param name="query">给定的查询接口。</param>
        /// <param name="order">给定的排序方法。</param>
        /// <returns>返回一个查询接口。</returns>
        public static IQueryable<T> Order<T>(this IQueryable<T> query, Action<Orderable<T>> order = null)
        {
            // 如果不排序，则直接返回
            if (ReferenceEquals(order, null))
                return query;

            var orderable = new Orderable<T>(query);
            order(orderable);

            return orderable.Queryable;
        }


        /// <summary>
        /// 对查询结果进行排序分页。
        /// </summary>
        /// <typeparam name="T">指定的元素类型。</typeparam>
        /// <param name="query">给定的查询接口。</param>
        /// <param name="order">给定的排序方法。</param>
        /// <param name="createInfoFactory">给定创建分页信息的工厂方法。</param>
        /// <returns>返回一个分页接口。</returns>
        public static IPagingable<T> Paging<T>(this IQueryable<T> query, Action<Orderable<T>> order,
            Func<int, PagingInfo> createInfoFactory)
        {
            // 分页必须启用排序
            order.NotNull(nameof(order));

            var orderable = new Orderable<T>(query);
            order.Invoke(orderable);

            query = orderable.Queryable;

            // 计算分页信息
            var total = query.Count();
            var info = createInfoFactory(total);

            // 跳过条数
            if (info.Skip > 0)
                query = query.Skip(info.Skip);

            // 获取条数
            if (info.Size > 0)
                query = query.Take(info.Size);

            // 执行查询
            var rows = new ReadOnlyCollection<T>(query.ToList());

            return new PagingList<T>(rows, info);
        }

    }
}
