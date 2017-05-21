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
    using Repositories;

    /// <summary>
    /// 实体仓库。
    /// </summary>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    public class EntityRepository<TEntity> : EntityRepositoryReader<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造一个实体仓库实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="logger">给定的记录器接口。</param>
        public EntityRepository(ILibrameBuilder builder, ILogger<AbstractRepositoryEntry<TEntity>> logger)
            : base(builder, logger)
        {
        }

    }
}
