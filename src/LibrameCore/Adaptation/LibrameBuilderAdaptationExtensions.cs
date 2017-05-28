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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LibrameCore
{
    using Adaptation;
    using Utility;

    /// <summary>
    /// Librame 构建器适配静态扩展。
    /// </summary>
    public static class LibrameBuilderAdaptationExtensions
    {

        #region TryAddAdapter

        /// <summary>
        /// 尝试添加适配模块（同时添加指定程序集数组包含的所有适配器模块）。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="assemblies">给定的程序集数组。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder TryAddAdaptation(this ILibrameBuilder builder, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length < 1)
                return builder;

            assemblies.Invoke(a =>
            {
                // 记录本地化调试日志
                builder.Logger.LogInformation("Try adding adapter modules by {0} assembly", a.FullName);

                // 获取所有适配器实现类型集合
                var implementationTypes = TypeUtil.EnumerableTypesByAssignableFrom<IAdapter>(a);
                if (implementationTypes.Length > 0)
                {
                    var baseAdapter = typeof(IAdapter);

                    implementationTypes.Invoke(t =>
                    {
                        // 搜索此适配器实现类型的接口类型集合，并过滤与适配器无关的接口，包括基础适配器接口自身
                        var interfaceTypes = t.GetInterfaces()
                            .Where(i => i.Name != baseAdapter.Name && baseAdapter.IsAssignableFrom(i));

                        // 默认取第一个接口类型为服务类型
                        var interfaceType = interfaceTypes.FirstOrDefault().AsOrDefault(baseAdapter);
                        builder.TryAddAdapter(interfaceType, t);
                    });
                }
            });

            return builder;
        }

        /// <summary>
        /// 尝试添加适配模块。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="interfaceImplementationTypes">给定的适配器接口键、实现类型值的字典集合。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder TryAddAdaptation(this ILibrameBuilder builder, IDictionary<Type, Type> interfaceImplementationTypes)
        {
            builder.NotNull(nameof(builder));

            if (interfaceImplementationTypes == null || interfaceImplementationTypes.Count < 1)
                return builder;
            
            interfaceImplementationTypes.Invoke(pair =>
            {
                builder.TryAddAdapter(pair.Key, pair.Value);
            });

            return builder;
        }


        /// <summary>
        /// 尝试添加适配器。
        /// </summary>
        /// <typeparam name="TInterface">指定的适配器接口类型。</typeparam>
        /// <typeparam name="TImplementation">指定的适配器实现类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddAdapter<TInterface, TImplementation>(this ILibrameBuilder builder)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return builder.TryAddSingleton<TInterface, TImplementation>();
        }

        /// <summary>
        /// 尝试添加适配器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="interfaceType">给定的适配器接口类型。</param>
        /// <param name="implementationType">给定的适配器实现类型。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder TryAddAdapter(this ILibrameBuilder builder, Type interfaceType, Type implementationType)
        {
            // 限定两者必须实现适配器接口
            typeof(IAdapter).CanAssignableFromType(implementationType);

            // 记录本地化调试日志
            builder.Logger.LogInformation("Try adding {0}, {1} adapter module", interfaceType.Name, implementationType.Name);

            return builder.TryAddSingleton(interfaceType, implementationType);
        }

        #endregion


        #region GetAdapter

        /// <summary>
        /// 获取所有适配器集合。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回集合。</returns>
        public static IEnumerable<IAdapter> GetAllAdapters(this ILibrameBuilder builder)
        {
            return builder.ServiceProvider.GetServices<IAdapter>();
        }


        /// <summary>
        /// 获取指定类型的适配器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <typeparam name="TInterface">指定的适配器接口类型。</typeparam>
        /// <returns>返回适配器。</returns>
        public static TInterface GetAdapter<TInterface>(this ILibrameBuilder builder)
            where TInterface : IAdapter
        {
            return (TInterface)builder.GetAdapter(typeof(TInterface));
        }
        /// <summary>
        /// 获取指定类型的适配器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="interfaceType">给定的适配器接口类型。</param>
        /// <returns>返回适配器。</returns>
        public static IAdapter GetAdapter(this ILibrameBuilder builder, Type interfaceType)
        {
            return (IAdapter)builder.GetService(interfaceType);
        }


        /// <summary>
        /// 获取算法适配器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回算法适配器。</returns>
        public static Algorithm.IAlgorithmAdapter GetAlgorithmAdapter(this ILibrameBuilder builder)
        {
            return builder.GetAdapter<Algorithm.IAlgorithmAdapter>();
        }

        /// <summary>
        /// 获取实体适配器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="connectionString">给定的数据库连接字符串（可选）。</param>
        /// <returns>返回实体适配器。</returns>
        public static Entity.IEntityAdapter GetEntityAdapter(this ILibrameBuilder builder, string connectionString = null)
        {
            var adapter = builder.GetAdapter<Entity.IEntityAdapter>();

            // 增强基于实体框架的 SQL Server 数据库
            adapter.BuildUpEntityFrameworkSqlServer(connectionString);

            return adapter;
        }

        #endregion

    }
}
