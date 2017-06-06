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
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibrameStandard.Entity.DbContexts
{
    /// <summary>
    /// SQLServer 数据库上下文读取器。
    /// </summary>
    public class SqlServerDbContextReader : AbstarctDbContext<SqlServerDbContextReader>
    {
        /// <summary>
        /// 构造一个数据库上下文读取器实例。
        /// </summary>
        /// <param name="dbContextOptions">给定的数据上下文选择项。</param>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public SqlServerDbContextReader(DbContextOptions<SqlServerDbContextReader> dbContextOptions,
            ILibrameBuilder builder)
            : base(dbContextOptions, builder)
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
