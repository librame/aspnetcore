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

namespace LibrameCore.Entity.Repositories
{
    using Utilities;

    /// <summary>
    /// 实体仓库入口。
    /// </summary>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    public class EntityRepositoryEntry<TEntity> : AbstractRepositoryEntry<TEntity>, IRepositoryEntry<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造一个实体框架仓库入口实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public EntityRepositoryEntry(ILibrameBuilder builder, ILogger<AbstractRepositoryEntry<TEntity>> logger)
            : base(builder, logger)
        {
            Provider = GetProvider();
        }


        /// <summary>
        /// 数据库上下文。
        /// </summary>
        protected DbContext Provider { get; }

        /// <summary>
        /// 实体集。
        /// </summary>
        protected DbSet<TEntity> DbSet => Provider.Set<TEntity>();


        /// <summary>
        /// 获取数据库上下文提供程序。
        /// </summary>
        /// <returns>返回提供程序。</returns>
        protected virtual DbContext GetProvider()
        {
            var providerType = Type.GetType(Builder.Options.Entity.EntityProviderTypeName, throwOnError: true);
            typeof(DbContext).CanAssignableFromType(providerType);

            return (DbContext)Builder.GetService(providerType).NotNull(nameof(providerType));
        }


        /// <summary>
        /// 准备管道动作方法。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        protected virtual void ReadyProvider(Action<DbContext, DbSet<TEntity>> action)
        {
            try
            {
                action.NotNull(nameof(action)).Invoke(Provider, DbSet);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.AsInnerMessage());

                return;
            }
        }

        /// <summary>
        /// 准备管道工厂方法。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="factory">给定的工厂方法。</param>
        /// <returns>返回值类型实例。</returns>
        protected virtual TValue ReadyProvider<TValue>(Func<DbContext, DbSet<TEntity>, TValue> factory)
        {
            try
            {
                return factory.NotNull(nameof(factory)).Invoke(Provider, DbSet);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.AsInnerMessage());

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
