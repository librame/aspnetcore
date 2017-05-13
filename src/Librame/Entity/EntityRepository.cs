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

namespace Librame.Entity
{
    using Providers;
    using Repositories;

    /// <summary>
    /// 实体仓库。
    /// </summary>
    /// <typeparam name="TEntity">指定实现自映射接口的实体类型。</typeparam>
    public class EntityRepository<TEntity> : EntityRepositoryReader<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造一个 <see cref="EntityRepository{TEntity}"/> 实例。
        /// </summary>
        /// <param name="provider">给定的数据库上下文提供程序。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public EntityRepository(DbContextProvider provider, ILogger<AbstractRepositoryEntry<TEntity>> logger)
            : base(provider, logger)
        {
        }

    }
}
