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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Data;

    /// <summary>
    /// 身份数据库上下文访问器。
    /// </summary>
    public class IdentityDbContextAccessor : IdentityDbContextAccessor<DefaultIdentityRole, string, DefaultIdentityUser, string>
    {
        /// <summary>
        /// 构造一个身份数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public IdentityDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 身份数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleId">指定的角色标识类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserId">指定的标识类型。</typeparam>
    public class IdentityDbContextAccessor<TRole, TRoleId, TUser, TUserId> : IdentityDbContextAccessor<TRole, TRoleId,
        IdentityRoleClaim<TUserId>, IdentityUserRole<TUserId>,
        TUser, TUserId, IdentityUserClaim<TUserId>, IdentityUserLogin<TUserId>, IdentityUserToken<TUserId>>
        where TRole : IdentityRole<TUserId>
        where TRoleId : IEquatable<TRoleId>
        where TUser : IdentityUser<TUserId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 构造一个身份数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public IdentityDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }
    }


    /// <summary>
    /// 身份数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleId">指定的角色标识类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    public class IdentityDbContextAccessor<TRole, TRoleId, TRoleClaim, TUserRole, TUser, TUserId, TUserClaim, TUserLogin, TUserToken> : DbContextAccessor
        where TRole : IdentityRole<TUserId>
        where TRoleId : IEquatable<TRoleId>
        where TRoleClaim : IdentityRoleClaim<TUserId>
        where TUserRole : IdentityUserRole<TUserId>
        where TUser : IdentityUser<TUserId>
        where TUserId : IEquatable<TUserId>
        where TUserClaim : IdentityUserClaim<TUserId>
        where TUserLogin : IdentityUserLogin<TUserId>
        where TUserToken : IdentityUserToken<TUserId>
    {
        /// <summary>
        /// 构造一个身份数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public IdentityDbContextAccessor(DbContextOptions options)
            : base(options)
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


        /// <summary>
        /// 开始创建模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var options = ServiceProvider.GetRequiredService<IOptions<IdentityBuilderOptions>>().Value;
            var storeOptions = ServiceProvider.GetRequiredService<IOptions<IdentityOptions>>().Value?.Stores;
            var dataProtector = ServiceProvider.GetService<IPersonalDataProtector>();

            modelBuilder.ConfigureIdentityEntities<TRole, TRoleId, TRoleClaim, TUserRole,
                TUser, TUserId, TUserClaim, TUserLogin, TUserToken>(options, storeOptions, dataProtector);
        }
    }
}
