// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Framework.DependencyInjection;

namespace Librame
{
    /// <summary>
    /// <see cref="IServiceMapper"/> 服务映射器接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface IServiceMapper
    {
        /// <summary>
        /// 获取服务集合。
        /// </summary>
        /// <seealso cref="IServiceCollection"/>
        IServiceCollection Services { get; }

        /// <summary>
        /// 映射服务与实现类型。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <typeparam name="TImplementation">指定的实现类型。</typeparam>
        /// <param name="lifecycle">给定的生存周期种类（可选；默认为单例）。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        IServiceMapper Map<TService, TImplementation>(LifecycleKind lifecycle = LifecycleKind.Singleton);
        /// <summary>
        /// 映射服务类型。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <param name="implementationInstance">给定的服务实现实例（可选）。</param>
        /// <returns>返回 <see cref="IServiceMapper"/>。</returns>
        IServiceMapper Map<TService>(TService implementationInstance = default(TService)) where TService : class;
    }
}