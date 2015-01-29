// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame
{
    /// <summary>
    /// <see cref="ApplicationBinder"/> 应用绑定器。
    /// </summary>
    /// <author>Librame Pang</author>
    public class ApplicationBinder : IApplicationBinder
    {
        /// <summary>
        /// 算法域。
        /// </summary>
        public const string AlgorithmDomain = "Algorithm";
        /// <summary>
        /// 查询拦截器域。
        /// </summary>
        public const string QueryInterceptorDomain = "QueryInterceptor";

        static ConcurrentDictionary<string, IApplicationContainer> _containers =
            new ConcurrentDictionary<string, IApplicationContainer>();

        /// <summary>
        /// 构造一个应用绑定器实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// builder 为空。
        /// </exception>
        /// <param name="builder">给定的应用构建器。</param>
        public ApplicationBinder(IApplicationBuilder builder)
        {
            if (ReferenceEquals(builder, null))
            {
                throw new ArgumentNullException("builder");
            }

            Builder = builder;
        }

        /// <summary>
        /// 获取应用构建器。
        /// </summary>
        /// <seealso cref="IApplicationBuilder"/>
        public IApplicationBuilder Builder { get; private set; }

        /// <summary>
        /// 获取指定域的容器接口。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <returns>返回容器接口或空。</returns>
        public IApplicationContainer this[string domain]
        {
            get { return _containers[domain]; }
        }


        /// <summary>
        /// 创建应用容器。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <returns>返回容器实例。</returns>
        protected virtual IApplicationContainer CreateContainer(string domain)
        {
            return new ApplicationContainer(domain);
        }


        /// <summary>
        /// 增加或更新指定域、应用类型的实例。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <param name="instance">给定的新实例。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public IApplicationBinder AddOrUpdateInstance(string domain, Type applicationType, object instance)
        {
            _containers.AddOrUpdate(domain, (key) =>
            {
                // Add
                return CreateContainer(domain).AddOrUpdateInstance(applicationType, instance);
            }, (key, value) =>
            {
                // Update
                return value.AddOrUpdateInstance(applicationType, instance);
            });

            return this;
        }


        /// <summary>
        /// 绑定应用。
        /// </summary>
        /// <param name="domain">给定的应用域。</param>
        /// <param name="option">给定的应用选项。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public IApplicationBinder Bind(string domain, ApplicationOption option)
        {
            _containers.AddOrUpdate(domain, (key) =>
            {
                // Add
                return CreateContainer(domain).Register(option);
            }, (key, value) =>
            {
                // Update
                return value.Register(option);
            });

            return this;
        }

        /// <summary>
        /// 绑定应用。
        /// </summary>
        /// <param name="domain">给定的应用域。</param>
        /// <param name="options">给定的应用选项集合。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public IApplicationBinder Bind(string domain, IEnumerable<ApplicationOption> options)
        {
            _containers.AddOrUpdate(domain, (key) =>
            {
                // Add
                return CreateContainer(domain).Register(options);
            }, (key, value) =>
            {
                // Update
                return value.Register(options);
            });

            return this;
        }

        /// <summary>
        /// 绑定应用。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// binderFactory 为空。
        /// </exception>
        /// <param name="domain">给定的应用域。</param>
        /// <param name="binderFactory">给定的应用绑定方法。</param>
        /// <returns>返回 <see cref="IApplicationBinder"/>。</returns>
        public IApplicationBinder Bind(string domain, Func<IApplicationContainer, IApplicationContainer> binderFactory)
        {
            if (ReferenceEquals(binderFactory, null))
            {
                throw new ArgumentNullException("binderFactory");
            }

            _containers.AddOrUpdate(domain, (key) =>
            {
                // Add
                return binderFactory(CreateContainer(domain));
            }, (key, value) =>
            {
                // Update
                return binderFactory(value);
            });

            return this;
        }


        /// <summary>
        /// 获取指定域的应用选项字典集合。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <returns>返回字典集合或空。</returns>
        public ConcurrentDictionary<Type, ApplicationOption> GetOptions(string domain)
        {
            return this[domain]?.Options;
        }

        /// <summary>
        /// 获取指定域的应用选项。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回应用选项或空。</returns>
        public ApplicationOption GetOption(string domain, Type applicationType)
        {
            return this[domain]?[applicationType];
        }


        /// <summary>
        /// 解析指定域、应用类型的实例。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="domain">给定的域。</param>
        /// <returns>返回实例或空。</returns>
        public T Resolve<T>(string domain)
        {
            return (T)Resolve(domain, typeof(T));
        }
        /// <summary>
        /// 解析指定域、应用类型的实例。
        /// </summary>
        /// <param name="domain">给定的域。</param>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <returns>返回实例或空。</returns>
        public object Resolve(string domain, Type applicationType)
        {
            return this[domain]?.Resolve(applicationType);
        }

    }
}