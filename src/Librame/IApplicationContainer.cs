// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame
{
    /// <summary>
    /// 应用容器接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface IApplicationContainer
    {
        /// <summary>
        /// 获取应用域。
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// 获取应用选项集合。
        /// </summary>
        ConcurrentDictionary<Type, ApplicationOption> Options { get; }

        /// <summary>
        /// 获取指定应用类型的选项。
        /// </summary>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回应用选项。</returns>
        ApplicationOption this[Type applicationType] { get; }


        /// <summary>
        /// 增加或更新指定应用类型的选项中的实例。
        /// </summary>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <param name="instance">给定的应用选项中的实例。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        IApplicationContainer AddOrUpdateInstance(Type applicationType, object instance);

        /// <summary>
        /// 注册指定类型与实现的应用。
        /// </summary>
        /// <typeparam name="TApplication">给定的应用类型。</typeparam>
        /// <typeparam name="TImplementation">给定的应用实现类型。</typeparam>
        /// <param name="parameters">给定的构造参数集合。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        IApplicationContainer Register<TApplication, TImplementation>(params object[] parameters);
        /// <summary>
        /// 注册指定类型实例。
        /// </summary>
        /// <typeparam name="TApplication">给定的应用类型。</typeparam>
        /// <param name="instance">给定的实例。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        IApplicationContainer Register<TApplication>(TApplication instance);
        /// <summary>
        /// 注册应用选项。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// option 为空。
        /// </exception>
        /// <param name="option">给定的应用选项。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        IApplicationContainer Register(ApplicationOption option);
        /// <summary>
        /// 注册应用选项集合。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// options 为空。
        /// </exception>
        /// <param name="options">给定的应用选项集合。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        IApplicationContainer Register(IEnumerable<ApplicationOption> options);

        /// <summary>
        /// 解析指定应用类型的选项中的实例。
        /// </summary>
        /// <typeparam name="T">指定的应用类型。</typeparam>
        /// <returns>返回实例。</returns>
        T Resolve<T>();
        /// <summary>
        /// 解析指定应用类型的选项中的实例。
        /// </summary>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回实例。</returns>
        object Resolve(Type applicationType);
    }
}