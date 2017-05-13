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
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>
    /// 表示可排序的查询。
    /// </summary>
    /// <typeparam name="T">指定的类型。</typeparam>
    public class Orderable<T>
    {
        private IQueryable<T> _queryable;

        /// <summary>
        /// 构造一个 <see cref="Orderable{T}"/> 实例。
        /// </summary>
        /// <param name="queryable">给定的 <see cref="IQueryable{T}"/>。</param>
        public Orderable(IQueryable<T> queryable)
        {
            _queryable = queryable.NotNull(nameof(queryable));
        }


        /// <summary>
        /// 获取 <see cref="IQueryable{T}"/>。
        /// </summary>
        public IQueryable<T> Queryable
        {
            get { return _queryable; }
        }


        /// <summary>
        /// 升序排列。
        /// </summary>
        /// <typeparam name="TKey">指定的键类型。</typeparam>
        /// <param name="keySelector">给定键选择器的查询表达式。</param>
        /// <returns>返回 <see cref="Orderable{T}"/>。</returns>
        public Orderable<T> Asc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = _queryable
                .OrderBy(keySelector);

            return this;
        }

        /// <summary>
        /// 升序排列。
        /// </summary>
        /// <typeparam name="TKey1">指定的键类型。</typeparam>
        /// <typeparam name="TKey2">指定的键类型。</typeparam>
        /// <param name="keySelector1">给定键选择器的查询表达式。</param>
        /// <param name="keySelector2">给定键选择器的查询表达式。</param>
        /// <returns>返回 <see cref="Orderable{T}"/>。</returns>
        public Orderable<T> Asc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                              Expression<Func<T, TKey2>> keySelector2)
        {
            _queryable = _queryable
                .OrderBy(keySelector1)
                .OrderBy(keySelector2);

            return this;
        }

        /// <summary>
        /// 升序排列。
        /// </summary>
        /// <typeparam name="TKey1">指定的键类型。</typeparam>
        /// <typeparam name="TKey2">指定的键类型。</typeparam>
        /// <typeparam name="TKey3">指定的键类型。</typeparam>
        /// <param name="keySelector1">给定键选择器的查询表达式。</param>
        /// <param name="keySelector2">给定键选择器的查询表达式。</param>
        /// <param name="keySelector3">给定键选择器的查询表达式。</param>
        /// <returns>返回 <see cref="Orderable{T}"/>。</returns>
        public Orderable<T> Asc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                     Expression<Func<T, TKey2>> keySelector2,
                                                     Expression<Func<T, TKey3>> keySelector3)
        {
            _queryable = _queryable
                .OrderBy(keySelector1)
                .OrderBy(keySelector2)
                .OrderBy(keySelector3);

            return this;
        }


        /// <summary>
        /// 降序排列。
        /// </summary>
        /// <typeparam name="TKey">指定的键类型。</typeparam>
        /// <param name="keySelector">给定键选择器的查询表达式。</param>
        /// <returns>返回 <see cref="Orderable{T}"/>。</returns>
        public Orderable<T> Desc<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector);

            return this;
        }

        /// <summary>
        /// 降序排列。
        /// </summary>
        /// <typeparam name="TKey1">指定的键类型。</typeparam>
        /// <typeparam name="TKey2">指定的键类型。</typeparam>
        /// <param name="keySelector1">给定键选择器的查询表达式。</param>
        /// <param name="keySelector2">给定键选择器的查询表达式。</param>
        /// <returns>返回 <see cref="Orderable{T}"/>。</returns>
        public Orderable<T> Desc<TKey1, TKey2>(Expression<Func<T, TKey1>> keySelector1,
                                               Expression<Func<T, TKey2>> keySelector2)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector1)
                .OrderByDescending(keySelector2);

            return this;
        }

        /// <summary>
        /// 降序排列。
        /// </summary>
        /// <typeparam name="TKey1">指定的键类型。</typeparam>
        /// <typeparam name="TKey2">指定的键类型。</typeparam>
        /// <typeparam name="TKey3">指定的键类型。</typeparam>
        /// <param name="keySelector1">给定键选择器的查询表达式。</param>
        /// <param name="keySelector2">给定键选择器的查询表达式。</param>
        /// <param name="keySelector3">给定键选择器的查询表达式。</param>
        /// <returns>返回 <see cref="Orderable{T}"/>。</returns>
        public Orderable<T> Desc<TKey1, TKey2, TKey3>(Expression<Func<T, TKey1>> keySelector1,
                                                      Expression<Func<T, TKey2>> keySelector2,
                                                      Expression<Func<T, TKey3>> keySelector3)
        {
            _queryable = _queryable
                .OrderByDescending(keySelector1)
                .OrderByDescending(keySelector2)
                .OrderByDescending(keySelector3);

            return this;
        }

    }
}
