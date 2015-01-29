// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame
{
    /// <summary>
    /// <see cref="IApplicationBinder"/> 应用绑定器接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface IApplicationBinder
    {
        /// <summary>
        /// 获取应用构建器。
        /// </summary>
        /// <seealso cref="IApplicationBuilder"/>
        IApplicationBuilder Builder { get; }

        /// <summary>
        /// 获取指定域的容器接口。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <returns>返回容器接口或空。</returns>
        IApplicationContainer this[string domain] { get; }


        /// <summary>
        /// 增加或更新指定域、应用类型的实例。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <param name="instance">给定的新实例。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        IApplicationBinder AddOrUpdateInstance(string domain, Type applicationType, object instance);

        /// <summary>
        /// 绑定应用。
        /// </summary>
        /// <param name="domain">给定的应用域。</param>
        /// <param name="option">给定的应用选项。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        IApplicationBinder Bind(string domain, ApplicationOption option);

        /// <summary>
        /// 绑定应用。
        /// </summary>
        /// <param name="domain">给定的应用域。</param>
        /// <param name="options">给定的应用选项集合。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        IApplicationBinder Bind(string domain, IEnumerable<ApplicationOption> options);

        /// <summary>
        /// 绑定应用。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// binderFactory 为空。
        /// </exception>
        /// <param name="domain">给定的应用域。</param>
        /// <param name="binderFactory">给定的应用绑定方法。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        IApplicationBinder Bind(string domain, Func<IApplicationContainer, IApplicationContainer> binderFactory);


        /// <summary>
        /// 获取指定域的应用选项字典集合。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <returns>返回字典集合或空。</returns>
        ConcurrentDictionary<Type, ApplicationOption> GetOptions(string domain);

        /// <summary>
        /// 获取指定域的应用选项。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回应用选项或空。</returns>
        ApplicationOption GetOption(string domain, Type applicationType);

        /// <summary>
        /// 解析指定域、应用类型的实例。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="domain">给定的域。</param>
        /// <returns>返回实例或空。</returns>
        T Resolve<T>(string domain);
        /// <summary>
        /// 解析指定域、应用类型的实例。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回实例或空。</returns>
        object Resolve(string domain, Type applicationType);

    }
}