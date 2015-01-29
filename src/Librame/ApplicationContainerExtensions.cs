// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Context;

namespace Librame
{
    /// <summary>
    /// 应用容器静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class ApplicationContainerExtensions
    {
        /// <summary>
        /// 注册查询拦截器的指定类型与实现的应用。
        /// </summary>
        /// <typeparam name="TInterceptor">指定的拦截器类型。</typeparam>
        /// <typeparam name="TImplementation">指定的实现类型。</typeparam>
        /// <param name="parameters">给定的构造参数集合。</param>
        /// <returns>返回 <see cref="IApplicationContainer"/>。</returns>
        public static IApplicationContainer RegisterQueryInterceptor<TInterceptor, TImplementation>(this IApplicationContainer container, params QueryInterceptorParameter[] parameters)
        {
            var option = new QueryInterceptorOption(typeof(TInterceptor), typeof(TImplementation), parameters);

            return container.Register((ApplicationOption)option);
        }

    }
}