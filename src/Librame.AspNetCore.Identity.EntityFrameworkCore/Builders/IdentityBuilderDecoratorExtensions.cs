#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Identity;
using Librame.AspNetCore.Identity.Builders;
using Librame.AspNetCore.Identity.Protectors;
using Librame.AspNetCore.Identity.Stores;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Options;
using Librame.Extensions.Data.Accessors;
using Librame.Extensions.Data.Builders;
using Librame.Extensions.Encryption.Builders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 身份构建器静态扩展。
    /// </summary>
    public static class IdentityBuilderDecoratorExtensions
    {
        /// <summary>
        /// 添加演示身份扩展（默认使用 <see cref="DefaultIdentityRole{Guid, Guid}"/> 角色与 <see cref="DefaultIdentityUser{Guid, Guid}"/> 用户模型）。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor>
            (this IExtensionBuilder parentBuilder, Action<IdentityBuilderDependency> configureDependency = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependency, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IAccessor
            => parentBuilder.AddIdentity<TAccessor, DefaultIdentityRole<Guid, Guid>,
                DefaultIdentityUser<Guid, Guid>>(configureDependency, builderFactory);

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        public static IIdentityBuilderDecorator AddIdentity<TAccessor, TRole, TUser>
            (this IExtensionBuilder parentBuilder, Action<IdentityBuilderDependency> configureDependency = null,
            Func<IdentityBuilder, IExtensionBuilder, IdentityBuilderDependency, IIdentityBuilderDecorator> builderFactory = null)
            where TAccessor : DbContext, IAccessor
            where TRole : class
            where TUser : class
            => parentBuilder.AddIdentity<IdentityBuilderDependency, TAccessor,
                TRole, TUser>(configureDependency, builderFactory);

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderDecorator"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public static IIdentityBuilderDecorator AddIdentity<TDependency, TAccessor, TRole, TUser>
            (this IExtensionBuilder parentBuilder, Action<TDependency> configureDependency = null,
            Func<IdentityBuilder, IExtensionBuilder, TDependency, IIdentityBuilderDecorator> builderFactory = null)
            where TDependency : IdentityBuilderDependency
            where TAccessor : DbContext, IAccessor
            where TRole : class
            where TUser : class
        {
            if (!parentBuilder.TryGetBuilder<IDataBuilder>(out var dataBuilder))
                throw new NotSupportedException($"You need to register to builder.{nameof(DataBuilderExtensions.AddData)}().");

            if (!parentBuilder.ContainsBuilder<IEncryptionBuilder>())
            {
                parentBuilder
                    .AddEncryption()
                    .AddDeveloperGlobalSigningCredentials();
            }

            // Clear Options Cache
            ConsistencyOptionsCache.TryRemove<IdentityBuilderOptions>();

            // Add Builder Dependency
            var dependency = parentBuilder.AddBuilderDependency(out var dependencyType, configureDependency);
            parentBuilder.Services.TryAddReferenceBuilderDependency<IdentityBuilderDependency>(dependency, dependencyType);

            // Add Dependencies
            var sourceBuilder = parentBuilder.Services
                .AddIdentityCore<TUser>()
                .AddRoles<TRole>()
                .AddEntityFrameworkStores<TAccessor>()
                .AddSignInManager()
                .AddDefaultTokenProviders()
                .AddRoleStore<DefaultRoleStore<TAccessor>>()
                .AddUserStore<DefaultUserStore<TAccessor>>()
                .AddPersonalDataProtection<IdentityLookupProtector, IdentityLookupProtectorKeyRing>();

            sourceBuilder.Services.TryReplaceAll<IdentityErrorDescriber, LocalizationIdentityErrorDescriber>();

            // Create Decorator
            var decorator = builderFactory.NotNullOrDefault(() =>
            {
                return (s, p, d) => new IdentityBuilderDecorator(s, p, d);
            })
            .Invoke(sourceBuilder, parentBuilder, dependency);

            decorator.SetProperty(p => p.DataBuilder, dataBuilder);

            // Configure Decorator
            return decorator;
        }

    }
}
