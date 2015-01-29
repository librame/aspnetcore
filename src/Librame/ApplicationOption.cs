// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Librame
{
    /// <summary>
    /// 应用选项。
    /// </summary>
    /// <author>Librame Pang</author>
    public class ApplicationOption
    {
        /// <summary>
        /// 构造一个应用选项实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// applicationType 为空。
        /// </exception>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <param name="implementationType">给定的实现类型。</param>
        /// <param name="parameters">给定的构造参数集合。</param>
        public ApplicationOption(Type applicationType, Type implementationType, params object[] parameters)
            : this(applicationType)
        {
            ImplementationType = implementationType;
            Parameters = parameters;
        }
        /// <summary>
        /// 构造一个应用选项实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// applicationType 为空。
        /// </exception>
        /// <param name="applicationType">给定的应用类型。</param>
        /// <param name="instance">给定的实例。</param>
        internal ApplicationOption(Type applicationType, object instance)
            : this(applicationType)
        {
            Instance = instance;
        }
        private ApplicationOption(Type applicationType)
        {
            if (ReferenceEquals(applicationType, null))
            {
                throw new ArgumentNullException("applicationType");
            }

            ApplicationType = applicationType;
        }

        /// <summary>
        /// 获取应用类型。
        /// </summary>
        public Type ApplicationType { get; private set; }
        /// <summary>
        /// 获取实现类型。
        /// </summary>
        public Type ImplementationType { get; private set; }
        /// <summary>
        /// 获取构造参数集合。
        /// </summary>
        public object[] Parameters { get; private set; }
        /// <summary>
        /// 获取或设置应用实例。
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// 更新实例。
        /// </summary>
        /// <param name="instance">给定的实例。</param>
        /// <returns>返回应用选项。</returns>
        internal virtual ApplicationOption UpdateInstance(object instance)
        {
            Instance = instance;

            return this;
        }

    }
}