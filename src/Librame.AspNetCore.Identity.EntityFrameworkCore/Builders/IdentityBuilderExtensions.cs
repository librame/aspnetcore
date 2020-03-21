#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Identity;
using Librame.AspNetCore.Identity.Accessors;
using Librame.AspNetCore.Identity.Builders;
using Librame.AspNetCore.Identity.Stores;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 身份构建器静态扩展。
    /// </summary>
    public static class IdentityBuilderExtensions
    {
        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor>(this IExtensionBuilder parentBuilder,
            Action<IdentityBuilderDependency> configureDependency = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependency, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor
            => parentBuilder.AddIdentity<TAccessor,
                DefaultIdentityUser<string>,
                DefaultIdentityRole<string>,
                string>(configureDependency, builderFactory);

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor, TUser, TRole, TGenId>(this IExtensionBuilder parentBuilder,
            Action<IdentityBuilderDependency> configureDependency = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependency, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor<TRole, TUser, TGenId>
            where TUser : class
            where TRole : class
            where TGenId : IEquatable<TGenId>
            => parentBuilder.AddIdentity<TAccessor,
                TRole,
                DefaultIdentityRoleClaim<TGenId>,
                DefaultIdentityUserRole<TGenId>,
                TUser,
                DefaultIdentityUserClaim<TGenId>,
                DefaultIdentityUserLogin<TGenId>,
                DefaultIdentityUserToken<TGenId>>(configureDependency, builderFactory);

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
        /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
        /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
        /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor, TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken>(
            this IExtensionBuilder parentBuilder, Action<IdentityBuilderDependency> configureDependency = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependency, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken>
            where TRole : class
            where TRoleClaim : class
            where TUserRole : class
            where TUser : class
            where TUserClaim : class
            where TUserLogin : class
            where TUserToken : class
            => parentBuilder.AddIdentity<IdentityBuilderDependency,
                TAccessor,
                TRole,
                TRoleClaim,
                TUserRole,
                TUser,
                TUserClaim,
                TUserLogin,
                TUserToken>(configureDependency, builderFactory);

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
        /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
        /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
        /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "parentBuilder")]
        public static IIdentityBuilderDecorator AddIdentity<TDependency, TAccessor, TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken>(
            this IExtensionBuilder parentBuilder, Action<TDependency> configureDependency = null,
            Func<IdentityBuilder, IExtensionBuilder, TDependency, IIdentityBuilderDecorator> builderFactory = null)
            where TDependency : IdentityBuilderDependency, new()
            where TAccessor : DbContext, IIdentityDbContextAccessor<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken>
            where TRole : class
            where TRoleClaim : class
            where TUserRole : class
            where TUser : class
            where TUserClaim : class
            where TUserLogin : class
            where TUserToken : class
        {
            parentBuilder.NotNull(nameof(parentBuilder));

            // Configure Dependencies
            var dependency = configureDependency.ConfigureDependency(parentBuilder);

            // Add Dependencies
            var sourceBuilder = parentBuilder.Services
                .AddIdentityCore<TUser>()
                .AddRoles<TRole>()
                .AddEntityFrameworkStores<TAccessor>()
                .AddSignInManager()
                .AddDefaultTokenProviders()
                .AddRoleStore<DefaultRoleStore<TAccessor>>()
                .AddUserStore<DefaultUserStore<TAccessor>>();

            sourceBuilder.Services.TryReplace<IdentityErrorDescriber, LocalizationIdentityErrorDescriber>();

            // Create Decorator
            var decorator = builderFactory.NotNullOrDefault(() =>
            {
                return (s, p, d) => new IdentityBuilderDecorator(s, p, d);
            })
            .Invoke(sourceBuilder, parentBuilder, dependency);

            // Configure Decorator
            return decorator
                .AddServices();
        }

    }
}
