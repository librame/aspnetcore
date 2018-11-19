#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 抽象身份数据库上下文。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public abstract class AbstractIdentityDbContext<TDbContext, TRole, TUser> : AbstractIdentityDbContext<TDbContext, TRole, TUser, string>, IIdentityDbContext<TRole, TUser>
        where TDbContext : DbContext, IIdentityDbContext<TRole, TUser>
        where TRole : IdentityRole<string>
        where TUser : IdentityUser<string>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractIdentityDbContext{TDbContext, TRole, TUser}"/> 实例。
        /// </summary>
        /// <param name="auditResolver">给定的 <see cref="IAuditResolver"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{TDbContext}"/>。</param>
        public AbstractIdentityDbContext(IAuditResolver auditResolver, IOptions<IdentityBuilderOptions> builderOptions,
            ILogger<TDbContext> logger, DbContextOptions<TDbContext> dbContextOptions)
            : base(auditResolver, builderOptions, logger, dbContextOptions)
        {
        }

    }


    /// <summary>
    /// 抽象身份数据库上下文。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public abstract class AbstractIdentityDbContext<TDbContext, TRole, TUser, TId> : AbstractIdentityDbContext<TDbContext, TRole, IdentityRoleClaim<TId>, IdentityUserRole<TId>, TUser, TId, IdentityUserClaim<TId>, IdentityUserLogin<TId>, IdentityUserToken<TId>>, IIdentityDbContext<TRole, TUser, TId>
        where TDbContext : DbContext, IIdentityDbContext<TRole, TUser, TId>
        where TRole : IdentityRole<TId>
        where TUser : IdentityUser<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractIdentityDbContext{TDbContext, TRole, TUser, TId}"/> 实例。
        /// </summary>
        /// <param name="auditResolver">给定的 <see cref="IAuditResolver"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{TDbContext}"/>。</param>
        public AbstractIdentityDbContext(IAuditResolver auditResolver, IOptions<IdentityBuilderOptions> builderOptions,
            ILogger<TDbContext> logger, DbContextOptions<TDbContext> dbContextOptions)
            : base(auditResolver, builderOptions, logger, dbContextOptions)
        {
        }

    }


    /// <summary>
    /// 抽象身份数据库上下文。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public abstract class AbstractIdentityDbContext<TDbContext, TRole, TRoleClaim, TUserRole, TUser, TId, TUserClaim, TUserLogin, TUserToken> : AbstractIdentityUserDbContext<TDbContext, TUser, TId, TUserClaim, TUserLogin, TUserToken>, IIdentityDbContext<TRole, TRoleClaim, TUserRole, TUser, TId, TUserClaim, TUserLogin, TUserToken>
        where TDbContext : DbContext, IIdentityDbContext<TRole, TRoleClaim, TUserRole, TUser, TId, TUserClaim, TUserLogin, TUserToken>
        where TRole : IdentityRole<TId>
        where TRoleClaim : IdentityRoleClaim<TId>
        where TUserRole : IdentityUserRole<TId>
        where TUser : IdentityUser<TId>
        where TId : IEquatable<TId>
        where TUserClaim : IdentityUserClaim<TId>
        where TUserLogin : IdentityUserLogin<TId>
        where TUserToken : IdentityUserToken<TId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractIdentityDbContext{TDbContext, TRole, TRoleClaim, TUserRole, TUser, TUserId, TUserClaim, TUserLogin, TUserToken}"/> 实例。
        /// </summary>
        /// <param name="auditResolver">给定的 <see cref="IAuditResolver"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{TDbContext}"/>。</param>
        public AbstractIdentityDbContext(IAuditResolver auditResolver, IOptions<IdentityBuilderOptions> builderOptions,
            ILogger<TDbContext> logger, DbContextOptions<TDbContext> dbContextOptions)
            : base(auditResolver, builderOptions, logger, dbContextOptions)
        {
        }


        /// <summary>
        /// 用户角色数据集。
        /// </summary>
        public DbSet<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// 角色数据集。
        /// </summary>
        public DbSet<TRole> Roles { get; set; }

        /// <summary>
        /// 角色声明数据集。
        /// </summary>
        public DbSet<TRoleClaim> RoleClaims { get; set; }


        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TUser>(b =>
            {
                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            modelBuilder.Entity<TRole>(b =>
            {
                b.ToTable(BuilderOptions.RoleTable ?? new TableOptions<TRole>());

                b.HasKey(r => r.Id);

                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                b.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany<TRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            modelBuilder.Entity<TRoleClaim>(b =>
            {
                b.ToTable(BuilderOptions.RoleClaimTable ?? new TableOptions<TRoleClaim>());

                b.HasKey(rc => rc.Id);
            });

            modelBuilder.Entity<TUserRole>(b =>
            {
                b.ToTable(BuilderOptions.UserRoleTable ?? new TableOptions<TUserRole>());

                b.HasKey(r => new { r.UserId, r.RoleId });
            });
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
