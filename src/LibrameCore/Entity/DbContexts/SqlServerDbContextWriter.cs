﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        /// 自映射表名。
        /// </summary>
        /// <param name="entityType">给定可变的实体类型。</param>
        protected override void MappingTableName(IMutableEntityType entityType)
        {
            entityType.MappingTableName(Logger);
        }

    }
}
