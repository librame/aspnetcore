// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame;
using System;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Librame 应用构建器静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameApplicationBuilderExtensions
    {
        /// <summary>
        /// 获取指定类型的服务。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <param name="app">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <returns>返回服务对象。</returns>
        /// <seealso cref="IServiceProvider.GetService(Type)"/>
        public static TService GetService<TService>(this IApplicationBuilder app)
        {
            return app.ApplicationServices.GetService<TService>();
        }

        /// <summary>
        /// 使用 Librame。
        /// </summary>
        /// <param name="app">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <param name="binderFactory">给定的绑定器工厂方法。</param>
        /// <returns>返回 <see cref="IApplicationBuilder"/>。</returns>
        public static IApplicationBuilder UseLibrame(this IApplicationBuilder app, Action<IApplicationBinder> binderFactory = null)
        {
            var binder = LibrameHelper.GetOrCreateBinder(app);

            // 查询拦截器
            binder.BindQueryInterceptor();
            // 算法
            //binder.BindAlgorithm();
            binder.BindSha256();
            binder.BindHmacSha256();
            binder.BindAes();

            if (!ReferenceEquals(binderFactory, null))
            {
                // 自定义绑定
                binderFactory(binder);
            }

            return app;
        }

    }
}