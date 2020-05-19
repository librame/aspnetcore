#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Options;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 表配置助手。
    /// </summary>
    public static class TableConfigurationHelper
    {
        /// <summary>
        /// 创建表配置。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="useIdentityServerPrefix">使用身份服务器前缀（可选；默认使用）。</param>
        /// <returns>返回 <see cref="TableConfiguration"/>。</returns>
        public static TableConfiguration Create<TEntity>(bool useIdentityServerPrefix = true)
            where TEntity : class
        {
            var table = TableDescriptor.Create<TEntity>();

            if (useIdentityServerPrefix)
                table.InsertIdentityServerPrefix();

            if (table.Schema.IsEmpty())
                return new TableConfiguration(table.Name);

            return new TableConfiguration(table.Name, table.Schema);
        }

    }
}
