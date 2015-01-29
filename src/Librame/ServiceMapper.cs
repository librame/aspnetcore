// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.DependencyInjection;
using System;

namespace Librame
{
    /// <summary>
    /// <see cref="ServiceMapper"/> 服务映射器。
    /// </summary>
    /// <author>Librame Pang</author>
    public class ServiceMapper : IServiceMapper
    {
        /// <summary>
        /// 构造一个服务映射器实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// services 为空。
        /// </exception>
        /// <param name="services">给定的服务集合。</param>
        public ServiceMapper(IServiceCollection services)
        {
            if (ReferenceEquals(services, null))
            {
                throw new ArgumentNullException("services");
            }

            Services = services;
        }

        /// <summary>
        /// 获取服务集合。
        /// </summary>
        /// <seealso cref="IServiceCollection"/>
        public IServiceCollection Services { get; private set; }

        /// <summary>
        /// 映射服务与实现类型。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <typeparam name="TImplementation">指定的实现类型。</typeparam>
        /// <param name="lifecycle">给定的生存周期种类（可选；默认为单例）。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        public IServiceMapper Map<TService, TImplementation>(LifecycleKind lifecycle = LifecycleKind.Singleton)
        {
            var descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifecycle);

            Services.Add(descriptor);
            
            return this;
        }
        /// <summary>
        /// 映射服务类型。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <param name="implementationInstance">给定的服务实现实例（可选）。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        public IServiceMapper Map<TService>(TService implementationInstance = default(TService))
            where TService : class
        {
            Services.AddInstance(implementationInstance);

            return this;
        }

    }
}