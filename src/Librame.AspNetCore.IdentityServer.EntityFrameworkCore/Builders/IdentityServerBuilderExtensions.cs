#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Librame.AspNetCore.Identity.Stores;
using Librame.AspNetCore.IdentityServer.Accessors;
using Librame.AspNetCore.IdentityServer.Builders;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Options;
using Librame.Extensions.Encryption.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 身份服务器构建器静态扩展。
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureIdentityServer">给定的配置身份服务器选项动作方法。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        public static IIdentityServerBuilderDecorator AddIdentityServer(this IExtensionBuilder parentBuilder,
            Action<IdentityServerOptions> configureIdentityServer,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependency, IIdentityServerBuilderDecorator> builderFactory = null)
            => parentBuilder.AddIdentityServer<DefaultIdentityUser<Guid>>(configureIdentityServer, builderFactory);

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureIdentityServer">给定的配置身份服务器选项动作方法。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        public static IIdentityServerBuilderDecorator AddIdentityServer<TUser>(this IExtensionBuilder parentBuilder,
            Action<IdentityServerOptions> configureIdentityServer,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependency, IIdentityServerBuilderDecorator> builderFactory = null)
            where TUser : class
        {
            return parentBuilder.AddIdentityServer<TUser>(dependency =>
            {
                dependency.IdentityServer = configureIdentityServer;
            },
            builderFactory);
        }


        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        public static IIdentityServerBuilderDecorator AddIdentityServer(this IExtensionBuilder parentBuilder,
            Action<IdentityServerBuilderDependency> configureDependency = null,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependency, IIdentityServerBuilderDecorator> builderFactory = null)
            => parentBuilder.AddIdentityServer<DefaultIdentityUser<Guid>>(configureDependency, builderFactory);

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        public static IIdentityServerBuilderDecorator AddIdentityServer<TUser>(this IExtensionBuilder parentBuilder,
            Action<IdentityServerBuilderDependency> configureDependency = null,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependency, IIdentityServerBuilderDecorator> builderFactory = null)
            where TUser : class
            => parentBuilder.AddIdentityServer<IdentityServerBuilderDependency, TUser>(configureDependency, builderFactory);

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IIdentityServerBuilderDecorator AddIdentityServer<TDependency, TUser>(this IExtensionBuilder parentBuilder,
            Action<TDependency> configureDependency = null,
            Func<IIdentityServerBuilder, IExtensionBuilder, TDependency, IIdentityServerBuilderDecorator> builderFactory = null)
            where TDependency : IdentityServerBuilderDependency
            where TUser : class
        {
            // Clear Options Cache
            ConsistencyOptionsCache.TryRemove<IdentityServerBuilderOptions>();

            // Add Builder Dependency
            var dependency = parentBuilder.AddBuilderDependency(out var dependencyType, configureDependency);
            parentBuilder.Services.TryAddReferenceBuilderDependency<IdentityServerBuilderDependency>(dependency, dependencyType);

            // Add Dependencies
            var sourceBuilder = parentBuilder.Services
                .AddIdentityServer(dependency.IdentityServer)
                //.ReplacedServices()
                .AddAspNetIdentity<TUser>();

            // AddConfigurationStore 与 AddOperationalStore 没有配置为选项服务，所以需要手动配置
            parentBuilder.Services.Configure(dependency.ConfigurationAction);
            parentBuilder.Services.Configure(dependency.OperationalAction);

            // Create Decorator
            var decorator = builderFactory.NotNullOrDefault(() =>
            {
                return (s, p, d) => new IdentityServerBuilderDecorator(typeof(TUser), s, p, d);
            })
            .Invoke(sourceBuilder, parentBuilder, dependency);

            return decorator;
        }

        //private static IIdentityServerBuilder ReplacedServices(this IIdentityServerBuilder builder)
        //{
        //    builder.AddCorsPolicyService<CorsPolicyService>();

        //    builder.Services.AddSingleton<IAbsoluteUrlFactory, AbsoluteUrlFactory>();
        //    builder.Services.AddSingleton<IRedirectUriValidator, RelativeRedirectUriValidator>();
        //    builder.Services.AddSingleton<IClientRequestParametersProvider, DefaultClientRequestParametersProvider>();

        //    ReplaceEndSessionEndpoint();

        //    return builder;

        //    void ReplaceEndSessionEndpoint()
        //    {
        //        // We don't have a better way to replace the end session endpoint as far as we know other than looking the descriptor up
        //        // on the container and replacing the instance. This is due to the fact that we chain on AddIdentityServer which configures the
        //        // list of endpoints by default.
        //        var endSessionEndpointDescriptor = builder.Services
        //            .Single(s => s.ImplementationInstance is Endpoint e
        //                && string.Equals(e.Name, "Endsession", StringComparison.OrdinalIgnoreCase)
        //                && string.Equals("/connect/endsession", e.Path, StringComparison.OrdinalIgnoreCase));

        //        builder.Services.Remove(endSessionEndpointDescriptor);
        //        builder.AddEndpoint<AutoRedirectEndSessionEndpoint>("EndSession", "/connect/endsession");
        //    }
        //}


        /// <summary>
        /// 添加数据库上下文访问器存储集合。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IIdentityServerBuilderDecorator"/>。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        public static IIdentityServerBuilderDecorator AddAccessorStores<TIdentityServerAccessor>(this IIdentityServerBuilderDecorator builder)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            => builder.AddAccessorStores<TIdentityServerAccessor, TIdentityServerAccessor>();

        /// <summary>
        /// 添加数据库上下文访问器存储集合。
        /// </summary>
        /// <typeparam name="TConfigurationAccessor">指定的配置访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IIdentityServerBuilderDecorator"/>。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IIdentityServerBuilderDecorator AddAccessorStores<TConfigurationAccessor, TPersistedGrantAccessor>(this IIdentityServerBuilderDecorator builder)
            where TConfigurationAccessor : DbContext, IConfigurationDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
        {
            builder.NotNull(nameof(builder));

            var dependency = builder.Dependency as IdentityServerBuilderDependency;

            // AddConfigurationStore
            builder.Source.AddConfigurationStore<TConfigurationAccessor>(dependency.ConfigurationAction);

            // AddOperationalStore
            builder.Source.AddOperationalStore<TPersistedGrantAccessor>(dependency.OperationalAction);

            return builder;
        }

        /// <summary>
        /// 添加内存存储集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityServerBuilderDecorator"/>。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IIdentityServerBuilderDecorator AddInMemoryStores(this IIdentityServerBuilderDecorator builder)
        {
            builder.NotNull(nameof(builder));

            builder.Source.AddClientStore<InMemoryClientStore>();
            builder.Source.AddResourceStore<InMemoryResourcesStore>();

            builder.AddService<IEnumerable<IdentityResource>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                return options.Authorizations.IdentityResources;
            });
            builder.AddService<IEnumerable<ApiResource>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                return options.Authorizations.ApiResources;
            });
            builder.AddService<IEnumerable<Client>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                return options.Authorizations.Clients;
            });

            var existingCors = builder.Services.Where(x => x.ServiceType == typeof(ICorsPolicyService)).LastOrDefault();
            if (existingCors != null &&
                existingCors.ImplementationType == typeof(DefaultCorsPolicyService) &&
                existingCors.Lifetime == ServiceLifetime.Transient)
            {
                // if our default is registered, then overwrite with the InMemoryCorsPolicyService
                // otherwise don't overwrite with the InMemoryCorsPolicyService, which uses the custom one registered by the host
                builder.AddService<ICorsPolicyService, InMemoryCorsPolicyService>();
            }

            return builder;
        }

    }
}
