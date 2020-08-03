#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Extensions.Data;
    using Extensions.Data.Accessors;

    /// <summary>
    /// 持久化授予数据库上下文访问器。
    /// </summary>
    public class PersistedGrantDbContextAccessor : PersistedGrantDbContextAccessor<Guid, int, Guid>
    {
        /// <summary>
        /// 构造一个持久化授予数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PersistedGrantDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 持久化授予数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class PersistedGrantDbContextAccessor<TGenId, TIncremId, TCreatedBy>
        : DataDbContextAccessor<TGenId, TIncremId, TCreatedBy>, IPersistedGrantAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个持久化授予数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected PersistedGrantDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 持久化授予数据集。
        /// </summary>
        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        /// <summary>
        /// 设备代码数据集。
        /// </summary>
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }


        /// <summary>
        /// 持久化授予数据集管理器。
        /// </summary>
        public DbSetManager<PersistedGrant> PersistedGrantsManager
            => PersistedGrants.AsManager();

        /// <summary>
        /// 设备流程代码数据集管理器。
        /// </summary>
        public DbSetManager<DeviceFlowCodes> DeviceFlowCodesManager
            => DeviceFlowCodes.AsManager();


        /// <summary>
        /// 异步保存更改。
        /// </summary>
        /// <returns>返回一个包含整数的异步操作。</returns>
        public virtual Task<int> SaveChangesAsync()
            => base.SaveChangesAsync();


        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var storeOptions = GetService<IOptions<OperationalStoreOptions>>().Value;
            modelBuilder.ConfigurePersistedGrantContext(storeOptions);
        }

    }
}
