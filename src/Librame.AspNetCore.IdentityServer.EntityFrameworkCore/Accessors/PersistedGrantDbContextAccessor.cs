﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Extensions.Core.Services;
    using Extensions.Data.Accessors;

    /// <summary>
    /// 持久化授予数据库上下文访问器。
    /// </summary>
    public class PersistedGrantDbContextAccessor : DbContextAccessor, IPersistedGrantDbContextAccessor
    {
        /// <summary>
        /// 构造一个持久化授予数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public PersistedGrantDbContextAccessor(DbContextOptions options)
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

            var storeOptions = ServiceFactory.GetRequiredService<IOptions<OperationalStoreOptions>>().Value;
            modelBuilder.ConfigurePersistedGrantContext(storeOptions);
        }

    }
}
