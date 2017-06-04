#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard
{
    using Entity;

    /// <summary>
    /// Librame 构建器核心静态扩展。
    /// </summary>
    public static class LibrameBuilderCoreExtensions
    {

        /// <summary>
        /// 获取 SQLServer 实体仓库。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回仓库实例。</returns>
        public static IRepository<SqlServerDbContext, TEntity> GetSqlServerRepository<TEntity>(this ILibrameBuilder builder)
            where TEntity : class
        {
            return builder.GetRepository<SqlServerDbContext, TEntity>();
        }

    }
}
