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
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace Librame
{
    using Utility;

    /// <summary>
    /// Librame 构建器接口。
    /// </summary>
    public interface ILibrameBuilder
    {
        /// <summary>
        /// 服务集合。
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序。
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 选项。
        /// </summary>
        LibrameOptions Options { get; }
    }


    /// <summary>
    /// Librame 构建器。
    /// </summary>
    public class LibrameBuilder : ILibrameBuilder
    {
        /// <summary>
        /// 构造一个 Librame 构建器实例。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        public LibrameBuilder(IServiceCollection services)
        {
            Services = services.NotNull(nameof(services));

            // 添加自身
            Services.AddSingleton<ILibrameBuilder>(this);
        }


        /// <summary>
        /// 服务集合。
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序。
        /// </summary>
        public IServiceProvider ServiceProvider => Services.BuildServiceProvider();

        /// <summary>
        /// 选项。
        /// </summary>
        public LibrameOptions Options => ServiceProvider.GetService<IOptions<LibrameOptions>>().Value;
    }


    /// <summary>
    /// Librame 构建器静态扩展。
    /// </summary>
    public static class LibrameBuilderExtensions
    {
        /// <summary>
        /// 使用适配功能（注册指定程序集数组包含的所有适配器）。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="assemblies">给定的程序集数组。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder UseAdaptation(this ILibrameBuilder builder, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length < 1)
                return builder;
            
            assemblies.Invoke(a =>
            {
                var types = TypeUtil.EnumerableAssignableTypes<Adaptation.IAdapter>(a);

                builder.UseAdaptation(types);
            });

            return builder;
        }
        /// <summary>
        /// 使用适配功能（注册指定的自定义适配器类型数组）。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="types">给定的适配器类型数组。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder UseAdaptation(this ILibrameBuilder builder, params Type[] types)
        {
            builder.NotNull(nameof(builder));

            if (types == null || types.Length < 1)
                return builder;

            var serviceType = typeof(Adaptation.IAdapter);

            types.Invoke(t =>
            {
                builder.Services.AddSingleton(serviceType, t);
            });

            return builder;
        }


        /// <summary>
        /// 使用实体功能。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="repositoryType">
        /// 给定实现 <see cref="Entity.IRepository{TEntity}"/> 接口的仓库类型
        /// （可选；默认使用 <see cref="Entity.EntityOptions.DefaultRepositoryTypeName"/> 选项绑定的类型名）。
        /// </param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder UseEntity(this ILibrameBuilder builder, Type repositoryType = null)
        {
            builder.NotNull(nameof(builder));
            
            if (repositoryType == null)
                repositoryType = Type.GetType(builder.Options.Entity.RepositoryTypeName, throwOnError: true);

            var baseType = typeof(Entity.IRepository<>);

            // 检测指定仓库类型是否继承于基础仓库类型
            repositoryType = baseType.CanAssignableFromType(repositoryType);

            builder.Services.AddTransient(baseType, repositoryType);

            return builder;
        }

    }
}
