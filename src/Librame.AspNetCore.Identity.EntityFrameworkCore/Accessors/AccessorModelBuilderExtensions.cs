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

    static class AccessorModelBuilderExtensions
    {
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
            var mapRelationship = options.Stores?.MapRelationship ?? true;
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

                if (mapRelationship)
                {
                    b.HasMany<TUserRole>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired();
                    b.HasMany<TRoleClaim>().WithOne().HasForeignKey(fk => fk.RoleId).IsRequired();
                }
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

                if (mapRelationship)
                {
                    b.HasMany<TUserClaim>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                    b.HasMany<TUserLogin>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                    b.HasMany<TUserToken>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                    b.HasMany<TUserRole>().WithOne().HasForeignKey(fk => fk.UserId).IsRequired();
                }

                if (encryptPersonalData)
                {
                    converter = new PersonalDataConverter(dataProtector);

                    b.ConfigureEncryptPersonalData(converter);
                }
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
