#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.AspNetCore.Identity.Accessors
{
    using AspNetCore.Identity.Stores;
    using Extensions.Data;
    using Extensions.Data.Accessors;

    /// <summary>
    /// 身份数据库上下文访问器。
    /// </summary>
    public class IdentityDbContextAccessor : IdentityDbContextAccessor<Guid, int, Guid>,
        IIdentityAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityDbContextAccessor"/>。
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
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityDbContextAccessor<TGenId, TIncremId, TCreatedBy>
        : IdentityDbContextAccessor<DefaultIdentityRole<TGenId, TCreatedBy>,
            DefaultIdentityUser<TGenId, TCreatedBy>,
            TGenId, TIncremId, TCreatedBy>, IIdentityAccessor<TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected IdentityDbContextAccessor(DbContextOptions options)
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
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityDbContextAccessor<TRole, TUser, TGenId, TIncremId, TCreatedBy>
        : IdentityDbContextAccessor<TRole,
            DefaultIdentityRoleClaim<TGenId, TCreatedBy>,
            TUser,
            DefaultIdentityUserClaim<TGenId, TCreatedBy>,
            DefaultIdentityUserLogin<TGenId, TCreatedBy>,
            DefaultIdentityUserRole<TGenId, TCreatedBy>,
            DefaultIdentityUserToken<TGenId, TCreatedBy>,
            TGenId, TIncremId, TCreatedBy>, IIdentityAccessor<TRole, TUser, TGenId, TIncremId, TCreatedBy>
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected IdentityDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 身份数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityDbContextAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken, TGenId, TIncremId, TCreatedBy>
        : DataDbContextAccessor<TGenId, TIncremId, TCreatedBy>,
            IIdentityAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken>
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TRoleClaim : DefaultIdentityRoleClaim<TGenId, TCreatedBy>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TUserClaim : DefaultIdentityUserClaim<TGenId, TCreatedBy>
        where TUserLogin : DefaultIdentityUserLogin<TGenId, TCreatedBy>
        where TUserRole : DefaultIdentityUserRole<TGenId, TCreatedBy>
        where TUserToken : DefaultIdentityUserToken<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected IdentityDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


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
        /// 用户角色数据集。
        /// </summary>
        public DbSet<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// 用户令牌数据集。
        /// </summary>
        public DbSet<TUserToken> UserTokens { get; set; }


        /// <summary>
        /// 角色数据集管理器。
        /// </summary>
        public DbSetManager<TRole> RolesManager
            => Roles.AsManager();

        /// <summary>
        /// 角色声明数据集管理器。
        /// </summary>
        public DbSetManager<TRoleClaim> RoleClaimsManager
            => RoleClaims.AsManager();

        /// <summary>
        /// 用户数据集管理器。
        /// </summary>
        public DbSetManager<TUser> UsersManager
            => Users.AsManager();

        /// <summary>
        /// 用户声明数据集管理器。
        /// </summary>
        public DbSetManager<TUserClaim> UserClaimsManager
            => UserClaims.AsManager();

        /// <summary>
        /// 用户登入数据集管理器。
        /// </summary>
        public DbSetManager<TUserLogin> UserLoginsManager
            => UserLogins.AsManager();

        /// <summary>
        /// 用户角色数据集管理器。
        /// </summary>
        public DbSetManager<TUserRole> UserRolesManager
            => UserRoles.AsManager();

        /// <summary>
        /// 用户令牌数据集管理器。
        /// </summary>
        public DbSetManager<TUserToken> UserTokensManager
            => UserTokens.AsManager();


        /// <summary>
        /// 配置模型构建器核心。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreatingCore(ModelBuilder modelBuilder)
        {
            base.OnModelCreatingCore(modelBuilder);
            modelBuilder.ConfigureIdentityStores(this);
        }

    }
}
