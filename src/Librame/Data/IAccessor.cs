// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Librame.Data
{
    /// <summary>
    /// 访问器接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface IAccessor : IDisposable
    {
        /// <summary>
        /// 获取当前数据源对象。
        /// </summary>
        object Source { get; }

        /// <summary>
        /// 获取查询接口。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <returns>返回查询对象。</returns>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        /// 获取匹配的单个实体。
        /// </summary>
        /// <remarks>
        /// 如果有多个匹配实体，则默认返回第一项。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="predicate">断定实体的表达式。</param>
        /// <returns>返回实体。</returns>
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        /// <summary>
        /// 异步获取匹配的单个实体。
        /// </summary>
        /// <remarks>
        /// 如果有多个匹配实体，则默认返回第一项。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="predicate">断定实体的表达式。</param>
        /// <returns>返回实体。</returns>
        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// 增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        T Add<T>(T entity) where T : class;
        /// <summary>
        /// 异步增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> AddAsync<T>(T entity) where T : class;

        /// <summary>
        /// 更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        T Update<T>(T entity) where T : class;
        /// <summary>
        /// 异步更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> UpdateAsync<T>(T entity) where T : class;

        /// <summary>
        /// 保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        T Save<T>(T entity) where T : class;
        /// <summary>
        /// 异步保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> SaveAsync<T>(T entity) where T : class;

        /// <summary>
        /// 删除单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        T Delete<T>(T entity) where T : class;
        /// <summary>
        /// 异步删除单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        Task<T> DeleteAsync<T>(T entity) where T : class;

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
    }
}