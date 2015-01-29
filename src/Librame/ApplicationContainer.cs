// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Librame
{
    /// <summary>
    /// 应用容器。
    /// </summary>
    /// <author>Librame Pang</author>
    public class ApplicationContainer : IApplicationContainer
    {
        /// <summary>
        /// 构造一个应用容器实例。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <param name="options">给定的应用选项。</param>
        public ApplicationContainer(string domain, ConcurrentDictionary<Type, ApplicationOption> options = null)
        {
            Domain = domain;
            Options = options ?? new ConcurrentDictionary<Type, ApplicationOption>();
        }

        /// <summary>
        /// 获取应用域。
        /// </summary>
        public string Domain { get; private set; }

        /// <summary>
        /// 获取应用选项集合。
        /// </summary>
        public ConcurrentDictionary<Type, ApplicationOption> Options { get; private set; }

        /// <summary>
        /// 获取指定应用类型的选项。
        /// </summary>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回应用选项。</returns>
        public ApplicationOption this[Type applicationType]
        {
            get { return Options.SingleOrDefault(pair => pair.Key == applicationType).Value; }
        }


        /// <summary>
        /// 增加或更新指定应用类型的选项中的实例。
        /// </summary>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <param name="instance">给定的应用选项中的实例。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        public IApplicationContainer AddOrUpdateInstance(Type applicationType, object instance)
        {
            Options.AddOrUpdate(applicationType,
                // Add
                key => new ApplicationOption(key, instance),
                // Update
                (key, value) =>
                {
                    value.Instance = instance;
                    return value;
                });

            return this;
        }


        /// <summary>
        /// 注册指定类型与实现的应用。
        /// </summary>
        /// <typeparam name="TApplication">给定的应用类型。</typeparam>
        /// <typeparam name="TImplementation">给定的应用实现类型。</typeparam>
        /// <param name="parameters">给定的构造参数集合。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        public IApplicationContainer Register<TApplication, TImplementation>(params object[] parameters)
        {
            return Register(new ApplicationOption(typeof(TApplication), typeof(TImplementation), parameters));
        }
        /// <summary>
        /// 注册指定类型实例。
        /// </summary>
        /// <typeparam name="TApplication">给定的应用类型。</typeparam>
        /// <param name="instance">给定的实例。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        public IApplicationContainer Register<TApplication>(TApplication instance)
        {
            return Register(new ApplicationOption(typeof(TApplication), instance));
        }
        /// <summary>
        /// 注册应用选项。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// option 为空。
        /// </exception>
        /// <param name="option">给定的应用选项。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        public IApplicationContainer Register(ApplicationOption option)
        {
            if (ReferenceEquals(option, null))
            {
                throw new ArgumentNullException("option");
            }

            Options.AddOrUpdate(option.ApplicationType, option, (key, value) => option);

            return this;
        }
        /// <summary>
        /// 注册应用选项集合。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// options 为空。
        /// </exception>
        /// <param name="options">给定的应用选项集合。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        public IApplicationContainer Register(IEnumerable<ApplicationOption> options)
        {
            if (ReferenceEquals(options, null))
            {
                throw new ArgumentNullException("options");
            }

            foreach (var option in options)
            {
                if (!ReferenceEquals(option, null))
                {
                    Options.AddOrUpdate(option.ApplicationType, option, (key, value) => option);
                }
            }

            return this;
        }


        /// <summary>
        /// 解析指定应用类型的选项中的实例。
        /// </summary>
        /// <typeparam name="T">指定的应用类型。</typeparam>
        /// <returns>返回实例。</returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        /// <summary>
        /// 解析指定应用类型的选项中的实例。
        /// </summary>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回实例。</returns>
        public object Resolve(Type applicationType)
        {
            return ResolveOption(Options[applicationType]);
        }
        private object ResolveOption(ApplicationOption option)
        {
            if (ReferenceEquals(option, null))
            {
                return null;
            }

            // 解析实例
            if (ReferenceEquals(option.Instance, null))
            {
                var type = option.ApplicationType;

                // 如果实现类型不为空
                if (!ReferenceEquals(option.ImplementationType, null))
                    type = option.ImplementationType;

                // 解析实例
                if (!ReferenceEquals(option.Parameters, null) && option.Parameters.Length > 0)
                {
                    return Activator.CreateInstance(type, option.Parameters);
                }
                else
                {
                    return Activator.CreateInstance(type);
                }
            }
            else
            {
                return option.Instance;
            }
        }

    }
}