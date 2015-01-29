// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Librame.Data
{
    /// <summary>
    /// 实体仓库接口。
    /// </summary>
    /// <typeparam name="T">指定的实体类型。</typeparam>
    /// <author>Librame Pang</author>
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// 获取或设置访问器。
        /// </summary>
        IAccessor Accessor { get; set; }

        /// <summary>
        /// 获取当前实体查询对象。
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        T Add(T entity);
        /// <summary>
        /// 异步增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// 更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        T Update(T entity);
        /// <summary>
        /// 异步更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// 保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        T Save(T entity);
        /// <summary>
        /// 异步保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> SaveAsync(T entity);

        /// <summary>
        /// 删除单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        T Delete(T entity);
        /// <summary>
        /// 异步删除单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> DeleteAsync(T entity);

        /// <summary>
        /// 清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        int Flush();
        /// <summary>
        /// 异步清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        Task<int> FlushAsync();

        /// <summary>
        /// 计数查询统计。
        /// </summary>
        /// <param name="predicate">给定的查询断定表达式。</param>
        /// <returns>返回整数。</returns>
        int Count(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 异步计数查询统计。
        /// </summary>
        /// <param name="predicate">给定的查询断定表达式。</param>
        /// <returns>返回整数。</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取匹配的单个实体。
        /// </summary>
        /// <remarks>
        /// 如果有多个匹配实体，则默认返回第一项。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="predicate">断定实体的表达式。</param>
        /// <returns>返回实体。</returns>
        T Get(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 异步获取匹配的单个实体。
        /// </summary>
        /// <remarks>
        /// 如果有多个匹配实体，则默认返回第一项。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="predicate">断定实体的表达式。</param>
        /// <returns>返回实体。</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 列表查询。
        /// </summary>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <returns>返回实体列表。</returns>
        IList<T> List(Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null);
        /// <summary>
        /// 异步列表查询。
        /// </summary>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <returns>返回实体列表。</returns>
        Task<IList<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null);

        /// <summary>
        /// 分页列表查询。
        /// </summary>
        /// <param name="skip">给定跳过的条数。</param>
        /// <param name="take">给定取得的条数。</param>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <returns>返回分页实体列表。</returns>
        IPageable<T> PagedList(int skip, int take,
            Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null);
        /// <summary>
        /// 异步分页列表查询。
        /// </summary>
        /// <param name="skip">给定跳过的条数。</param>
        /// <param name="take">给定取得的条数。</param>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <returns>返回分页实体列表。</returns>
        Task<IPageable<T>> PagedListAsync(int skip, int take,
            Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null);
    }
}