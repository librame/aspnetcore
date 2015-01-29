// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data.Models;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Librame.Context.Mvc
{
    /// <summary>
    /// 带泛类型编号的控制器抽象基类。
    /// </summary>
    /// <typeparam name="TDbContext">指定的 <see cref="DbContext"/> 类型。</typeparam>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <typeparam name="TId">指定的实体编号类型。</typeparam>
    /// <author>Librame Pang</author>
    public abstract class ControllerBase<TDbContext, TEntity, TId> : Microsoft.AspNet.Mvc.Controller
        where TDbContext : DbContext
        where TEntity : IdEntity<TId>
    {
        ContextRepository<TDbContext, TEntity, TId> _repository = null;
        /// <summary>
        /// 获取请求上下文数据仓库。
        /// </summary>
        public ContextRepository<TDbContext, TEntity, TId> Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new ContextRepository<TDbContext, TEntity, TId>(Context);

                    BindRepositoryFuncs();
                }

                return _repository;
            }
        }
        /// <summary>
        /// 绑定数据仓库相关处理方法。
        /// </summary>
        protected virtual void BindRepositoryFuncs()
        {
            _repository.IdIsNotNullFunc = IdIsNotNull;
            _repository.IdEqualsFunc = IdEquals;

            _repository.ListPreQueryFunc = ListPreQuery;
            _repository.ListCompleteFunc = ListComplete;
            _repository.PagedListCompleteFunc = PagedListComplete;

            _repository.CreatingPredicateExpressionFunc = CreatingPredicateExpression;
            _repository.CreatingExistsFunc = CreatingExists;

            _repository.UpdatingPredicateExpressionFunc = UpdatingPredicateExpression;
            _repository.UpdatingExistsFunc = UpdatingExists;

            _repository.LogicalDeleteFactoryFunc = LogicalDeleteFactory;
        }


        #region GetSingle

        /// <summary>
        /// 验证编号不为空。
        /// </summary>
        /// <param name="id">给定的编号。</param>
        /// <returns>返回是否为空的布尔值。</returns>
        protected virtual bool IdIsNotNull(TId id)
        {
            return !Equals(id, default(TId));
        }

        /// <summary>
        /// 验证指定编号与当前编号是否相等。
        /// </summary>
        /// <param name="id">给定的编号。</param>
        /// <param name="compareId">给定的对比编号。</param>
        /// <returns>返回是否相等的布尔值。</returns>
        protected virtual bool IdEquals(TId id, TId compareId)
        {
            return Equals(id, compareId);
        }

        #endregion


        #region GetList

        /// <summary>
        /// 自定义列表预查询。
        /// </summary>
        /// <param name="query">给定的当前查询对象。</param>
        /// <returns>返回查询对象（默认不处理，直接返回）。</returns>
        protected virtual IQueryable<TEntity> ListPreQuery(IQueryable<TEntity> query)
        {
            return query;
        }

        /// <summary>
        /// 自定义实体列表完成处理。
        /// </summary>
        /// <param name="list">给定的实体列表。</param>
        /// <returns>返回实体集合（默认不处理，直接返回）。</returns>
        protected virtual IEnumerable<TEntity> ListComplete(IList<TEntity> list)
        {
            return list;
        }

        /// <summary>
        /// 自定义分页实体列表完成处理。
        /// </summary>
        /// <param name="pagedList">给定的分页实体列表。</param>
        /// <returns>返回实体集合（默认不处理，直接返回）。</returns>
        protected virtual IPageable<TEntity> PagedListComplete(IPageable<TEntity> pagedList)
        {
            return pagedList;
        }

        #endregion


        #region Creating

        /// <summary>
        /// 创建实体的断定表达式。
        /// </summary>
        /// <param name="create">给定要创建的实体。</param>
        /// <returns>返回断定表达式（默认返回空）。</returns>
        protected virtual Expression<Func<TEntity, bool>> CreatingPredicateExpression(TEntity create)
        {
            // 联合验证本地化名称与数据范围的唯一性约束
            return null;
        }
        /// <summary>
        /// 判定要创建的实体是否已存在的方法。
        /// </summary>
        /// <remarks>
        /// 如果断定表达式 <see cref="CreatingPredicateExpression(TEntity)"/> 返回空，则此方法将无效。
        /// </remarks>
        /// <param name="result">给定要创建的实体。</param>
        /// <returns>返回布尔值（TRUE 表示已存在，反之则不存在）。</returns>
        protected virtual bool CreatingExists(TEntity result)
        {
            // 验证要创建的数据是否已存在
            return (!ReferenceEquals(null, result) && IdIsNotNull(result.Id));
        }

        #endregion


        #region Updating

        /// <summary>
        /// 更新实体的断定表达式。
        /// </summary>
        /// <param name="create">给定的更新实体。</param>
        /// <returns>返回断定表达式（默认返回空）。</returns>
        protected virtual Expression<Func<TEntity, bool>> UpdatingPredicateExpression(TEntity update)
        {
            // 联合验证本地化名称与数据范围的唯一性约束
            return null;
        }
        /// <summary>
        /// 判定要更新的实体是否已存在的方法。
        /// </summary>
        /// <remarks>
        /// 如果断定表达式 <see cref="UpdatingPredicateExpression(TEntity)"/> 返回空，则此方法将无效。
        /// </remarks>
        /// <param name="result">给定要创建的实体。</param>
        /// <returns>返回布尔值（TRUE 表示已存在，反之则不存在）。</returns>
        protected virtual bool UpdatingExists(TEntity result)
        {
            // 验证要更新的数据是否已存在
            return (!ReferenceEquals(null, result) && IdIsNotNull(result.Id));
        }

        #endregion


        #region Logical Delete

        /// <summary>
        /// 获取逻辑删除工厂方法。
        /// </summary>
        protected virtual void LogicalDeleteFactory(TEntity delete)
        {
            //
        }

        #endregion


        /// <summary>
        /// 释放资源。
        /// </summary>
        /// <param name="disposing">是否立即释放资源。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repository.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}