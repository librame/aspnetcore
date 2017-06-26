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
using System.Reflection;

namespace LibrameStandard.Entity
{
    using DbContexts;
    using Utilities;

    /// <summary>
    /// 实体适配器静态扩展。
    /// </summary>
    public static class EntityAdapterExtensions
    {

        ///// <summary>
        ///// 获取实体仓库。
        ///// </summary>
        ///// <remarks>
        ///// 须注册 IServiceCollection.AddDbContext{DbContexts.DbContextReader}。
        ///// </remarks>
        ///// <typeparam name="TEntity">指定的实体类型。</typeparam>
        ///// <returns>返回仓库实例。</returns>
        //public static IRepository<TEntity> GetSqlServerRepository<TEntity>(this IEntityAdapter adapter)
        //    where TEntity : class
        //{
        //    return adapter.GetRepository<TEntity>();
        //}

        //public static IRepository<DbContext, TModel> GetSqlServerRepositoryModel<TModel>(this IEntityAdapter adapter, TModel model)
        //{
        //    var modelType = typeof(TModel);

        //    if (modelType.GetTypeInfo().IsInterface)
        //        modelType = model.NotNull(nameof(model)).GetType();

        //    var dbContextReader = Type.GetType(options.Repository2TypeName, throwOnError: true);

        //    var repositoryType = typeof(IRepository<,>);
        //    repositoryType = repositoryType.MakeGenericType(, modelType);

        //    //IRepository<TDbContext, TEntity>
        //}

        ///// <summary>
        ///// 获取实体仓库。
        ///// </summary>
        ///// <remarks>
        ///// 须注册 IServiceCollection.AddDbContext{DbContexts.SqlServerDbContextReader}。
        ///// </remarks>
        ///// <typeparam name="TEntity">指定的实体类型。</typeparam>
        ///// <returns>返回仓库实例。</returns>
        //public static IRepository<SqlServerDbContextReader, TEntity> GetSqlServerRepository2<TEntity>(this IEntityAdapter adapter)
        //    where TEntity : class
        //{
        //    return adapter.GetRepository<SqlServerDbContextReader, TEntity>();
        //}

        ///// <summary>
        ///// 获取 SQLServer 实体仓库（支持读写分离）。
        ///// </summary>
        ///// <remarks>
        ///// 须注册 IServiceCollection.AddDbContext{DbContexts.SqlServerDbContextReader}、IServiceCollection.AddDbContext{DbContexts.SqlServerDbContextWriter}（可选）
        ///// </remarks>
        ///// <typeparam name="TEntity">指定的实体类型。</typeparam>
        ///// <param name="adapter">给定的 Librame 构建器接口。</param>
        ///// <returns>返回仓库实例。</returns>
        //public static IRepository<DbContext, DbContext, TEntity> GetSqlServerRepository3<TEntity>(this IEntityAdapter adapter)
        //    where TEntity : class
        //{
        //    if (adapter.Builder.Options.Entity.EnableReadWriteSeparation)
        //        return adapter.GetRepository<SqlServerDbContextReader, SqlServerDbContextWriter, TEntity>();

        //    // 默认均使用数据库上下文读取器
        //    return adapter.GetRepository<SqlServerDbContextReader, SqlServerDbContextReader, TEntity>();
        //}

    }
}
