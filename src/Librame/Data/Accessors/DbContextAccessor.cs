// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Librame.Data.Accessors
{
    /// <summary>
    /// DbContext 访问器。
    /// </summary>
    /// <author>Librame Pang</author>
    public class DbContextAccessor : IAccessor
    {
        private DbContext _context = null;

        /// <summary>
        /// 构造一个 DbContext 访问器。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// context 为空。
        /// </exception>
        /// <param name="context">给定的 <see cref="DbContext"/>。</param>
        public DbContextAccessor(DbContext context)
        {
            if (ReferenceEquals(context, null))
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// 获取当前数据源对象。
        /// </summary>
        /// <value>即 <see cref="DbContext"/>。</value>
        public object Source
        {
            get { return _context; }
        }

        /// <summary>
        /// 获取查询接口。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <returns>返回查询对象。</returns>
        public IQueryable<T> Query<T>() where T : class
        {
            return _context.Set<T>().AsQueryable<T>();
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
        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Query<T>().FirstOrDefault(predicate);
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
        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Query<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        public T Add<T>(T entity) where T : class
        {
            var entry = _context.Add(entity);

            // 新增数据一般需更新 ID，所以集成保存到数据库
            _context.SaveChanges();

            return entry.Entity;
        }
        /// <summary>
        /// 异步增加单个实体。
        /// </summary>
        /// <remarks>
        /// 已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要增加的实体。</param>
        /// <returns>返回实体。</returns>
        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            var entry = await _context.AddAsync(entity);

            // 新增数据一般需更新 ID，所以集成保存到数据库
            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        /// <summary>
        /// 更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public T Update<T>(T entity) where T : class
        {
            var entry = _context.Update(entity);

            return entry.Entity;
        }
        /// <summary>
        /// 异步更新单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            var entry = await Task.FromResult(_context.Update(entity));

            return entry.Entity;
        }

        /// <summary>
        /// 保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public T Save<T>(T entity) where T : class
        {
            var entry = _context.Attach(entity);

            switch (entry.State)
            {
                case EntityState.Added:
                    return Add(entity); // 直接增加到数据库

                case EntityState.Modified:
                    var update = Update(entity);
                    Flush();
                    return update;

                case EntityState.Unknown:
                    return entity; // 暂不处理

                default:
                    return entity;
            }
        }
        /// <summary>
        /// 异步保存单个实体。
        /// </summary>
        /// <remarks>
        /// 支持增加或更新操作，已集成保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public async Task<T> SaveAsync<T>(T entity) where T : class
        {
            var entry = _context.Attach(entity);

            switch (entry.State)
            {
                case EntityState.Added:
                    return await AddAsync(entity);

                case EntityState.Modified:
                    var update = await UpdateAsync(entity);
                    await FlushAsync();
                    return update;

                case EntityState.Unknown:
                    return entity; // 暂不处理

                default:
                    return entity;
            }
        }

        /// <summary>
        /// 删除单个实体。
        /// </summary>
        /// <remarks>
        /// 需自行调用保存到数据库方法 <see cref="Flush()"/>。
        /// </remarks>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="entity">给定要更新的实体。</param>
        /// <returns>返回实体。</returns>
        public T Delete<T>(T entity) where T : class
        {
            var entry = _context.Remove(entity);

            return entry.Entity;
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
        public async Task<T> DeleteAsync<T>(T entity) where T : class
        {
            return await Task.FromResult(Delete(entity));
        }

        /// <summary>
        /// 清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        public int Flush()
        {
            return _context.SaveChanges();
        }
        /// <summary>
        /// 异步清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        public async Task<int> FlushAsync()
        {
            return await _context.SaveChangesAsync();
        }


        #region IAccessor Members

        IQueryable<T> IAccessor.Query<T>()
        {
            return Query<T>();
        }

        T IAccessor.Get<T>(Expression<Func<T, bool>> predicate)
        {
            return Get(predicate);
        }
        async Task<T> IAccessor.GetAsync<T>(Expression<Func<T, bool>> predicate)
        {
            return await GetAsync(predicate);
        }

        T IAccessor.Add<T>(T entity)
        {
            return Add(entity);
        }
        async Task<T> IAccessor.AddAsync<T>(T entity)
        {
            return await AddAsync(entity);
        }

        T IAccessor.Update<T>(T entity)
        {
            return Update(entity);
        }
        async Task<T> IAccessor.UpdateAsync<T>(T entity)
        {
            return await UpdateAsync(entity);
        }

        T IAccessor.Save<T>(T entity)
        {
            return Save(entity);
        }
        async Task<T> IAccessor.SaveAsync<T>(T entity)
        {
            return await SaveAsync(entity);
        }

        T IAccessor.Delete<T>(T entity)
        {
            return Delete(entity);
        }

        int IAccessor.Flush()
        {
            return Flush();
        }
        async Task<int> IAccessor.FlushAsync()
        {
            return await FlushAsync();
        }

        #endregion


        /// <summary>
        /// 释放访问器资源。
        /// </summary>
        public virtual void Dispose()
        {
            if (!ReferenceEquals(_context, null))
            {
                _context.Dispose();
            }
        }

    }
}