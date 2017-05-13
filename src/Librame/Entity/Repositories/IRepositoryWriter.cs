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
using System.Linq.Expressions;

namespace Librame.Entity.Repositories
{
    /// <summary>
    /// 仓库写入器接口。
    /// </summary>
    /// <typeparam name="TEntity">指定实现自映射接口的实体类型。</typeparam>
    public interface IRepositoryWriter<TEntity> : IRepositoryEntry<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 保存。
        /// </summary>
        void Save();


        /// <summary>
        /// 创建类型实例。
        /// </summary>
        /// <param name="entity">给定的类型实例。</param>
        /// <param name="syncDatabase">是否同步到数据库（可选；默认同步）。</param>
        /// <returns>返回类型实例。</returns>
        TEntity Create(TEntity entity, bool syncDatabase = true);


        /// <summary>
        /// 更新类型实例。
        /// </summary>
        /// <param name="entity">给定的类型实例。</param>
        /// <param name="syncDatabase">是否同步到数据库（可选；默认同步）。</param>
        /// <returns>返回类型实例。</returns>
        TEntity Update(TEntity entity, bool syncDatabase = true);

        /// <summary>
        /// 通过更新获取单条符合指定查询表达式的类型实例。
        /// </summary>
        /// <param name="predicate">给定的查询表达式（可选；如果为空，则查询第一条数据）。</param>
        /// <param name="updateAction">给定更新此类型实例的方法。</param>
        /// <returns>返回类型实例。</returns>
        TEntity GetByUpdate(Expression<Func<TEntity, bool>> predicate = null,
            Action<TEntity> updateAction = null);


        /// <summary>
        /// 删除类型实例。
        /// </summary>
        /// <param name="entity">给定的类型实例。</param>
        /// <param name="syncDatabase">是否同步到数据库（可选；默认同步）。</param>
        /// <returns>返回类型实例。</returns>
        TEntity Delete(TEntity entity, bool syncDatabase = true);
    }
}
