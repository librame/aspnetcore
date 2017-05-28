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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace LibrameCore
{
    using Utility;

    /// <summary>
    /// 实体适配器静态扩展。
    /// </summary>
    public static class EntityAdapterExtensions
    {
        /// <summary>
        /// 增强基于实体框架的 SQL Server 数据库。
        /// </summary>
        /// <param name="adapter">给定的实体适配器接口。</param>
        /// <param name="connectionString">给定的数据库连接字符串（可选）。</param>
        /// <returns>返回实体适配器。</returns>
        public static Entity.IEntityAdapter BuildUpEntityFrameworkSqlServer(this Entity.IEntityAdapter adapter,
            string connectionString = null)
        {
            adapter.NotNull(nameof(adapter));

            // 如果没有注册数据库提供程序
            if (!adapter.Builder.ContainsService<IDatabaseProvider>())
            {
                adapter.Builder.Services.AddEntityFrameworkSqlServer().AddDbContext<Entity.Providers.DbContextProvider>(options =>
                {
                    // 注入连接字符串
                    options.UseSqlServer(connectionString, sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                });
            }

            return adapter;
        }

    }
}
