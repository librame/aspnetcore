#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Data.Accessors;
using Librame.Extensions.Data.Builders;
using Librame.Extensions.Data.Services;
using Librame.Extensions.Data.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 访问器数据构建器静态扩展。
    /// </summary>
    public static class AspNetCoreAccessorDataBuilderExtensions
    {
        /// <summary>
        /// 添加服务类型为 <see cref="IAccessor"/> 的数据库上下文访问器。
        /// </summary>
        /// <typeparam name="TAccessor">指定派生自 <see cref="DataDbContextAccessor{TGenId, TIncremId, TCreatedBy}"/> 的访问器类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IDataBuilder"/>。</param>
        /// <param name="setupAction">给定的 <see cref="Action{DataBuilderOptions, DbContextOptionsBuilder}"/>。</param>
        /// <param name="poolSize">设置池保留的最大实例数（可选；默认为128，如果小于1，将使用 AddDbContext() 注册）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddAccessorCore<TAccessor>(this IDataBuilder builder,
            Action<ITenant, DbContextOptionsBuilder> setupAction, int poolSize = 128)
            where TAccessor : DbContext, IAccessor
            => builder.AddAccessorCore<IAccessor, TAccessor>(setupAction, poolSize);

        /// <summary>
        /// 添加数据库上下文访问器。
        /// </summary>
        /// <typeparam name="TAccessor">指定派生自 <see cref="IDataAccessor{TAudit, TAuditProperty, TEntity, TMigration, TTenant}"/> 的访问器类型。</typeparam>
        /// <typeparam name="TImplementation">指定派生自 <see cref="DataDbContextAccessor{TGenId, TIncremId, TCreatedBy}"/> 的访问器实现类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IDataBuilder"/>。</param>
        /// <param name="setupAction">给定的 <see cref="Action{ITenant, DbContextOptionsBuilder}"/>。</param>
        /// <param name="poolSize">设置池保留的最大实例数（可选；默认为128，如果小于1，将使用 AddDbContext() 注册）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IDataBuilder AddAccessorCore<TAccessor, TImplementation>(this IDataBuilder builder,
            Action<ITenant, DbContextOptionsBuilder> setupAction, int poolSize = 128)
            where TAccessor : class, IAccessor
            where TImplementation : DbContext, TAccessor
            => builder
                .AddAccessor<TAccessor, TImplementation>(setupAction, poolSize)
                .AddInternalAspNetCoreAccessorServices();


        private static IDataBuilder AddInternalAspNetCoreAccessorServices(this IDataBuilder builder)
        {
            builder.Services.RemoveAll<IMultiTenancyAccessorService>();
            builder.AddMultiTenantAccessorService(typeof(AspNetCoreMultiTenancyAccessorService<,,,,,,,>));

            return builder;
        }

    }
}
