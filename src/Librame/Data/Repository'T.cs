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
    /// 实体仓库。
    /// </summary>
    /// <typeparam name="T">指定的实体类型。</typeparam>
    /// <author>Librame Pang</author>
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// 构造一个实体仓库实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// accessor 为空。
        /// </exception>
        /// <param name="accessor">给定的访问器。</param>
        public Repository(IAccessor accessor)
        {
            if (ReferenceEquals(accessor, null))
                throw new ArgumentNullException("accessor");

            Accessor = accessor;
        }


        /// <summary>
        /// 获取或设置访问器。
        /// </summary>
        public virtual IAccessor Accessor { get; set; }

        /// <summary>
        /// 获取当前实体查询对象。
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                // NHibernate.Linq.LinqExtensionMethods.Cacheable()
                return Accessor.Query<T>();
            }
        }


        /// <summary>
        /// 增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual T Add(T entity)
        {
            return Accessor.Add(entity);
        }
        /// <summary>
        /// 异步增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            return await Accessor.AddAsync(entity);
        }

        /// <summary>
        /// 更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual T Update(T entity)
        {
            return Accessor.Update(entity);
        }
        /// <summary>
        /// 异步更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual async Task<T> UpdateAsync(T entity)
        {
            return await Accessor.UpdateAsync(entity);
        }

        /// <summary>
        /// 保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual T Save(T entity)
        {
            return Accessor.Save(entity);
        }
        /// <summary>
        /// 异步保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual async Task<T> SaveAsync(T entity)
        {
            return await Accessor.SaveAsync(entity);
        }

        /// <summary>
        /// 删除单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual T Delete(T entity)
        {
            return Accessor.Delete(entity);
        }
        /// <summary>
        /// 异步删除单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public virtual async Task<T> DeleteAsync(T entity)
        {
            return await Accessor.DeleteAsync(entity);
        }

        /// <summary>
        /// 清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        public virtual int Flush()
        {
            return Accessor.Flush();
        }
        /// <summary>
        /// 异步清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        public virtual async Task<int> FlushAsync()
        {
            return await Accessor.FlushAsync();
        }

        /// <summary>
        /// 计数查询统计。
        /// </summary>
        /// <param name="predicate">给定的查询断定表达式。</param>
        /// <returns>返回整数。</returns>
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate).Count();
        }
        /// <summary>
        /// 异步计数查询统计。
        /// </summary>
        /// <param name="predicate">给定的查询断定表达式。</param>
        /// <returns>返回整数。</returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).CountAsync();
        }

        /// <summary>
        /// 获取匹配的单个实体。
        /// </summary>
        /// <remarks>
        /// 如果有多个匹配实体，则默认返回第一项。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="predicate">断定实体的表达式。</param>
        /// <returns>返回实体。</returns>
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            // 供 Create and Update 验证
            if (ReferenceEquals(predicate, null))
            {
                return null;
            }

            return Accessor.Get<T>(predicate);
        }
        /// <summary>
        /// 异步获取匹配的单个实体。
        /// </summary>
        /// <remarks>
        /// 如果有多个匹配实体，则默认返回第一项。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="predicate">断定实体的表达式。</param>
        /// <returns>返回实体。</returns>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            // 供 Create and Update 验证
            if (ReferenceEquals(predicate, null))
            {
                return null;
            }

            return await Accessor.GetAsync<T>(predicate);
        }

        /// <summary>
        /// 列表查询。
        /// </summary>
        /// <param name="filterFactory">给定的过滤方法（可选）。</param>
        /// <param name="sorterFactory">给定的排序方法（可选）。</param>
        /// <returns>返回实体列表。</returns>
        public virtual IList<T> List(Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null)
        {
            int total;
            return Table.Query(out total, filterFactory, sorterFactory);
        }
        /// <summary>
        /// 异步列表查询。
        /// </summary>
        /// <param name="filterFactory">给定的过滤方法。</param>
        /// <param name="sorterFactory">给定的排序方法。</param>
        /// <returns>返回实体列表。</returns>
        public virtual async Task<IList<T>> ListAsync(Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null)
        {
            return await Table.QueryAsync(filterFactory, sorterFactory);
        }

        /// <summary>
        /// 分页列表查询。
        /// </summary>
        /// <param name="filterFactory">给定的过滤方法。</param>
        /// <param name="sorterFactory">给定的排序方法。</param>
        /// <param name="skip">给定跳过的条数。</param>
        /// <param name="take">给定取得的条数。</param>
        /// <returns>返回分页实体列表。</returns>
        public virtual IPageable<T> PagedList(int skip, int take,
            Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null)
        {
            int total;
            return Table.Query(out total, filterFactory, sorterFactory, skip, take).ToPagedList(total, skip, take);
        }
        /// <summary>
        /// 异步分页列表查询。
        /// </summary>
        /// <param name="filterFactory">给定的过滤方法。</param>
        /// <param name="sorterFactory">给定的排序方法。</param>
        /// <param name="skip">给定跳过的条数。</param>
        /// <param name="take">给定取得的条数。</param>
        /// <returns>返回分页实体列表。</returns>
        public virtual async Task<IPageable<T>> PagedListAsync(int skip, int take,
            Func<IQueryable<T>, IQueryable<T>> filterFactory = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory = null)
        {
            // 因异步方法不支持 OUT 参数，所以只能分拆
            int total = await Table.QueryTotalAsync(filterFactory, sorterFactory);
            var rows = await Table.QueryAsync(filterFactory, sorterFactory, skip, take);

            return rows.ToPagedList(total, skip, take);
        }


        #region IRepository<T> Members

        IAccessor IRepository<T>.Accessor
        {
            get { return Accessor; }
            set { Accessor = value; }
        }

        IQueryable<T> IRepository<T>.Table
        {
            get { return Table; }
        }

        T IRepository<T>.Add(T entity)
        {
            return Add(entity);
        }
        async Task<T> IRepository<T>.AddAsync(T entity)
        {
            return await AddAsync(entity);
        }

        T IRepository<T>.Update(T entity)
        {
            return Update(entity);
        }
        async Task<T> IRepository<T>.UpdateAsync(T entity)
        {
            return await UpdateAsync(entity);
        }

        T IRepository<T>.Save(T entity)
        {
            return Save(entity);
        }
        async Task<T> IRepository<T>.SaveAsync(T entity)
        {
            return await SaveAsync(entity);
        }

        T IRepository<T>.Delete(T entity)
        {
            return Delete(entity);
        }
        async Task<T> IRepository<T>.DeleteAsync(T entity)
        {
            return await DeleteAsync(entity);
        }

        int IRepository<T>.Flush()
        {
            return Flush();
        }
        async Task<int> IRepository<T>.FlushAsync()
        {
            return await FlushAsync();
        }

        int IRepository<T>.Count(Expression<Func<T, bool>> predicate)
        {
            return Count(predicate);
        }
        async Task<int> IRepository<T>.CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await CountAsync(predicate);
        }

        T IRepository<T>.Get(Expression<Func<T, bool>> predicate)
        {
            return Get(predicate);
        }
        async Task<T> IRepository<T>.GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAsync(predicate);
        }

        IList<T> IRepository<T>.List(Func<IQueryable<T>, IQueryable<T>> filterFactory,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory)
        {
            return List(filterFactory, sorterFactory);
        }
        async Task<IList<T>> IRepository<T>.ListAsync(Func<IQueryable<T>, IQueryable<T>> filterFactory,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory)
        {
            return await ListAsync(filterFactory, sorterFactory);
        }

        IPageable<T> IRepository<T>.PagedList(int skip, int take,
            Func<IQueryable<T>, IQueryable<T>> filterFactory,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory)
        {
            return PagedList(skip, take, filterFactory, sorterFactory);
        }
        async Task<IPageable<T>> IRepository<T>.PagedListAsync(int skip, int take,
            Func<IQueryable<T>, IQueryable<T>> filterFactory,
            Func<IQueryable<T>, IOrderedQueryable<T>> sorterFactory)
        {
            return await PagedListAsync(skip, take, filterFactory, sorterFactory);
        }

        #endregion


        /// <summary>
        /// 释放访问器资源。
        /// </summary>
        public virtual void Dispose()
        {
            if (!ReferenceEquals(Accessor, null))
            {
                Accessor.Dispose();
            }
        }

    }
}