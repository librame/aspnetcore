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
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Identity.Accessors
{
    using Extensions;
    using Builders;
    using Stores;

    /// <summary>
    /// 身份访问器模型构建器静态扩展。
    /// </summary>
    public static class IdentityAccessorModelBuilderExtensions
    {
        /// <summary>
        /// 配置身份存储。
        /// </summary>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
        /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
        /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
        /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="IdentityBuilderOptions"/>。</param>
        /// <param name="sourceOptions">给定的 <see cref="IdentityOptions"/>。</param>
        /// <param name="dataProtector">给定的 <see cref="IPersonalDataProtector"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void ConfigureIdentityStore<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken, TGenId>(this ModelBuilder modelBuilder,
            IdentityBuilderOptions options, IdentityOptions sourceOptions, IPersonalDataProtector dataProtector)
            where TRole : DefaultIdentityRole<TGenId>
            where TRoleClaim : DefaultIdentityRoleClaim<TGenId>
            where TUserRole : DefaultIdentityUserRole<TGenId>
            where TUser : DefaultIdentityUser<TGenId>
            where TUserClaim : DefaultIdentityUserClaim<TGenId>
            where TUserLogin : DefaultIdentityUserLogin<TGenId>
            where TUserToken : DefaultIdentityUserToken<TGenId>
            where TGenId : IEquatable<TGenId>
        {
            var mapRelationship = options.Stores?.MapRelationship ?? true;
            var maxKeyLength = sourceOptions.Stores?.MaxLengthForKeys ?? 0;
            var encryptPersonalData = sourceOptions.Stores?.ProtectPersonalData ?? false;
            var trimTableNamePrefix = "Default";

            PersonalDataConverter converter = null;

            modelBuilder.Entity<TRole>(b =>
            {
                b.ToTable(descr => descr.ChangeBodyName(names => names.TrimStart(trimTableNamePrefix)),
                    options.Tables.RoleFactory);

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
                b.ToTable(descr => descr.ChangeBodyName(names => names.TrimStart(trimTableNamePrefix)),
                    options.Tables.RoleClaimFactory);

                b.HasKey(k => k.Id);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUserRole>(b =>
            {
                b.ToTable(descr => descr.ChangeBodyName(names => names.TrimStart(trimTableNamePrefix)),
                    options.Tables.UserRoleFactory);

                b.HasKey(k => new { k.UserId, k.RoleId });

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUser>(b =>
            {
                b.ToTable(descr => descr.ChangeBodyName(names => names.TrimStart(trimTableNamePrefix)),
                    options.Tables.UserFactory);

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
                    converter = new PersonalDataConverter(dataProtector);

                    b.ConfigureEncryptPersonalData(converter);
                }
            });

            modelBuilder.Entity<TUserClaim>(b =>
            {
                b.ToTable(descr => descr.ChangeBodyName(names => names.TrimStart(trimTableNamePrefix)),
                    options.Tables.UserClaimFactory);

                b.HasKey(k => k.Id);

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUserLogin>(b =>
            {
                b.ToTable(descr => descr.ChangeBodyName(names => names.TrimStart(trimTableNamePrefix)),
                    options.Tables.UserLoginFactory);

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
                b.ToTable(descr => descr.ChangeBodyName(names => names.TrimStart(trimTableNamePrefix)),
                    options.Tables.UserTokenFactory);

                b.HasKey(k => new { k.UserId, k.LoginProvider, k.Name });

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.LoginProvider).HasMaxLength(maxKeyLength);
                    b.Property(p => p.Name).HasMaxLength(maxKeyLength);
                    b.Property(p => p.CreatedBy).HasMaxLength(maxKeyLength);
                }

                if (encryptPersonalData)
                {
                    b.ConfigureEncryptPersonalData(converter);
                }
            });
        }

        private static void ConfigureEncryptPersonalData<TEntity>(this EntityTypeBuilder<TEntity> builder, ValueConverter converter)
            where TEntity : class
        {
            builder.ConfigureEncryptPersonalData<TEntity, ProtectedPersonalDataAttribute>(converter);
        }

        private static void ConfigureEncryptPersonalData<TEntity, TAttribute>(this EntityTypeBuilder<TEntity> builder, ValueConverter converter)
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
