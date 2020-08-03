#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using Extensions;
    using Extensions.Core.Builders;
    using Extensions.Core.Services;
    using Extensions.Data.Builders;
    using Extensions.Encryption.Services;

    /// <summary>
    /// <see cref="IIdentityServerBuilder"/> 装饰器。
    /// </summary>
    public class IdentityServerBuilderDecorator : AbstractExtensionBuilderDecorator<IIdentityServerBuilder>,
        IIdentityServerBuilderDecorator
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerBuilderDecorator"/>。
        /// </summary>
        /// <param name="sourceBuilder">给定的 <see cref="IIdentityServerBuilder"/>。</param>
        /// <param name="parentBuilder">给定的父级 <see cref="IDataBuilder"/>。</param>
        /// <param name="dependency">给定的 <see cref="IdentityServerBuilderDependency"/>。</param>
        public IdentityServerBuilderDecorator(IIdentityServerBuilder sourceBuilder,
            IExtensionBuilder parentBuilder, IdentityServerBuilderDependency dependency)
            : base(sourceBuilder, parentBuilder, dependency)
        {
            Services.AddSingleton<IIdentityServerBuilderDecorator>(this);

            AddInternalServices();
        }


        private void AddInternalServices()
        {
            AddService<ISigningCredentialStore>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                if (options.SigningCredentials.IsNull())
                {
                    var service = provider.GetRequiredService<ISigningCredentialsService>();
                    options.SigningCredentials = service.GetGlobalSigningCredentials();
                }
                return new InMemorySigningCredentialsStore(options.SigningCredentials);
            });

            AddService<IValidationKeysStore>(provider =>
            {
                var store = provider.GetRequiredService<ISigningCredentialStore>();
                var credentials = store.GetSigningCredentialsAsync().ConfigureAwaitCompleted();

                var keyInfo = new SecurityKeyInfo
                {
                    Key = credentials.Key,
                    SigningAlgorithm = SecurityAlgorithms.RsaSha256
                };
                return new InMemoryValidationKeysStore(keyInfo.YieldEnumerable());
            });
        }


        /// <summary>
        /// 用户类型。
        /// </summary>
        public Type UserType { get; private set; }


        /// <summary>
        /// 获取指定服务类型的特征。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回 <see cref="ServiceCharacteristics"/>。</returns>
        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => IdentityServerBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);

    }
}
