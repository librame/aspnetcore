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
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrameStandard.Entity
{
    using Utilities;

    /// <summary>
    /// SQLServer 数据库上下文。
    /// </summary>
    public class SqlServerDbContext : AbstarctDbContext<SqlServerDbContext>
    {
        /// <summary>
        /// 构造一个数据库上下文提供程序实例。
        /// </summary>
        /// <param name="dbContextOptions">给定的数据上下文选择项。</param>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> dbContextOptions,
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
            // 自映射实体类型名的复数形式（仅支持 SQL Server）
            var annotations = entityType.SqlServer();

            // 默认表名规范，复数形式
            var tableName = StringUtility.AsPluralize(entityType.ClrType.Name);

            // 自定义表名规范，属性特性优先
            var tableAttribute = entityType.ClrType.Attribute<TableAttribute>();
            if (tableAttribute != null)
            {
                tableName = tableAttribute.Name;

                if (!string.IsNullOrEmpty(tableAttribute.Schema))
                    annotations.Schema = tableAttribute.Schema;
            }

            Logger.LogDebug("Mapping entity type {0} to table {1}.{2}.",
                entityType.ClrType.FullName, annotations.Schema, annotations.TableName);

            // 设定表名
            annotations.TableName = tableName;
        }

    }
}
