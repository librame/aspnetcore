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
    using Extensions.Data.Accessors;

    /// <summary>
    /// 配置数据库上下文访问器。
    /// </summary>
    public class ConfigurationDbContextAccessor : ConfigurationDbContextAccessor<Guid, int, Guid>
    {
        /// <summary>
        /// 构造一个配置数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public ConfigurationDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 配置数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class ConfigurationDbContextAccessor<TGenId, TIncremId, TCreatedBy>
        : DbContextAccessor<TGenId, TIncremId, TCreatedBy>, IConfigurationDbContextAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个配置数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public ConfigurationDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 客户端数据集。
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// 身份资源数据集。
        /// </summary>
        public DbSet<IdentityResource> IdentityResources { get; set; }

        /// <summary>
        /// API 资源数据集。
        /// </summary>
        public DbSet<ApiResource> ApiResources { get; set; }


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

            var storeOptions = GetService<IOptions<ConfigurationStoreOptions>>().Value;
            modelBuilder.ConfigureClientContext(storeOptions);
            modelBuilder.ConfigureResourcesContext(storeOptions);
        }

    }
}
