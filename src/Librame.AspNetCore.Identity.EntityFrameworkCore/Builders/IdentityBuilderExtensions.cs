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
using Librame.Extensions;
using Librame.Extensions.Core;
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
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="identityAction">给定的身份选项配置动作。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor>(this IExtensionBuilder builder,
            Action<IdentityOptions> identityAction,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependencyOptions, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor
        {
            return builder.AddIdentity<TAccessor,
                DefaultIdentityUser<string>,
                DefaultIdentityRole<string>,
                string>(identityAction, builderFactory);
        }

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="identityAction">给定的身份选项配置动作。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor, TUser, TRole, TGenId>(this IExtensionBuilder builder,
            Action<IdentityOptions> identityAction,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependencyOptions, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor<TRole, TUser, TGenId>
            where TUser : class
            where TRole : class
            where TGenId : IEquatable<TGenId>
        {
            return builder.AddIdentity<TAccessor,
                TRole,
                DefaultIdentityRoleClaim<TGenId>,
                DefaultIdentityUserRole<TGenId>,
                TUser,
                DefaultIdentityUserClaim<TGenId>,
                DefaultIdentityUserLogin<TGenId>,
                DefaultIdentityUserToken<TGenId>>(dependency =>
                {
                    dependency.Identity.Action = identityAction;
                },
                builderFactory);
        }


        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor>(this IExtensionBuilder builder,
            Action<IdentityBuilderDependencyOptions> dependencyAction = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependencyOptions, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor
        {
            return builder.AddIdentity<TAccessor,
                DefaultIdentityUser<string>,
                DefaultIdentityRole<string>,
                string>(dependencyAction, builderFactory);
        }

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor, TUser, TRole, TGenId>(this IExtensionBuilder builder,
            Action<IdentityBuilderDependencyOptions> dependencyAction = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependencyOptions, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor<TRole, TUser, TGenId>
            where TUser : class
            where TRole : class
            where TGenId : IEquatable<TGenId>
        {
            return builder.AddIdentity<TAccessor,
                TRole,
                DefaultIdentityRoleClaim<TGenId>,
                DefaultIdentityUserRole<TGenId>,
                TUser,
                DefaultIdentityUserClaim<TGenId>,
                DefaultIdentityUserLogin<TGenId>,
                DefaultIdentityUserToken<TGenId>>(dependencyAction, builderFactory);
        }

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
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IIdentityBuilderDecorator AddIdentity<TAccessor, TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken>(this IExtensionBuilder builder,
            Action<IdentityBuilderDependencyOptions> dependencyAction = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependencyOptions, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor<TRole, TRoleClaim, TUserRole, TUser, TUserClaim, TUserLogin, TUserToken>
            where TRole : class
            where TRoleClaim : class
            where TUserRole : class
            where TUser : class
            where TUserClaim : class
            where TUserLogin : class
            where TUserToken : class
        {
            builder.NotNull(nameof(builder));

            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependency();
            builder.Services.AddAllOptionsConfigurators(dependency);

            // Configure Dependencies
            var source = builder.Services
                .AddIdentityCore<TUser>(dependency.Identity.Action)
                .AddRoles<TRole>()
                .AddEntityFrameworkStores<TAccessor>()
                .AddSignInManager()
                .AddDefaultTokenProviders()
                .AddRoleStore<DefaultRoleStore<TAccessor>>()
                .AddUserStore<DefaultUserStore<TAccessor>>();

            source.Services.TryReplace<IdentityErrorDescriber, LocalizationIdentityErrorDescriber>();

            // Create Decorator
            var decorator = builderFactory.NotNullOrDefault(() =>
            {
                return (s, b, d) => new IdentityBuilderDecorator(s, b, d);
            })
            .Invoke(source, builder, dependency);

            // Configure Decorator
            return decorator
                .AddServices();
        }

    }
}
