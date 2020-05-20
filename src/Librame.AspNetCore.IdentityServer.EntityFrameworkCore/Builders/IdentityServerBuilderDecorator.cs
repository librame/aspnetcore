#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
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
    using Extensions.Encryption.Services;

    internal class IdentityServerBuilderDecorator : AbstractExtensionBuilderDecorator<IIdentityServerBuilder>, IIdentityServerBuilderDecorator
    {
        public IdentityServerBuilderDecorator(Type userType, IIdentityServerBuilder sourceBuilder, IExtensionBuilder parentBuilder,
            IdentityServerBuilderDependency dependency)
            : base(sourceBuilder, parentBuilder, dependency)
        {
            UserType = userType.NotNull(nameof(userType));

            Services.AddSingleton<IIdentityServerBuilderDecorator>(this);

            AddIdentityServerServices();
        }


        public Type UserType { get; }


        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => IdentityServerBuilderServiceCharacteristicsRegistration.Register.GetOrDefault(serviceType);


        private void AddIdentityServerServices()
        {
            AddService<ISigningCredentialStore>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<IdentityServerBuilderOptions>>().Value;
                if (options.Authorizations.SigningCredentials.IsNull())
                {
                    var service = provider.GetRequiredService<ISigningCredentialsService>();
                    options.Authorizations.SigningCredentials = service.GetGlobalSigningCredentials();
                }
                return new DefaultSigningCredentialsStore(options.Authorizations.SigningCredentials);
            });
            AddService<IValidationKeysStore>(provider =>
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
        }

    }
}
