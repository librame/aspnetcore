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

namespace LibrameStandard
{
    using Entity;

    /// <summary>
    /// Librame 构建器核心静态扩展。
    /// </summary>
    public static class LibrameBuilderCoreExtensions
    {

        /// <summary>
        /// 获取 SQLServer 实体仓库（支持读写分离）。
        /// </summary>
        /// <remarks>
        /// 须注册 IServiceCollection.AddDbContext{DbContexts.SqlServerDbContextReader}、IServiceCollection.AddDbContext{DbContexts.SqlServerDbContextWriter}（可选）
        /// </remarks>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回仓库实例。</returns>
        public static IRepository<DbContext, DbContext, TEntity> GetSqlServerRepository<TEntity>(this ILibrameBuilder builder)
            where TEntity : class
        {
            return builder.GetEntityAdapter().GetSqlServerRepository<TEntity>();
        }

    }
}
