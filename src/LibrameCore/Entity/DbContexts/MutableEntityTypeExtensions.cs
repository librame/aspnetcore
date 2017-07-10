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

namespace LibrameStandard.Entity.DbContexts
{
    using Utilities;

    /// <summary>
    /// <see cref="IMutableEntityType"/> 静态扩展。
    /// </summary>
    internal static class MutableEntityTypeExtensions
    {
        /// <summary>
        /// 自映射表名。
        /// </summary>
        /// <param name="entityType">给定可变的实体类型。</param>
        /// <param name="logger">给定的记录器。</param>
        public static void MappingTableName(this IMutableEntityType entityType, ILogger logger)
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

            if (logger != null)
            {
                var schemaName = (string.IsNullOrEmpty(annotations.Schema) ? "dbo" : annotations.Schema);

                logger.LogDebug("Mapping entity type {0} to table {1}.{2}",
                    entityType.ClrType.FullName, schemaName, tableName);
            }

            // 设定表名
            annotations.TableName = tableName;
        }

    }
}
