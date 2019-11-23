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
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Librame.AspNetCore.IdentityServer;
using Librame.Extensions;
using Librame.Extensions.Core;
using Librame.Extensions.Encryption;
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
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawAction">给定的封装器选项配置动作。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        public static IIdentityServerBuilderDecorator AddIdentityServer<TUser>(this IExtensionBuilder builder,
            Action<IdentityServerOptions> rawAction,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependencyOptions, IIdentityServerBuilderDecorator> builderFactory = null)
            where TUser : class
        {
            return builder.AddIdentityServer<TUser>(dependency =>
            {
                dependency.IdentityServer = rawAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderDecorator"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IIdentityServerBuilderDecorator AddIdentityServer<TUser>(this IExtensionBuilder builder,
            Action<IdentityServerBuilderDependencyOptions> dependencyAction = null,
            Func<IIdentityServerBuilder, IExtensionBuilder, IdentityServerBuilderDependencyOptions, IIdentityServerBuilderDecorator> builderFactory = null)
            where TUser : class
        {
            builder.NotNull(nameof(builder));

            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependency();
            builder.Services.AddAllOptionsConfigurators(dependency);

            // Configure Dependencies
            var source = builder.Services
                .AddIdentityServer(dependency.IdentityServer)
                //.ReplacedServices()
                .AddAspNetIdentity<TUser>()
                .AddEncryptionSigningCredential();

            // AddConfigurationStore 与 AddOperationalStore 没有配置为选项服务，所以需要手动配置
            builder.Services.Configure(dependency.ConfigurationAction);
            builder.Services.Configure(dependency.OperationalAction);

            // Create Decorator
            var decorator = builderFactory.NotNullOrDefault(() =>
            {
                return (s, b, d) => new IdentityServerBuilderDecorator(typeof(TUser), s, b, d);
            })
            .Invoke(source, builder, dependency);

            return decorator;
        }

        private static IIdentityServerBuilder AddEncryptionSigningCredential(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<ISigningCredentialStore>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                if (options.Authorizations.SigningCredentials.IsNull())
                {
                    var service = provider.GetRequiredService<ISigningCredentialsService>();
                    options.Authorizations.SigningCredentials = service.GetGlobalSigningCredentials();
                }
                return new DefaultSigningCredentialsStore(options.Authorizations.SigningCredentials);
            });

            builder.Services.AddSingleton<IValidationKeysStore>(provider =>
            {
                var store = provider.GetRequiredService<ISigningCredentialStore>();
                var credentials = store.GetSigningCredentialsAsync().ConfigureAndResult();

                var keyInfo = new SecurityKeyInfo
                {
                    Key = credentials.Key,
                    SigningAlgorithm = SecurityAlgorithms.RsaSha256
                };
                return new DefaultValidationKeysStore(keyInfo.YieldEnumerable());
            });

            return builder;
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
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IIdentityServerBuilderDecorator AddAccessorStores<TConfigurationAccessor, TPersistedGrantAccessor>(this IIdentityServerBuilderDecorator builder)
            where TConfigurationAccessor : DbContext, IConfigurationDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
        {
            builder.NotNull(nameof(builder));

            var dependency = builder.DependencyOptions as IdentityServerBuilderDependencyOptions;

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
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IIdentityServerBuilderDecorator AddInMemoryStores(this IIdentityServerBuilderDecorator builder)
        {
            builder.NotNull(nameof(builder));

            builder.Source.AddClientStore<InMemoryClientStore>();
            builder.Source.AddResourceStore<InMemoryResourcesStore>();

            builder.Services.AddSingleton<IEnumerable<IdentityResource>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                return options.Authorizations.IdentityResources;
            });

            builder.Services.AddSingleton<IEnumerable<ApiResource>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                return options.Authorizations.ApiResources;
            });

            builder.Services.AddSingleton<IEnumerable<Client>>(sp =>
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
                builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
            }

            return builder;
        }

    }
}
