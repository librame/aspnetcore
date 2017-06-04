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
using Microsoft.Extensions.DependencyInjection;

namespace LibrameStandard.Entity
{
    using Utilities;

    /// <summary>
    /// 实体适配器静态扩展。
    /// </summary>
    public static class EntityAdapterExtensions
    {

        ///// <summary>
        ///// 增强基于实体框架的 SQL Server 数据库。
        ///// </summary>
        ///// <param name="adapter">给定的实体适配器接口。</param>
        ///// <param name="connectionString">给定的数据库连接字符串（可选）。</param>
        ///// <returns>返回实体适配器。</returns>
        //public static IEntityAdapter BuildUpEntityFrameworkSqlServer(this IEntityAdapter adapter,
        //    string connectionString = null)
        //{
        //    adapter.NotNull(nameof(adapter));

        //    adapter.Builder.Services.AddEntityFrameworkSqlServer().AddDbContext<SqlServerDbContext>(options =>
        //    {
        //        // 注入连接字符串
        //        options.UseSqlServer(connectionString, sql =>
        //        {
        //            sql.UseRowNumberForPaging();
        //            sql.MaxBatchSize(50);
        //        });
        //    });

        //    return adapter;
        //}


        /// <summary>
        /// 获取 SQLServer 实体仓库。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="adapter">给定的 Librame 构建器接口。</param>
        /// <returns>返回仓库实例。</returns>
        public static IRepository<SqlServerDbContext, TEntity> GetSqlServerRepository<TEntity>(this IEntityAdapter adapter)
            where TEntity : class
        {
            return adapter.GetRepository<SqlServerDbContext, TEntity>();
        }

    }
}
