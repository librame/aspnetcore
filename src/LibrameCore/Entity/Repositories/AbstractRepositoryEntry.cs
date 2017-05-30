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

namespace LibrameCore.Entity.Repositories
{
    using Utilities;

    /// <summary>
    /// 抽象仓库入口。
    /// </summary>
    /// <typeparam name="TEntity">指定实现自映射接口的实体类型。</typeparam>
    public abstract class AbstractRepositoryEntry<TEntity> : IRepositoryEntry<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造一个抽象仓库入口实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public AbstractRepositoryEntry(ILibrameBuilder builder, ILogger<AbstractRepositoryEntry<TEntity>> logger)
        {
            Builder = builder.NotNull(nameof(builder));
            Logger = logger.NotNull(nameof(logger));
        }


        /// <summary>
        /// Librame 构建器接口。
        /// </summary>
        public ILibrameBuilder Builder { get; }

        /// <summary>
        /// 记录器接口。
        /// </summary>
        public ILogger Logger { get; }


        private IEntityBinding<TEntity> _binding;
        /// <summary>
        /// 实体绑定接口。
        /// </summary>
        public IEntityBinding<TEntity> Binding
        {
            get
            {
                if (_binding == null)
                    _binding = new EntityBinding<TEntity>();

                return _binding;
            }
            set
            {
                _binding = value.NotNull(nameof(value));
            }
        }


        /// <summary>
        /// 准备查询动作。
        /// </summary>
        /// <param name="action">给定的查询动作。</param>
        public abstract void Ready(Action<IQueryable<TEntity>> action);

        /// <summary>
        /// 准备查询工厂方法。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="factory">给定的工厂方法。</param>
        /// <returns>返回值实例。</returns>
        public abstract TValue Ready<TValue>(Func<IQueryable<TEntity>, TValue> factory);


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
