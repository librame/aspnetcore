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
using System.Linq;

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 内部访问器模型构建器静态扩展。
    /// </summary>
    internal static class InternalAccessorModelBuilderExtensions
    {
        private class PersonalDataConverter : ValueConverter<string, string>
        {
            public PersonalDataConverter(IPersonalDataProtector protector)
                : base(s => protector.Protect(s), s => protector.Unprotect(s), default)
            {
            }
        }


        /// <summary>
        /// 配置身份实体集合。
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
        /// <param name="coreOptions">给定的 <see cref="StoreOptions"/>。</param>
        /// <param name="dataProtector">给定的 <see cref="IPersonalDataProtector"/>。</param>
        public static void ConfigureIdentityEntities<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken, TGenId>(this ModelBuilder modelBuilder,
            IdentityBuilderOptions options, IdentityOptions coreOptions, IPersonalDataProtector dataProtector)
            where TRole : IdentityRole<TGenId>
            where TRoleClaim : IdentityRoleClaim<TGenId>
            where TUserRole : IdentityUserRole<TGenId>
            where TUser : IdentityUser<TGenId>
            where TUserClaim : IdentityUserClaim<TGenId>
            where TUserLogin : IdentityUserLogin<TGenId>
            where TUserToken : IdentityUserToken<TGenId>
            where TGenId : IEquatable<TGenId>
        {
            var maxKeyLength = coreOptions.Stores?.MaxLengthForKeys ?? 0;
            var encryptPersonalData = coreOptions.Stores?.ProtectPersonalData ?? false;

            PersonalDataConverter converter = null;

            modelBuilder.Entity<TRole>(b =>
            {
                b.ToTable(options.TableSchemas.RoleFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.NormalizedName).HasName().IsUnique();

                b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(p => p.Name).HasMaxLength(256);
                b.Property(p => p.NormalizedName).HasMaxLength(256);

                b.HasMany<TUserRole>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired();
                b.HasMany<TRoleClaim>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired();
            });

            modelBuilder.Entity<TRoleClaim>(b =>
            {
                b.ToTable(options.TableSchemas.RoleClaimFactory);

                b.HasKey(k => k.Id);
            });

            modelBuilder.Entity<TUserRole>(b =>
            {
                b.ToTable(options.TableSchemas.UserRoleFactory);

                b.HasKey(k => new { k.UserId, k.RoleId });
            });

            modelBuilder.Entity<TUser>(b =>
            {
                b.ToTable(options.TableSchemas.UserFactory);

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.NormalizedUserName).HasName().IsUnique();
                b.HasIndex(i => i.NormalizedEmail).HasName();

                b.Property(p => p.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(p => p.UserName).HasMaxLength(256);
                b.Property(p => p.NormalizedUserName).HasMaxLength(256);
                b.Property(p => p.Email).HasMaxLength(256);
                b.Property(p => p.NormalizedEmail).HasMaxLength(256);

                if (encryptPersonalData)
                {
                    converter = new PersonalDataConverter(dataProtector);

                    ConfigureEncryptPersonalData<TUser, ProtectedPersonalDataAttribute>(b, converter);
                }

                b.HasMany<TUserClaim>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                b.HasMany<TUserLogin>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                b.HasMany<TUserToken>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                b.HasMany<TUserRole>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
            });

            modelBuilder.Entity<TUserClaim>(b =>
            {
                b.ToTable(options.TableSchemas.UserClaimFactory);

                b.HasKey(k => k.Id);
            });

            modelBuilder.Entity<TUserLogin>(b =>
            {
                b.ToTable(options.TableSchemas.UserLoginFactory);

                b.HasKey(k => new { k.LoginProvider, k.ProviderKey });

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.LoginProvider).HasMaxLength(maxKeyLength);
                    b.Property(p => p.ProviderKey).HasMaxLength(maxKeyLength);
                }
            });

            modelBuilder.Entity<TUserToken>(b =>
            {
                b.ToTable(options.TableSchemas.UserTokenFactory);

                b.HasKey(k => new { k.UserId, k.LoginProvider, k.Name });

                if (maxKeyLength > 0)
                {
                    b.Property(p => p.LoginProvider).HasMaxLength(maxKeyLength);
                    b.Property(p => p.Name).HasMaxLength(maxKeyLength);
                }

                if (encryptPersonalData)
                {
                    ConfigureEncryptPersonalData<TUserToken, ProtectedPersonalDataAttribute>(b, converter);
                }
            });
        }


        private static void ConfigureEncryptPersonalData<TEntity, TAttribute>(EntityTypeBuilder<TEntity> builder, ValueConverter converter)
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
