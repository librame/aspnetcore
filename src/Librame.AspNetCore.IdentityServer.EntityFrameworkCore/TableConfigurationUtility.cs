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
    using Extensions.Data;

    /// <summary>
    /// 表配置实用工具。
    /// </summary>
    public class TableConfigurationUtility
    {
        private static string _tablePrefix = nameof(IdentityServer);


        /// <summary>
        /// 创建表配置。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <returns>返回 <see cref="TableConfiguration"/>。</returns>
        public static TableConfiguration CreateTableConfiguration<TEntity>()
        {
            var descriptor = new TableNameDescriptor<TEntity>();
            descriptor.ChangeBodyName(names => _tablePrefix + names);

            return new TableConfiguration(descriptor.ToString());
        }

    }
}
