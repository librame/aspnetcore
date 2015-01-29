// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data;
using Librame.Data.Context;
using Librame.Data.Models;
using Librame.Utility;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Librame.Context
{
    /// <summary>
    /// 带泛类型编号的上下文数据仓库。
    /// </summary>
    /// <typeparam name="TDbContext">指定的 <see cref="DbContext"/> 类型。</typeparam>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <typeparam name="TId">指定的实体编号类型。</typeparam>
    /// <author>Librame Pang</author>
    public class ContextRepository<TDbContext, TEntity, TId> : IDisposable
        where TDbContext : DbContext
        where TEntity : IdEntity<TId>
    {
        /// <summary>
        /// 构造一个带泛类型编号的上下文数据仓库实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// context 为空。
        /// </exception>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        public ContextRepository(HttpContext context)
        {
            if (ReferenceEquals(context, null))
            {
                throw new ArgumentNullException("context");
            }

            Context = context;

            Initialize();
        }

        /// <summary>
        /// 获取当前 <see cref="HttpContext"/>。
        /// </summary>
        public HttpContext Context { get; private set; }

        /// <summary>
        /// 初始化请求。
        /// </summary>
        protected virtual void Initialize()
        {
            Query = new QueryRequest(Context.Request);
            Accessor = Context.ApplicationServices.CreateAccessor<TDbContext>();
            Entity = Accessor.CreateRepository<TEntity>();
        }

        /// <summary>
        /// 获取当前查询请求。
        /// </summary>
        public QueryRequestBase Query { get; protected set; }
        /// <summary>
        /// 获取当前数据访问器。
        /// </summary>
        public IAccessor Accessor { get; protected set; }
        /// <summary>
        /// 获取当前实体仓库。
        /// </summary>
        public IRepository<TEntity> Entity { get; protected set; }


        #region GetSingle

        /// <summary>
        /// 获取或设置编号不为空的方法。
        /// </summary>
        public Func<TId, bool> IdIsNotNullFunc { get; set; }
        /// <summary>
        /// 获取或设置编号是否相等的方法。
        /// </summary>
        public Func<TId, TId, bool> IdEqualsFunc { get; set; }


        void ValidateSingleFuncs()
        {
            if (ReferenceEquals(IdIsNotNullFunc, null))
                throw new ArgumentNullException("IdIsNotNullFunc");

            if (ReferenceEquals(IdEqualsFunc, null))
                throw new ArgumentNullException("IdEqualsFunc");
        }

        /// <summary>
        /// 获取指定编号的实体。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <see cref="IdIsNotNullFunc"/> and <see cref="IdEqualsFunc"/> 属性为空。
        /// </exception>
        /// <param name="id">给定的编号。</param>
        /// <returns>返回实体或空。</returns>
        public virtual TEntity GetSingle(TId id)
        {
            ValidateSingleFuncs();

            if (IdIsNotNullFunc(id))
            {
                return Entity.Get(p => IdEqualsFunc(p.Id, id));
            }

            return null;
        }
        /// <summary>
        /// 异步获取指定编号的实体。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <see cref="IdIsNotNullFunc"/> and <see cref="IdEqualsFunc"/> 属性为空。
        /// </exception>
        /// <param name="id">给定的编号。</param>
        /// <returns>返回实体或空。</returns>
        public virtual async Task<TEntity> GetSingleAsync(TId id)
        {
            ValidateSingleFuncs();

            if (IdIsNotNullFunc(id))
            {
                return await Entity.GetAsync(p => IdEqualsFunc(p.Id, id));
            }

            return null;
        }

        #endregion


        #region GetList

        /// <summary>
        /// 获取或设置自定义列表预查询方法。
        /// </summary>
        /// <value>默认不处理，直接返回。</value>
        public Func<IQueryable<TEntity>, IQueryable<TEntity>> ListPreQueryFunc { get; set; }
            = (q => q);

        /// <summary>
        /// 获取或设置自定义实体列表完成处理。
        /// </summary>
        /// <value>默认不处理，直接返回。</value>
        public Func<IList<TEntity>, IEnumerable<TEntity>> ListCompleteFunc { get; set; }

        /// <summary>
        /// 获取或设置自定义分页实体列表完成处理。
        /// </summary>
        /// <value>默认不处理，直接返回。</value>
        public Func<IPageable<TEntity>, IPageable<TEntity>> PagedListCompleteFunc { get; set; }
            = (pagedList => pagedList);


        /// <summary>
        /// 获取所有实体列表。
        /// </summary>
        /// <returns>返回实体集合。</returns>
        public virtual IEnumerable<TEntity> GetList()
        {
            var list = Entity.List(fq => ListPreQueryFunc(fq).Filtration(Query.Filter),
                sq => sq.Sorting(Query.Sorter));

            return ListCompleteFunc(list);
        }
        /// <summary>
        /// 异步获取所有实体列表。
        /// </summary>
        /// <returns>返回实体集合。</returns>
        public virtual async Task<IEnumerable<TEntity>> GetListAsync()
        {
            var list = await Entity.ListAsync(fq => ListPreQueryFunc(fq).Filtration(Query.Filter),
                sq => sq.Sorting(Query.Sorter));

            return ListCompleteFunc(list);
        }

        /// <summary>
        /// 获取分页实体列表。
        /// </summary>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的取得条数。</param>
        /// <returns>返回分页实体集合。</returns>
        public virtual IPageable<TEntity> GetPagedList(int skip, int take)
        {
            var pagedList = Entity.PagedList(skip, take,
                fq => ListPreQueryFunc(fq).Filtration(Query.Filter),
                sq => sq.Sorting(Query.Sorter, dsq => dsq.OrderBy(e => e.Id)));

            return PagedListCompleteFunc(pagedList);
        }
        /// <summary>
        /// 异步获取分页实体列表。
        /// </summary>
        /// <param name="skip">给定的跳过条数。</param>
        /// <param name="take">给定的取得条数。</param>
        /// <returns>返回分页实体集合。</returns>
        public virtual async Task<IPageable<TEntity>> GetPagedListAsync(int skip, int take)
        {
            var pagedList = await Entity.PagedListAsync(skip, take,
                fq => ListPreQueryFunc(fq).Filtration(Query.Filter),
                sq => sq.Sorting(Query.Sorter, dsq => dsq.OrderBy(e => e.Id)));

            return PagedListCompleteFunc(pagedList);
        }

        #endregion


        #region Creating

        /// <summary>
        /// 获取或设置创建实体断定表达式的方法。
        /// </summary>
        /// <value>默认返回空，表示不验证创建实体。</value>
        public Func<TEntity, Expression<Func<TEntity, bool>>> CreatingPredicateExpressionFunc { get; set; }
            = null;

        /// <summary>
        /// 获取或设置判定要创建的实体是否已存在的方法。
        /// </summary>
        public Func<TEntity, bool> CreatingExistsFunc { get; set; }
            = (result => (!ReferenceEquals(result, null)));

        /// <summary>
        /// 创建实体集合。
        /// </summary>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual IEnumerable<TEntity> Creating(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (ReferenceEquals(null, entity))
                {
                    continue;
                }

                var create = Entity.Get(CreatingPredicateExpressionFunc(entity));
                if (CreatingExistsFunc(create))
                {
                    // 如果此记录已存在，则跳过
                    continue;
                    //return this.NotPassValidation();
                }
                else
                {
                    // 即时同步数据库
                    Entity.Add(entity);
                }
            }

            return entities;
        }
        /// <summary>
        /// 异步创建实体集合。
        /// </summary>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual async Task<IEnumerable<TEntity>> CreatingAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (ReferenceEquals(null, entity))
                {
                    continue;
                }

                var create = await Entity.GetAsync(CreatingPredicateExpressionFunc(entity));
                if (CreatingExistsFunc(create))
                {
                    // 如果此记录已存在，则跳过
                    continue;
                    //return this.NotPassValidation();
                }
                else
                {
                    // 即时同步数据库
                    await Entity.AddAsync(entity);
                }
            }

            return entities;
        }

        #endregion


        #region Updating

        /// <summary>
        /// 获取或设置更新实体断定表达式的方法。
        /// </summary>
        /// <value>默认返回空，表示不验证创建实体。</value>
        public Func<TEntity, Expression<Func<TEntity, bool>>> UpdatingPredicateExpressionFunc { get; set; }
            = null;

        /// <summary>
        /// 获取或设置判定要更新的实体是否已存在的方法。
        /// </summary>
        public Func<TEntity, bool> UpdatingExistsFunc { get; set; }
            = (result => (!ReferenceEquals(result, null)));

        /// <summary>
        /// 更新实体集合。
        /// </summary>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual IEnumerable<TEntity> Updating(IEnumerable<TEntity> entities)
        {
            bool isUpdated = false;

            foreach (var entity in entities)
            {
                if (ReferenceEquals(null, entity))
                {
                    continue;
                }

                var update = Entity.Get(UpdatingPredicateExpressionFunc(entity));
                if (UpdatingExistsFunc(update))
                {
                    // 如果此记录已存在，则跳过
                    continue;
                    //return this.NotPassValidation();
                }
                else
                {
                    update = Entity.Get(p => IdEqualsFunc(p.Id, entity.Id));
                    // update == entity
                    update.Merge(entity);

                    Entity.Update(update);

                    if (isUpdated == false)
                    {
                        isUpdated = true;
                    }
                }
            }

            if (isUpdated)
            {
                // 批量保存到数据库
                Entity.Flush();
            }

            return entities;
        }
        /// <summary>
        /// 异步更新实体集合。
        /// </summary>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual async Task<IEnumerable<TEntity>> UpdatingAsync(IEnumerable<TEntity> entities)
        {
            bool isUpdated = false;

            foreach (var entity in entities)
            {
                if (ReferenceEquals(null, entity))
                {
                    continue;
                }

                var update = await Entity.GetAsync(UpdatingPredicateExpressionFunc(entity));
                if (UpdatingExistsFunc(update))
                {
                    // 如果此记录已存在，则跳过
                    continue;
                    //return this.NotPassValidation();
                }
                else
                {
                    update = await Entity.GetAsync(p => IdEqualsFunc(p.Id, entity.Id));
                    // update == entity
                    update.Merge(entity);

                    await Entity.UpdateAsync(update);

                    if (isUpdated == false)
                    {
                        isUpdated = true;
                    }
                }
            }

            if (isUpdated)
            {
                // 批量保存到数据库
                await Entity.FlushAsync();
            }

            return entities;
        }

        #endregion


        #region Logical Delete

        /// <summary>
        /// 获取或删除逻辑删除工厂方法。
        /// </summary>
        public Action<TEntity> LogicalDeleteFactoryFunc { get; set; }

        /// <summary>
        /// 逻辑删除实体集合。
        /// </summary>
        /// <remarks>
        /// 通过调用 <see cref="Repository.Update(TEntity)"/> 实现。
        /// </remarks>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual IEnumerable<TEntity> LogicalDelete(IEnumerable<TEntity> entities)
        {
            if (ReferenceEquals(null, LogicalDeleteFactoryFunc))
            {
                bool isDeleted = false;

                foreach (var entity in entities)
                {
                    if (ReferenceEquals(null, entity))
                    {
                        continue;
                    }

                    // 更新被管理的同主键实体对象
                    var destroy = Entity.Get(p => IdEqualsFunc(p.Id, entity.Id));
                    destroy.Merge(entity);

                    // 调用逻辑删除方法
                    LogicalDeleteFactoryFunc(destroy);

                    // 更新数据
                    Entity.Update(destroy);

                    if (isDeleted == false)
                    {
                        isDeleted = true;
                    }
                }

                if (isDeleted)
                {
                    // 需同步数据库
                    Entity.Flush();
                }

                return entities;
            }
            else
            {
                // 不处理
                return entities;
            }
        }
        /// <summary>
        /// 异步逻辑删除实体集合。
        /// </summary>
        /// <remarks>
        /// 通过调用 <see cref="Repository.Update(TEntity)"/> 实现。
        /// </remarks>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual async Task<IEnumerable<TEntity>> LogicalDeleteAsync(IEnumerable<TEntity> entities)
        {
            if (ReferenceEquals(null, LogicalDeleteFactoryFunc))
            {
                bool isDeleted = false;

                foreach (var entity in entities)
                {
                    if (ReferenceEquals(null, entity))
                    {
                        continue;
                    }

                    // 更新被管理的同主键实体对象
                    var destroy = await Entity.GetAsync(p => IdEqualsFunc(p.Id, entity.Id));
                    destroy.Merge(entity);

                    // 调用逻辑删除方法
                    LogicalDeleteFactoryFunc(destroy);

                    // 更新数据
                    await Entity.UpdateAsync(destroy);

                    if (isDeleted == false)
                    {
                        isDeleted = true;
                    }
                }

                if (isDeleted)
                {
                    // 需同步数据库
                    Entity.Flush();
                }

                return entities;
            }
            else
            {
                // 不处理
                return entities;
            }
        }

        #endregion


        #region Physical Delete

        /// <summary>
        /// 物理删除实体集合。
        /// </summary>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual IEnumerable<TEntity> PhysicalDelete(IEnumerable<TEntity> entities)
        {
            bool isDeleted = false;

            foreach (var entity in entities)
            {
                if (ReferenceEquals(null, entity))
                {
                    continue;
                }

                // 更新被管理的同主键实体对象
                var delete = Entity.Get(p => IdEqualsFunc(p.Id, entity.Id));
                delete.Merge(entity);

                Entity.Delete(delete);

                if (isDeleted == false)
                {
                    isDeleted = true;
                }
            }

            if (isDeleted)
            {
                // 需同步数据库
                Entity.Flush();
            }

            return entities;
        }
        /// <summary>
        /// 异步物理删除实体集合。
        /// </summary>
        /// <param name="entities">给定的实体集合。</param>
        /// <returns>返回实体集合。</returns>
        public virtual async Task<IEnumerable<TEntity>> PhysicalDeleteAsync(IEnumerable<TEntity> entities)
        {
            bool isDeleted = false;

            foreach (var entity in entities)
            {
                if (ReferenceEquals(null, entity))
                {
                    continue;
                }

                // 更新被管理的同主键实体对象
                var delete = await Entity.GetAsync(p => IdEqualsFunc(p.Id, entity.Id));
                delete.Merge(entity);

                await Entity.DeleteAsync(delete);

                if (isDeleted == false)
                {
                    isDeleted = true;
                }
            }

            if (isDeleted)
            {
                // 需同步数据库
                await Entity.FlushAsync();
            }

            return entities;
        }

        #endregion


        #region Flush

        /// <summary>
        /// 清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        public virtual int Flush()
        {
            return Entity.Flush();
        }
        /// <summary>
        /// 异步清空实体变化并保存到数据库。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        public virtual async Task<int> FlushAsync()
        {
            return await Entity.FlushAsync();
        }

        #endregion


        #region Dispose

        /// <summary>
        /// 释放资源。
        /// </summary>
        /// <remarks>
        /// 立即释放实体仓库对象资源。
        /// </remarks>
        public virtual void Dispose()
        {
            Entity.Dispose();
        }

        #endregion

    }
}