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
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 抽象身份用户数据库上下文。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public abstract class AbstractIdentityUserDbContext<TDbContext, TUser> : AbstractIdentityUserDbContext<TDbContext, TUser, string>, IIdentityUserDbContext<TUser>
        where TDbContext : DbContext, IIdentityUserDbContext<TUser>
        where TUser : IdentityUser<string>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractIdentityUserDbContext{TDbContext, TUser}"/> 实例。
        /// </summary>
        /// <param name="trackerContext">给定的 <see cref="IChangeTrackerContext"/>。</param>
        /// <param name="tenantContext">给定的 <see cref="ITenantContext"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{TDbContext}"/>。</param>
        public AbstractIdentityUserDbContext(IChangeTrackerContext trackerContext, ITenantContext tenantContext,
            IOptions<IdentityBuilderOptions> builderOptions, ILogger<TDbContext> logger, DbContextOptions<TDbContext> dbContextOptions)
            : base(trackerContext, tenantContext, builderOptions, logger, dbContextOptions)
        {
        }

    }


    /// <summary>
    /// 抽象身份用户数据库上下文。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public abstract class AbstractIdentityUserDbContext<TDbContext, TUser, TUserId> : AbstractIdentityUserDbContext<TDbContext, TUser, TUserId, IdentityUserClaim<TUserId>, IdentityUserLogin<TUserId>, IdentityUserToken<TUserId>>, IIdentityUserDbContext<TUser, TUserId>
        where TDbContext : DbContext, IIdentityUserDbContext<TUser, TUserId>
        where TUser : IdentityUser<TUserId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractIdentityUserDbContext{TDbContext, TUser, TUserId}"/> 实例。
        /// </summary>
        /// <param name="trackerContext">给定的 <see cref="IChangeTrackerContext"/>。</param>
        /// <param name="tenantContext">给定的 <see cref="ITenantContext"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{TDbContext}"/>。</param>
        public AbstractIdentityUserDbContext(IChangeTrackerContext trackerContext, ITenantContext tenantContext,
            IOptions<IdentityBuilderOptions> builderOptions, ILogger<TDbContext> logger, DbContextOptions<TDbContext> dbContextOptions)
            : base(trackerContext, tenantContext, builderOptions, logger, dbContextOptions)
        {
        }

    }


    /// <summary>
    /// 抽象身份用户数据库上下文。
    /// </summary>
    /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public abstract class AbstractIdentityUserDbContext<TDbContext, TUser, TUserId, TUserClaim, TUserLogin, TUserToken> : AbstractDbContext<TDbContext, IdentityBuilderOptions>, IIdentityUserDbContext<TUser, TUserId, TUserClaim, TUserLogin, TUserToken>
        where TDbContext : DbContext, IIdentityUserDbContext<TUser, TUserId, TUserClaim, TUserLogin, TUserToken>
        where TUser : IdentityUser<TUserId>
        where TUserId : IEquatable<TUserId>
        where TUserClaim : IdentityUserClaim<TUserId>
        where TUserLogin : IdentityUserLogin<TUserId>
        where TUserToken : IdentityUserToken<TUserId>
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractIdentityUserDbContext{TDbContext, TUser, UserId, TUserClaim, TUserLogin, TUserToken}"/> 实例。
        /// </summary>
        /// <param name="trackerContext">给定的 <see cref="IChangeTrackerContext"/>。</param>
        /// <param name="tenantContext">给定的 <see cref="ITenantContext"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{TDbContext}"/>。</param>
        public AbstractIdentityUserDbContext(IChangeTrackerContext trackerContext, ITenantContext tenantContext,
            IOptions<IdentityBuilderOptions> builderOptions, ILogger<TDbContext> logger, DbContextOptions<TDbContext> dbContextOptions)
            : base(trackerContext, tenantContext, builderOptions, logger, dbContextOptions)
        {
        }

        
        /// <summary>
        /// 用户数据集。
        /// </summary>
        public DbSet<TUser> Users { get; set; }

        /// <summary>
        /// 用户声明数据集。
        /// </summary>
        public DbSet<TUserClaim> UserClaims { get; set; }

        /// <summary>
        /// 用户登陆数据集。
        /// </summary>
        public DbSet<TUserLogin> UserLogins { get; set; }

        /// <summary>
        /// 用户令牌数据集。
        /// </summary>
        public DbSet<TUserToken> UserTokens { get; set; }


        private StoreOptions GetStoreOptions() => this.GetService<IDbContextOptions>()
                            .Extensions.OfType<CoreOptionsExtension>()
                            .FirstOrDefault()
                            ?.ApplicationServiceProvider
                            ?.GetRequiredService<IOptions<IdentityOptions>>()
                            ?.Value?.Stores;


        private class PersonalDataConverter : ValueConverter<string, string>
        {
            public PersonalDataConverter(IPersonalDataProtector protector)
                : base(s => protector.Protect(s), s => protector.Unprotect(s), default)
            {
            }
        }


        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var storeOptions = GetStoreOptions();
            var maxKeyLength = storeOptions?.MaxLengthForKeys ?? 0;
            var encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;

            PersonalDataConverter converter = null;

            modelBuilder.Entity<TUser>(b =>
            {
                b.ToTable(BuilderOptions.UserTable ?? new TableOptions<TUser>());

                b.HasKey(u => u.Id);

                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                if (encryptPersonalData)
                {
                    converter = new PersonalDataConverter(this.GetService<IPersonalDataProtector>());

                    ConfigureEncryptPersonalData<TUser, ProtectedPersonalDataAttribute>(b, converter);
                }

                b.HasMany<TUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
                b.HasMany<TUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
                b.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            });

            modelBuilder.Entity<TUserClaim>(b =>
            {
                b.ToTable(BuilderOptions.UserClaimTable ?? new TableOptions<TUserClaim>());

                b.HasKey(u => u.Id);
            });

            modelBuilder.Entity<TUserLogin>(b =>
            {
                b.ToTable(BuilderOptions.UserLoginTable ?? new TableOptions<TUserLogin>());

                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                if (maxKeyLength > 0)
                {
                    b.Property(l => l.LoginProvider).HasMaxLength(maxKeyLength);
                    b.Property(l => l.ProviderKey).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUserToken>(b =>
            {
                b.ToTable(BuilderOptions.UserTokenTable ?? new TableOptions<TUserToken>());

                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                if (maxKeyLength > 0)
                {
                    b.Property(t => t.LoginProvider).HasMaxLength(maxKeyLength);
                    b.Property(t => t.Name).HasMaxLength(maxKeyLength);
                }

                if (encryptPersonalData)
                {
                    ConfigureEncryptPersonalData<TUserToken, ProtectedPersonalDataAttribute>(b, converter);
                }
            });

            base.OnModelCreating(modelBuilder);
        }


        private void ConfigureEncryptPersonalData<TEntity, TAttribute>(EntityTypeBuilder<TEntity> builder, ValueConverter converter)
            where TEntity : class
            where TAttribute : PersonalDataAttribute
        {
            var tokenProps = typeof(TEntity).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(TAttribute)));

            foreach (var p in tokenProps)
            {
                if (p.PropertyType != typeof(string))
                    throw new InvalidOperationException($"[{nameof(TAttribute).TrimEnd(nameof(Attribute))}] only works strings by default.");

                builder.Property(typeof(string), p.Name).HasConversion(converter);
            }
        }

    }
}
