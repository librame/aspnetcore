#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibrameCore
{
    using Utility;

    /// <summary>
    /// Librame 构建器静态扩展。
    /// </summary>
    public static class LibrameBuilderExtensions
    {

        /// <summary>
        /// 是否包含指定服务。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回布尔值。</returns>
        public static bool ContainsService<TService>(this ILibrameBuilder builder)
        {
            return builder.ContainsService(typeof(TService));
        }
        /// <summary>
        /// 是否包含指定服务。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回布尔值。</returns>
        public static bool ContainsService(this ILibrameBuilder builder, Type serviceType)
        {
            return (builder.GetService(serviceType) != null);
        }


        /// <summary>
        /// 获取指定类型的服务。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回服务实例。</returns>
        public static TService GetService<TService>(this ILibrameBuilder builder)
        {
            return (TService)builder.ServiceProvider.GetService(typeof(TService));
        }
        /// <summary>
        /// 获取指定类型的服务。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回服务对象。</returns>
        public static object GetService(this ILibrameBuilder builder, Type serviceType)
        {
            return builder.ServiceProvider.GetService(serviceType);
        }


        /// <summary>
        /// 尝试添加域例服务。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <typeparam name="TService">给定的服务类型。</typeparam>
        /// <typeparam name="TImplementation">给定的实现类型。</typeparam>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddScopedService<TService, TImplementation>(this ILibrameBuilder builder)
            where TService : class
            where TImplementation : class, TService
        {
            var serviceType = typeof(TService);

            if (!builder.ContainsService(serviceType))
                builder.Services.AddScoped(serviceType, typeof(TImplementation));

            return builder;
        }
        /// <summary>
        /// 尝试添加域例服务。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <param name="implementationType">给定的实现类型。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddScopedService(this ILibrameBuilder builder, Type serviceType, Type implementationType)
        {
            if (!builder.ContainsService(serviceType))
            {
                serviceType.CanAssignableFromType(implementationType);
                builder.Services.AddScoped(serviceType, implementationType);
            }

            return builder;
        }


        /// <summary>
        /// 尝试添加单例服务。
        /// </summary>
        /// <typeparam name="TService">给定的服务类型。</typeparam>
        /// <typeparam name="TImplementation">给定的实现类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddSingletonService<TService, TImplementation>(this ILibrameBuilder builder)
            where TService : class
            where TImplementation : class, TService
        {
            var serviceType = typeof(TService);

            if (!builder.ContainsService(serviceType))
                builder.Services.AddSingleton(serviceType, typeof(TImplementation));

            return builder;
        }
        /// <summary>
        /// 尝试添加单例服务。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <param name="implementationType">给定的实现类型。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddSingletonService(this ILibrameBuilder builder, Type serviceType, Type implementationType)
        {
            if (!builder.ContainsService(serviceType))
            {
                serviceType.CanAssignableFromType(implementationType);
                builder.Services.AddSingleton(serviceType, implementationType);
            }

            return builder;
        }


        /// <summary>
        /// 尝试添加瞬例服务。
        /// </summary>
        /// <typeparam name="TService">给定的服务类型。</typeparam>
        /// <typeparam name="TImplementation">给定的实现类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddTransientService<TService, TImplementation>(this ILibrameBuilder builder)
            where TService : class
            where TImplementation : class, TService
        {
            var serviceType = typeof(TService);

            if (!builder.ContainsService(serviceType))
                builder.Services.AddTransient(serviceType, typeof(TImplementation));

            return builder;
        }
        /// <summary>
        /// 尝试添加瞬例服务。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <param name="implementationType">给定的实现类型。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddTransientService(this ILibrameBuilder builder, Type serviceType, Type implementationType)
        {
            if (!builder.ContainsService(serviceType))
            {
                serviceType.CanAssignableFromType(implementationType);
                builder.Services.AddTransient(serviceType, implementationType);
            }

            return builder;
        }

    }
}
