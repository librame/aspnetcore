#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Librame.Entity.Repositories
{
    using Providers;
    using Utility;

    /// <summary>
    /// 实体框架仓库读取器。
    /// </summary>
    /// <typeparam name="TEntity">指定实现自映射接口的实体类型。</typeparam>
    public class EntityRepositoryReader<TEntity> : EntityRepositoryWriter<TEntity>, IRepositoryReader<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造一个实体框架仓库读取器实例。
        /// </summary>
        /// <param name="provider">给定的数据库上下文提供程序。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public EntityRepositoryReader(DbContextProvider provider, ILogger<AbstractRepositoryEntry<TEntity>> logger)
            : base(provider, logger)
        {
        }


        /// <summary>
        /// 复制类型实例。
        /// </summary>
        /// <param name="source">给定的源类型实例。</param>
        /// <param name="target">给定的目标类型实例。</param>
        public virtual void Copy(TEntity source, TEntity target)
        {
            source.CopyTo(target);
        }


        /// <summary>
        /// 计数查询。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则计算所有条数）。</param>
        /// <returns>返回整数。</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return ReadyProvider((p, s) => s.WhereOrDefault(predicate).Count());
        }


        /// <summary>
        /// 获取单条指定编号的类型实例。
        /// </summary>
        /// <param name="id">给定的编号。</param>
        /// <returns>返回类型实例。</returns>
        public virtual TEntity Get(object id)
        {
            return ReadyProvider((p, s) => s.Find(id));
        }

        /// <summary>
        /// 获取单条符合指定查询表达式的数据。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则根据唯一性要求查询第一条数据）。</param>
        /// <param name="isUnique">是否要求唯一性（如果为 True，则表示查询出多条数据将会抛出异常）。</param>
        /// <returns>返回类型实例。</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate = null, bool isUnique = true)
        {
            return ReadyProvider((p, s) => s.FirstOrSingleOrDefault(predicate, isUnique));
        }


        /// <summary>
        /// 获取多条符合指定查询表达式的数据集合。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则查询所有数据）。</param>
        /// <param name="order">给定的排序方法（可选；如果为空，则采用默认排序）。</param>
        /// <returns>返回数组。</returns>
        public virtual TEntity[] GetMany(Expression<Func<TEntity, bool>> predicate = null, Action<Orderable<TEntity>> order = null)
        {
            return ReadyProvider((p, s) => s.WhereOrDefault(predicate).Order(order).ToArray());
        }


        /// <summary>
        /// 获取分页数据。
        /// </summary>
        /// <param name="createInfoFactory">给定创建分页信息的方法。</param>
        /// <param name="order">给定的排序方法。</param>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则查询所有数据）。</param>
        /// <returns>返回 <see cref="IPagingable{T}"/>。</returns>
        public virtual IPagingable<TEntity> GetPaging(Func<int, PagingInfo> createInfoFactory,
            Action<Orderable<TEntity>> order, Expression<Func<TEntity, bool>> predicate = null)
        {
            return ReadyProvider((p, s) => s.WhereOrDefault(predicate).Paging(order, createInfoFactory));
        }


        /// <summary>
        /// 获取指定属性值集合。
        /// </summary>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="selector">给定的属性选择器。</param>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则根据唯一性要求查询第一条数据）。</param>
        /// <param name="removeDuplicates">是否移除重复项（可选；默认移除）。</param>
        /// <returns>返回数组。</returns>
        public virtual TProperty[] GetProperties<TProperty>(Expression<Func<TEntity, TProperty>> selector,
            Expression<Func<TEntity, bool>> predicate = null, bool removeDuplicates = true)
        {
            return ReadyProvider((p, s) => s.WhereOrDefault(predicate).SelectProperties(selector, removeDuplicates).ToArray());
        }


        #region IRepositoryReader<T>

        void IRepositoryReader<TEntity>.Copy(TEntity source, TEntity target)
        {
            Copy(source, target);
        }

        
        int IRepositoryReader<TEntity>.Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate);
        }

        
        TEntity IRepositoryReader<TEntity>.Get(object id)
        {
            return Get(id);
        }
        
        TEntity IRepositoryReader<TEntity>.Get(Expression<Func<TEntity, bool>> predicate, bool isUnique)
        {
            return Get(predicate, isUnique);
        }


        TEntity[] IRepositoryReader<TEntity>.GetMany(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order)
        {
            return GetMany(predicate, order);
        }


        IPagingable<TEntity> IRepositoryReader<TEntity>.GetPaging(Func<int, PagingInfo> createInfoFactory,
            Action<Orderable<TEntity>> order, Expression<Func<TEntity, bool>> predicate)
        {
            return GetPaging(createInfoFactory, order, predicate);
        }


        TProperty[] IRepositoryReader<TEntity>.GetProperties<TProperty>(Expression<Func<TEntity, TProperty>> selector,
            Expression<Func<TEntity, bool>> predicate, bool removeDuplicates)
        {
            return GetProperties(selector, predicate, removeDuplicates);
        }

        #endregion

    }
}
