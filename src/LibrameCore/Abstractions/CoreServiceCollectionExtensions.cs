#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Abstractions;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IServiceCollection"/> 核心静态扩展。
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="fileName">给定的 JSON 文件名（可选；默认为 <see cref="ExtensionHelper.JSON_CONFIGURATION_FILENAME"/>）。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <param name="configureRoot">给定的根配置（可选）。</param>
        /// <param name="configureOptions">给定的配置依赖选项（可选）。</param>
        /// <param name="configureDependency">给定的配置依赖（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCoreByJsonFile(this IServiceCollection services,
            string fileName = ExtensionHelper.JSON_CONFIGURATION_FILENAME,
            string basePath = null,
            Action<IConfigurationRoot> configureRoot = null,
            Action<CoreLibraryOptions> configureOptions = null,
            Action<ILibraryDependency, CoreLibraryOptions> configureDependency = null)
        {
            return services.AddLibrameByJsonFile(fileName, basePath, configureRoot,
                configureOptions,
                configureDependency);
        }

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="source">给定的文件配置源。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <param name="configureRoot">给定的根配置（可选）。</param>
        /// <param name="configureOptions">给定的配置依赖选项（可选）。</param>
        /// <param name="configureDependency">给定的配置依赖（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCoreByFile(this IServiceCollection services,
            FileConfigurationSource source,
            string basePath = null,
            Action<IConfigurationRoot> configureRoot = null,
            Action<CoreLibraryOptions> configureOptions = null,
            Action<ILibraryDependency, CoreLibraryOptions> configureDependency = null)
        {
            return services.AddLibrameByFile(source, basePath, configureRoot,
                configureOptions,
                configureDependency);
        }

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configureBuilder">给定的构建器配置。</param>
        /// <param name="configureOptions">给定的配置依赖选项（可选）。</param>
        /// <param name="configureDependency">给定的配置依赖（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCoreByConfig(this IServiceCollection services,
            Func<IConfigurationBuilder, IConfigurationRoot> configureBuilder,
            Action<CoreLibraryOptions> configureOptions = null,
            Action<ILibraryDependency, CoreLibraryOptions> configureDependency = null)
        {
            return services.AddLibrameByConfig(configureBuilder,
                configureOptions,
                configureDependency);
        }

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configureOptions">给定的配置依赖选项（可选）。</param>
        /// <param name="configureDependency">给定的配置依赖（可选）。</param>
        /// <param name="configuration">给定的服务配置（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCore(this IServiceCollection services,
            Action<CoreLibraryOptions> configureOptions = null,
            Action<ILibraryDependency, CoreLibraryOptions> configureDependency = null,
            IConfiguration configuration = null)
        {
            return services.AddLibrame(configureOptions,
                configureDependency,
                configuration);
        }

    }
}
