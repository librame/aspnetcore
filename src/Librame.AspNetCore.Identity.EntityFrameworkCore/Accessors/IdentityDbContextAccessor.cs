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

namespace Librame.AspNetCore.Identity.Accessors
{
    using Builders;
    using Extensions.Core.Services;
    using Extensions.Data.Accessors;
    using Stores;

    /// <summary>
    /// 身份数据库上下文访问器。
    /// </summary>
    public class IdentityDbContextAccessor : IdentityDbContextAccessor<DefaultIdentityRole<string>, DefaultIdentityUser<string>, string>
        , IIdentityDbContextAccessor
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
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class IdentityDbContextAccessor<TRole, TUser, TGenId>
        : IdentityDbContextAccessor<TRole, DefaultIdentityRoleClaim<TGenId>, DefaultIdentityUserRole<TGenId>,
            TUser, DefaultIdentityUserClaim<TGenId>, DefaultIdentityUserLogin<TGenId>, DefaultIdentityUserToken<TGenId>, TGenId>
        , IIdentityDbContextAccessor<TRole, TUser, TGenId>
        where TRole : DefaultIdentityRole<TGenId>
        where TUser : DefaultIdentityUser<TGenId>
        where TGenId : IEquatable<TGenId>
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
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class IdentityDbContextAccessor<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken, TGenId>
        : DbContextAccessor, IIdentityDbContextAccessor<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken>
        where TRole : DefaultIdentityRole<TGenId>
        where TRoleClaim : DefaultIdentityRoleClaim<TGenId>
        where TUserRole : DefaultIdentityUserRole<TGenId>
        where TUser : DefaultIdentityUser<TGenId>
        where TUserClaim : DefaultIdentityUserClaim<TGenId>
        where TUserLogin : DefaultIdentityUserLogin<TGenId>
        where TUserToken : DefaultIdentityUserToken<TGenId>
        where TGenId : IEquatable<TGenId>
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

            var options = ServiceFactory.GetRequiredService<IOptions<IdentityBuilderOptions>>().Value;
            var sourceOptions = ServiceFactory.GetRequiredService<IOptions<IdentityOptions>>().Value;
            var dataProtector = InternalServiceProvider.GetService<IPersonalDataProtector>();

            modelBuilder.ConfigureIdentityStore<TRole, TRoleClaim, TUserRole,
                TUser, TUserClaim, TUserLogin, TUserToken, TGenId>(options, sourceOptions, dataProtector);
        }

    }
}
