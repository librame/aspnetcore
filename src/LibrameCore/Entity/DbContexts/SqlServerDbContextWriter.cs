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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace LibrameStandard.Entity.DbContexts
{
    /// <summary>
    /// SQLServer 数据库上下文写入器。
    /// </summary>
    public class SqlServerDbContextWriter : AbstarctDbContext<SqlServerDbContextWriter>
    {
        /// <summary>
        /// 构造一个数据库上下文写入器实例。
        /// </summary>
        /// <param name="dbContextOptions">给定的数据上下文选择项。</param>
        /// <param name="options">给定的实体选项。</param>
        /// <param name="logger">给定的记录器。</param>
        public SqlServerDbContextWriter(DbContextOptions<SqlServerDbContextWriter> dbContextOptions,
            IOptions<EntityOptions> options, ILogger<SqlServerDbContextWriter> logger)
            : base(dbContextOptions, options, logger)
        {
        }


        /// <summary>
        /// 映射实体类型。
        /// </summary>
        /// <param name="modelBuilder">给定的模型构建器。</param>
        /// <param name="mappingType">给定的映射实体类型。</param>
        protected override void MappingEntityType(ModelBuilder modelBuilder, Type mappingType)
        {
            var entityType = modelBuilder.Model.AddEntityType(mappingType);

            var entity = entityType.MappingTableName();

            Logger.LogDebug(LibrameCore.Resources.Core.MappingEntityType,
                entityType.ClrType.FullName, entity.schema, entity.table);
        }

    }
}
