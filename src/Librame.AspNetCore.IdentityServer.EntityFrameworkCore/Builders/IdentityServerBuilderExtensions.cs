#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.Configuration;
using IdentityServer4.Hosting;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;
    using Extensions.Core;
    using Extensions.Encryption;

    /// <summary>
    /// 身份服务器构建器静态扩展。
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawAction">给定的封装器选项配置动作。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerOptions> rawAction,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependencyOptions, IIdentityServerBuilderWrapper> builderFactory = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TUser : class
            => builder.AddIdentityServer<TIdentityServerAccessor, TIdentityServerAccessor, TUser>(rawAction, builderFactory);

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerBuilderDependencyOptions> dependencyAction = null,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependencyOptions, IIdentityServerBuilderWrapper> builderFactory = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TUser : class
            => builder.AddIdentityServer<TIdentityServerAccessor, TIdentityServerAccessor, TUser>(dependencyAction, builderFactory);


        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TConfigurationAccessor">指定的配置访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawAction">给定的封装器选项配置动作。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TConfigurationAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerOptions> rawAction,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependencyOptions, IIdentityServerBuilderWrapper> builderFactory = null)
            where TConfigurationAccessor : DbContext, IConfigurationDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            return builder.AddIdentityServer<TConfigurationAccessor, TPersistedGrantAccessor, TUser>(dependency =>
            {
                dependency.RawAction = rawAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TConfigurationAccessor">指定的配置访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TConfigurationAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerBuilderDependencyOptions> dependencyAction = null,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependencyOptions, IIdentityServerBuilderWrapper> builderFactory = null)
            where TConfigurationAccessor : DbContext, IConfigurationDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependencyOptions();

            var rawBuilder = builder.Services
                .AddIdentityServer(dependency.RawAction)
                .AddConfigurationStore<TConfigurationAccessor>(dependency.ConfigurationAction)
                .AddOperationalStore<TPersistedGrantAccessor>(dependency.OperationalAction)
                .AddAspNetIdentity<TUser>()
                .ConfigureReplacedServices()
                .AddAuthorization();

            // Add Builder
            builder.Services.OnlyConfigure(dependency.OptionsAction, dependency.OptionsName);

            var builderWrapper = builderFactory.NotNullOrDefault(()
                => (r, b, d) => new IdentityServerBuilderWrapper(typeof(TUser), r, b, d)).Invoke(rawBuilder, builder, dependency);

            return builderWrapper;
        }

        private static IIdentityServerBuilder AddAuthorization(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IEnumerable<IdentityResource>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>();
                return options.Value.Authorizations.IdentityResources;
            });

            builder.Services.AddSingleton<IEnumerable<ApiResource>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>();
                return options.Value.Authorizations.ApiResources;
            });

            builder.Services.AddSingleton<IEnumerable<Client>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>();
                return options.Value.Authorizations.Clients;
            });

            builder.Services.AddSingleton<ISigningCredentialStore>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>();
                if (options.Value.Authorizations.SigningCredentials.IsNull())
                {
                    var service = sp.GetRequiredService<ISigningCredentialsService>();
                    options.Value.Authorizations.SigningCredentials = service.GetGlobalSigningCredentials();
                }
                return new DefaultSigningCredentialsStore(options.Value.Authorizations.SigningCredentials);
            });

            builder.Services.AddSingleton<IValidationKeysStore>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>();
                if (options.Value.Authorizations.SigningCredentials.IsNull())
                {
                    var service = sp.GetRequiredService<ISigningCredentialsService>();
                    options.Value.Authorizations.SigningCredentials = service.GetGlobalSigningCredentials();
                }
                return new DefaultValidationKeysStore(options.Value.Authorizations.SigningCredentials.Key.YieldEnumerable());
            });

            return builder;
        }

        private static IIdentityServerBuilder ConfigureReplacedServices(this IIdentityServerBuilder builder)
        {
            builder.Services.TryAddSingleton<IAbsoluteUrlFactory, AbsoluteUrlFactory>();
            builder.Services.AddSingleton<IRedirectUriValidator, RelativeRedirectUriValidator>();
            builder.Services.AddSingleton<IClientRequestParametersProvider, DefaultClientRequestParametersProvider>();
            ReplaceEndSessionEndpoint(builder);

            return builder;
        }

        private static void ReplaceEndSessionEndpoint(IIdentityServerBuilder builder)
        {
            // We don't have a better way to replace the end session endpoint as far as we know other than looking the descriptor up
            // on the container and replacing the instance. This is due to the fact that we chain on AddIdentityServer which configures the
            // list of endpoints by default.
            var endSessionEndpointDescriptor = builder.Services
                            .Single(s => s.ImplementationInstance is Endpoint e &&
                                    string.Equals(e.Name, "Endsession", StringComparison.OrdinalIgnoreCase) &&
                                    string.Equals("/connect/endsession", e.Path, StringComparison.OrdinalIgnoreCase));

            builder.Services.Remove(endSessionEndpointDescriptor);
            builder.AddEndpoint<AutoRedirectEndSessionEndpoint>("EndSession", "/connect/endsession");
        }

    }
}
