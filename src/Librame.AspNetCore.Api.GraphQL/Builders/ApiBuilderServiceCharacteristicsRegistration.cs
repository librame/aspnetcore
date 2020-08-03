#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL;
using GraphQL.Http;
using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.Api.Builders
{
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// <see cref="IApiBuilder"/> 服务特征注册。
    /// </summary>
    public static class ApiBuilderServiceCharacteristicsRegistration
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
                { typeof(IDocumentExecuter), ServiceCharacteristics.Singleton() },
                { typeof(IDocumentWriter), ServiceCharacteristics.Singleton() },

                { typeof(IGraphApiMutation), ServiceCharacteristics.Scoped() },
                { typeof(IGraphApiQuery), ServiceCharacteristics.Scoped() },
                { typeof(IGraphApiSubscription), ServiceCharacteristics.Scoped() },
                { typeof(IGraphApiSchema), ServiceCharacteristics.Scoped() },

                { typeof(IApiRequest), ServiceCharacteristics.Transient() }
            };
        }

    }
}
