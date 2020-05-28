#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Accessors
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 身份访问器模型构建器静态扩展。
    /// </summary>
    public static class IdentityAccessorModelBuilderExtensions
    {
        /// <summary>
        /// 配置身份存储集合。
        /// </summary>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
        /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
        /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
        /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="accessor">给定的数据库上下文访问器。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void ConfigureIdentityStores<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken, TGenId, TIncremId>
            (this ModelBuilder modelBuilder, IdentityDbContextAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken, TGenId, TIncremId> accessor)
            where TRole : DefaultIdentityRole<TGenId>
            where TRoleClaim : DefaultIdentityRoleClaim<TGenId>
            where TUser : DefaultIdentityUser<TGenId>
            where TUserClaim : DefaultIdentityUserClaim<TGenId>
            where TUserLogin : DefaultIdentityUserLogin<TGenId>
            where TUserRole : DefaultIdentityUserRole<TGenId>
            where TUserToken : DefaultIdentityUserToken<TGenId>
            where TGenId : IEquatable<TGenId>
            where TIncremId : IEquatable<TIncremId>
        {
            modelBuilder.NotNull(nameof(modelBuilder));
            accessor.NotNull(nameof(accessor));

            var options = accessor.GetService<IOptions<IdentityBuilderOptions>>().Value;
            var sourceOptions = accessor.GetService<IOptions<IdentityOptions>>().Value;
            
            var encryptPersonalData = sourceOptions.Stores?.ProtectPersonalData ?? false;
            var maxKeyLength = sourceOptions.Stores?.MaxLengthForKeys ?? 0;
            var mapRelationship = options.Stores?.MapRelationship ?? true;
            var useIdentityPrefix = options.Tables.UseIdentityPrefix;

            PersonalDataConverter converter = null;
            if (encryptPersonalData)
            {
                var dataProtector = accessor.GetService<IPersonalDataProtector>();
                converter = new PersonalDataConverter(dataProtector);
            }

            modelBuilder.Entity<TRole>(b =>
            {
                b.ToTable(table =>
                {
                    if (useIdentityPrefix)
                        table.InsertIdentityPrefix();

                    table.Configure(options.Tables.Role);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.NormalizedName).HasName().IsUnique();

                b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(p => p.Name).HasMaxLength(256);
                b.Property(p => p.NormalizedName).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TUserRole>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired();
                    b.HasMany<TRoleClaim>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired();
                }

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TRoleClaim>(b =>
            {
                b.ToTable(table =>
                {
                    if (useIdentityPrefix)
                        table.InsertIdentityPrefix();

                    table.Configure(options.Tables.RoleClaim);
                });

                b.HasKey(k => k.Id);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUserRole>(b =>
            {
                b.ToTable(table =>
                {
                    if (useIdentityPrefix)
                        table.InsertIdentityPrefix();

                    table.Configure(options.Tables.UserRole);
                });

                b.HasKey(k => new { k.UserId, k.RoleId });

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUser>(b =>
            {
                b.ToTable(table =>
                {
                    if (useIdentityPrefix)
                        table.InsertIdentityPrefix();

                    table.Configure(options.Tables.User);
                });

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.NormalizedUserName).HasName().IsUnique();
                b.HasIndex(i => i.NormalizedEmail).HasName();

                b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(p => p.UserName).HasMaxLength(256);
                b.Property(p => p.NormalizedUserName).HasMaxLength(256);
                b.Property(p => p.Email).HasMaxLength(256);
                b.Property(p => p.NormalizedEmail).HasMaxLength(256);

                if (mapRelationship)
                {
                    b.HasMany<TUserClaim>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                    b.HasMany<TUserLogin>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                    b.HasMany<TUserToken>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                    b.HasMany<TUserRole>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                }

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }

                if (encryptPersonalData)
                {
                    b.ConfigurePersonalData(converter);
                }
            });

            modelBuilder.Entity<TUserClaim>(b =>
            {
                b.ToTable(table =>
                {
                    if (useIdentityPrefix)
                        table.InsertIdentityPrefix();

                    table.Configure(options.Tables.UserClaim);
                });

                b.HasKey(k => k.Id);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }

                if (encryptPersonalData)
                {
                    b.ConfigurePersonalData(converter);
                }
            });

            modelBuilder.Entity<TUserLogin>(b =>
            {
                b.ToTable(table =>
                {
                    if (useIdentityPrefix)
                        table.InsertIdentityPrefix();

                    table.Configure(options.Tables.UserLogin);
                });

                b.HasKey(k => new { k.LoginProvider, k.ProviderKey });

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.LoginProvider).HasMaxLength(maxKeyLength);
                    b.Property(p => p.ProviderKey).HasMaxLength(maxKeyLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUserToken>(b =>
            {
                b.ToTable(table =>
                {
                    if (useIdentityPrefix)
                        table.InsertIdentityPrefix();

                    table.Configure(options.Tables.UserToken);
                });

                b.HasKey(k => new { k.UserId, k.LoginProvider, k.Name });

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.LoginProvider).HasMaxLength(maxKeyLength);
                    b.Property(p => p.Name).HasMaxLength(maxKeyLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }

                if (encryptPersonalData)
                {
                    b.ConfigurePersonalData(converter);
                }
            });
        }

    }
}
