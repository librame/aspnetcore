#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard.Entity
{
    using DbContexts;
    using Repositories;

    ///// <summary>
    ///// 实体仓库。
    ///// </summary>
    ///// <typeparam name="TEntity">指定的实体类型。</typeparam>
    //public class SqlServerEntityRepository<TEntity> : EntityRepositoryReader<SqlServerDbContextReader, TEntity>, IRepository<TEntity>
    //    where TEntity : class
    //{
    //    /// <summary>
    //    /// 构造一个实体仓库实例。
    //    /// </summary>
    //    /// <param name="builder">给定的 Librame 构建器接口。</param>
    //    public SqlServerEntityRepository(ILibrameBuilder builder)
    //        : base(builder)
    //    {
    //    }


    //    /// <summary>
    //    /// 仓库写入器。
    //    /// </summary>
    //    public IRepositoryWriter<SqlServerDbContextReader, TEntity> Writer
    //        => Builder.GetService<IRepositoryWriter<SqlServerDbContextReader, TEntity>>();
    //}
}
