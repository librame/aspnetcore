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
using System.Linq;
using System.Linq.Expressions;

namespace LibrameCore.Entity.Repositories
{
    /// <summary>
    /// 实体仓库写入器。
    /// </summary>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    public abstract class EntityRepositoryWriter<TEntity> : EntityRepositoryEntry<TEntity>, IRepositoryWriter<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造一个实体仓库写入器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public EntityRepositoryWriter(ILibrameBuilder builder, ILogger<AbstractRepositoryEntry<TEntity>> logger)
            : base(builder, logger)
        {
        }
        
        ///// <summary>
        ///// 移除被占用的实体。
        ///// </summary>
        ///// <param name="entity">给定的实体。</param>
        ///// <returns>返回布尔值。</returns>
        //protected virtual bool RemoveHolding(TEntity entity)
        //{
        //    return ReadyProvider((p, s) => RemoveHolding(p, entity));
        //}
        //private bool RemoveHolding(IObjectContextAdapter contextAdapter, TEntity entity)
        //{
        //    var objContext = contextAdapter.ObjectContext;
        //    var objSet = objContext.CreateObjectSet<TEntity>();
        //    var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

        //    object foundEntity;
        //    var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
        //    if (exists)
        //    {
        //        // 移除此对象
        //        objContext.Detach(foundEntity);
        //    }

        //    return exists;
        //}


        /// <summary>
        /// 保存。
        /// </summary>
        public virtual void Save()
        {
            ReadyProvider((p, s) => p.SaveChanges());
        }


        /// <summary>
        /// 创建类型实例。
        /// </summary>
        /// <param name="entity">给定的类型实例。</param>
        /// <param name="syncDatabase">是否同步到数据库（可选；默认同步）。</param>
        /// <returns>返回类型实例。</returns>
        public virtual TEntity Create(TEntity entity, bool syncDatabase = true)
        {
            if (ReferenceEquals(entity, null))
                return default(TEntity);

            // Auto SaveChanges
            return ReadyProvider((p, s) =>
            {
                s.Add(entity);

                if (syncDatabase)
                    p.SaveChanges();

                return entity;
            });
        }


        /// <summary>
        /// 更新类型实例。
        /// </summary>
        /// <param name="entity">给定的类型实例。</param>
        /// <param name="syncDatabase">是否同步到数据库（可选；默认同步）。</param>
        /// <returns>返回类型实例。</returns>
        public virtual TEntity Update(TEntity entity, bool syncDatabase = true)
        {
            if (ReferenceEquals(entity, null))
                return default(TEntity);

            return ReadyProvider((p, s) =>
            {
                // 移除被占用的实体
                //RemoveHolding(p, entity);

                // 附加实体
                s.Update(entity);

                if (syncDatabase)
                    p.SaveChanges();

                return entity;
            });
        }

        /// <summary>
        /// 通过更新获取单条符合指定查询表达式的类型实例。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则查询第一条数据）。</param>
        /// <param name="updateAction">给定更新此类型实例的方法。</param>
        /// <returns>返回对象。</returns>
        public virtual TEntity GetByUpdate(Expression<Func<TEntity, bool>> predicate = null, Action<TEntity> updateAction = null)
        {
            return ReadyProvider((p, s) =>
            {
                var entity = default(TEntity);

                // 支持筛选
                if (!ReferenceEquals(predicate, null))
                    entity = s.FirstOrDefault(predicate);
                else
                    entity = s.FirstOrDefault();

                if (!ReferenceEquals(updateAction, null))
                {
                    // 调用更新方法
                    updateAction.Invoke(entity);

                    // 同步数据库
                    p.SaveChanges();
                }

                return entity;
            });
        }


        /// <summary>
        /// 删除类型实例。
        /// </summary>
        /// <param name="entity">给定的类型实例。</param>
        /// <param name="syncDatabase">是否同步到数据库（可选；默认同步）。</param>
        /// <returns>返回类型实例。</returns>
        public virtual TEntity Delete(TEntity entity, bool syncDatabase = true)
        {
            if (ReferenceEquals(entity, null))
                return default(TEntity);

            return ReadyProvider((p, s) =>
            {
                s.Remove(entity);

                if (syncDatabase)
                    p.SaveChanges();

                return entity;
            });
        }


        #region IRepositoryWriter<T>

        void IRepositoryWriter<TEntity>.Save()
        {
            Save();
        }


        TEntity IRepositoryWriter<TEntity>.Create(TEntity entity, bool syncDatabase)
        {
            return Create(entity, syncDatabase);
        }


        TEntity IRepositoryWriter<TEntity>.Update(TEntity entity, bool syncDatabase)
        {
            return Update(entity, syncDatabase);
        }

        TEntity IRepositoryWriter<TEntity>.GetByUpdate(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction)
        {
            return GetByUpdate(predicate, updateAction);
        }


        TEntity IRepositoryWriter<TEntity>.Delete(TEntity entity, bool syncDatabase)
        {
            return Delete(entity, syncDatabase);
        }

        #endregion

    }
}
