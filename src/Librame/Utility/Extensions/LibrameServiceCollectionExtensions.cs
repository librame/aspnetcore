// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame;
using Microsoft.AspNet.Builder;
using System;

namespace Microsoft.Framework.DependencyInjection
{
    /// <summary>
    /// Librame 服务集合静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameServiceCollectionExtensions
    {
        /// <summary>
        /// 增加 Librame 支持。
        /// </summary>
        /// <param name="services">给定的服务集合接口。</param>
        /// <param name="mapperFactory">给定的映射器工厂方法。</param>
        /// <returns>返回服务集合对象。</returns>
        public static IServiceCollection AddLibrame(this IServiceCollection services, Action<IServiceMapper> mapperFactory = null)
        {
            var mapper = LibrameHelper.GetOrCreateMapper(services);

            // 预映射
            mapper.MapId();
            mapper.MapEncoding();
            mapper.MapAccessorFactory();

            if (!ReferenceEquals(mapperFactory, null))
            {
                // 自定义映射
                mapperFactory(mapper);
            }

            return services;
        }

    }
}