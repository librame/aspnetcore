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
using IdentityServer4.Services;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// <see cref="IIdentityServerBuilderDecorator"/> 服务特征注册。
    /// </summary>
    public static class IdentityServerBuilderServiceCharacteristicsRegistration
    {
        private static IServiceCharacteristicsRegister _register;

        /// <summary>
        /// 当前注册器。
        /// </summary>
        public static IServiceCharacteristicsRegister Register
        {
            get => _register.EnsureSingleton(() => new ServiceCharacteristicsRegister(InitializeCharacteristics()));
            set => _register = value.NotNull(nameof(value));
        }


        private static IDictionary<Type, ServiceCharacteristics> InitializeCharacteristics()
        {
            return new Dictionary<Type, ServiceCharacteristics>
            {
                { typeof(ISigningCredentialStore), ServiceCharacteristics.Singleton() },
                { typeof(IValidationKeysStore), ServiceCharacteristics.Singleton() },
                { typeof(IEnumerable<IdentityResource>), ServiceCharacteristics.Singleton() },
                { typeof(IEnumerable<ApiResource>), ServiceCharacteristics.Singleton() },
                { typeof(IEnumerable<Client>), ServiceCharacteristics.Singleton() },
                { typeof(ICorsPolicyService), ServiceCharacteristics.Transient() }
            };
        }

    }
}
