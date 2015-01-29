// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data;
using Microsoft.Data.Entity;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace System
{
    /// <summary>
    /// Librame 服务管道静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameServiceProviderExtensions
    {
        /// <summary>
        /// 创建实体仓库。
        /// </summary>
        /// <typeparam name="TDbContext">指定的数据上下文类型。</typeparam>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="provider">给定的服务管道接口。</param>
        /// <param name="createAccessorFactory">给定的创建访问器方法。</param>
        /// <returns>返回实体仓库对象。</returns>
        public static IRepository<TEntity> CreateRepository<TDbContext, TEntity>(this IServiceProvider provider, Func<TDbContext, IAccessor> createAccessorFactory = null)
            where TDbContext : DbContext
            where TEntity : class
        {
            var accessor = provider.CreateAccessor<TDbContext>(createAccessorFactory);

            return new Repository<TEntity>(accessor);
        }
        /// <summary>
        /// 创建数据访问器。
        /// </summary>
        /// <typeparam name="TDbContext">指定的数据上下文类型。</typeparam>
        /// <param name="provider">给定的服务管道接口。</param>
        /// <param name="createAccessorFactory">给定的创建访问器方法。</param>
        /// <returns>返回数据访问器对象。</returns>
        public static IAccessor CreateAccessor<TDbContext>(this IServiceProvider provider, Func<TDbContext, IAccessor> createAccessorFactory = null) where TDbContext : DbContext
        {
            var context = provider.GetService<TDbContext>();

            IAccessor accessor = null;

            if (ReferenceEquals(createAccessorFactory, null))
            {
                var factory = provider.GetService<AccessorFactory>();

                if (ReferenceEquals(factory, null))
                    throw new ArgumentException("请在 Startup.cs 中配置 app.UseServices(services => { services.AddLibrame(); })");

                accessor = factory.Create(context);
            }
            else
            {
                accessor = createAccessorFactory(context);
            }

            return accessor;
        }

        /// <summary>
        /// 获取指定类型的服务。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <param name="provider">给定的服务管道接口。</param>
        /// <returns>返回服务对象。</returns>
        /// <seealso cref="IServiceProvider.GetService(Type)"/>
        public static TService GetService<TService>(this IServiceProvider provider)
        {
            try
            {
                return (TService)provider.GetService(typeof(TService));
            }
            catch (TargetInvocationException ex)
            {
                // TODO: See DependencyInjection Issue #127
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

    }
}