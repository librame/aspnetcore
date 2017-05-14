#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Librame.Entity.Repositories
{
    using Providers;
    using Utility;

    /// <summary>
    /// 实体框架仓库入口。
    /// </summary>
    /// <typeparam name="TEntity">指定实现自映射接口的实体类型。</typeparam>
    public class EntityRepositoryEntry<TEntity> : AbstractRepositoryEntry<TEntity>, IRepositoryEntry<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造一个实体框架仓库入口实例。
        /// </summary>
        /// <param name="provider">给定的数据库上下文提供程序。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public EntityRepositoryEntry(DbContextProvider provider, ILogger<AbstractRepositoryEntry<TEntity>> logger)
            : base(logger)
        {
            Provider = provider.NotNull(nameof(provider));
        }
        
        /// <summary>
        /// 数据上下文提供程序。
        /// </summary>
        protected DbContextProvider Provider { get; }

        /// <summary>
        /// 实体集。
        /// </summary>
        protected DbSet<TEntity> DbSet => Provider.Set<TEntity>();
        
        /// <summary>
        /// 准备管道动作方法。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        protected virtual void ReadyProvider(Action<DbContextProvider, DbSet<TEntity>> action)
        {
            try
            {
                action.NotNull(nameof(action)).Invoke(Provider, DbSet);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.InnerMessage());

                return;
            }
        }

        /// <summary>
        /// 准备管道工厂方法。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="factory">给定的工厂方法。</param>
        /// <returns>返回值类型实例。</returns>
        protected virtual TValue ReadyProvider<TValue>(Func<DbContextProvider, DbSet<TEntity>, TValue> factory)
        {
            try
            {
                return factory.NotNull(nameof(factory)).Invoke(Provider, DbSet);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.InnerMessage());

                return default(TValue);
            }
        }


        /// <summary>
        /// 准备查询动作。
        /// </summary>
        /// <param name="action">给定的查询动作。</param>
        public override void Ready(Action<IQueryable<TEntity>> action)
        {
            action.NotNull(nameof(action));

            ReadyProvider((p, s) => action.Invoke(s));
        }

        /// <summary>
        /// 准备查询工厂方法。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="factory">给定的工厂方法。</param>
        /// <returns>返回值实例。</returns>
        public override TValue Ready<TValue>(Func<IQueryable<TEntity>, TValue> factory)
        {
            factory.NotNull(nameof(factory));

            return ReadyProvider((p, s) =>
            {
                return factory.Invoke(s);
            });
        }


        #region IRepositoryEntry<T>

        void IRepositoryEntry<TEntity>.Ready(Action<IQueryable<TEntity>> action)
        {
            Ready(action);
        }

        TValue IRepositoryEntry<TEntity>.Ready<TValue>(Func<IQueryable<TEntity>, TValue> factory)
        {
            return Ready(factory);
        }

        #endregion

    }
}
