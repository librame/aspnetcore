#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Linq;

namespace Librame.Entity.Repositories
{
    using Utility;

    /// <summary>
    /// 抽象仓库入口。
    /// </summary>
    /// <typeparam name="TEntity">指定实现自映射接口的实体类型。</typeparam>
    public abstract class AbstractRepositoryEntry<TEntity> : IRepositoryEntry<TEntity>
        where TEntity : class
    {
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
