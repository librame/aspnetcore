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

namespace LibrameStandard.Entity
{
    using DbContexts;
    
    /// <summary>
    /// 实体适配器静态扩展。
    /// </summary>
    public static class EntityAdapterExtensions
    {

        /// <summary>
        /// 获取 SQLServer 实体仓库（支持读写分离）。
        /// </summary>
        /// <remarks>
        /// 须注册 IServiceCollection.AddDbContext{DbContexts.SqlServerDbContextReader}、IServiceCollection.AddDbContext{DbContexts.SqlServerDbContextWriter}（可选）
        /// </remarks>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="adapter">给定的 Librame 构建器接口。</param>
        /// <returns>返回仓库实例。</returns>
        public static IRepository<DbContext, DbContext, TEntity> GetSqlServerRepository<TEntity>(this IEntityAdapter adapter)
            where TEntity : class
        {
            if (adapter.Builder.Options.Entity.EnableReadWriteSeparation)
                return adapter.GetRepository<SqlServerDbContextReader, SqlServerDbContextWriter, TEntity>();

            // 默认均使用数据库上下文读取器
            return adapter.GetRepository<SqlServerDbContextReader, SqlServerDbContextReader, TEntity>();
        }

    }
}
