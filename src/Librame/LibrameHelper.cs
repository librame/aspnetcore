// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Librame
{
    /// <summary>
    /// Librame 助手。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameHelper
    {
        static IApplicationBinder _binder = null;
        static IServiceMapper _mapper = null;

        /// <summary>
        /// 获取或创建应用绑定器。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// builder 为空（首次实例化时不能为空）。
        /// </exception>
        /// <param name="builder">给定的应用构建器（可为空，但首次实例化时不能为空）。</param>
        /// <returns>返回应用绑定器。</returns>
        public static IApplicationBinder GetOrCreateBinder(IApplicationBuilder builder = null)
        {
            if (ReferenceEquals(_binder, null))
            {
                // 首次实例化时，应用构建器不能为空
                if (ReferenceEquals(builder, null))
                {
                    throw new ArgumentNullException("builder");
                }

                _binder = new ApplicationBinder(builder);
            }

            return _binder;
        }

        /// <summary>
        /// 获取或创建服务映射器。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// services 为空（首次实例化时不能为空）。
        /// </exception>
        /// <param name="services">给定的服务集合（可为空，但首次实例化时不能为空）。</param>
        /// <returns>返回服务映射器。</returns>
        public static IServiceMapper GetOrCreateMapper(IServiceCollection services = null)
        {
            if (ReferenceEquals(_mapper, null))
            {
                // 首次实例化时，服务集合不能为空
                if (ReferenceEquals(services, null))
                {
                    throw new ArgumentNullException("services");
                }

                _mapper = new ServiceMapper(services);
            }

            return _mapper;
        }

    }
}