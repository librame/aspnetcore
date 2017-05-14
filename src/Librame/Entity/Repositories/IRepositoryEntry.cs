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

namespace Librame.Entity.Repositories
{
    /// <summary>
    /// 仓库入口接口。
    /// </summary>
    /// <typeparam name="TEntity">指定实现自映射接口的实体类型。</typeparam>
    public interface IRepositoryEntry<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 记录器接口。
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 实体绑定接口。
        /// </summary>
        IEntityBinding<TEntity> Binding { get; }


        /// <summary>
        /// 准备查询动作。
        /// </summary>
        /// <param name="action">给定的查询动作。</param>
        void Ready(Action<IQueryable<TEntity>> action);

        /// <summary>
        /// 准备查询工厂方法。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="factory">给定的工厂方法。</param>
        /// <returns>返回值实例。</returns>
        TValue Ready<TValue>(Func<IQueryable<TEntity>, TValue> factory);
    }
}
