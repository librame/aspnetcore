#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Librame 核心服务集合静态扩展。
    /// </summary>
    public static class LibrameCoreServiceCollectionExtensions
    {

        /// <summary>
        /// 注册 LibrameCore 模块集合。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="fileName">给定的 JSON 文件名（可选；默认为 <see cref="LibrameDefaults.JSON_CONFIGURATION_FILENAME"/>）。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <param name="configureRoot">给定的根配置（可选）。</param>
        /// <param name="configureModules">给定的模块配置（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCoreByJson(this IServiceCollection services,
            string fileName = LibrameDefaults.JSON_CONFIGURATION_FILENAME,
            string basePath = null,
            Action<IConfigurationRoot> configureRoot = null,
            Action<ILibrameModuleCollection> configureModules = null)
        {
            var source = new JsonConfigurationSource
            {
                Path = fileName,
                // 如果此配置文件不存在，将抛出异常
                Optional = false,
                // 当文件变换时重载
                ReloadOnChange = true
            };

            return services.AddLibrameCoreByFile(source, basePath, configureRoot, configureModules);
        }

        /// <summary>
        /// 注册 LibrameCore 模块集合。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="source">给定的文件配置源。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <param name="configureRoot">给定的根配置（可选）。</param>
        /// <param name="configureModules">给定的模块配置（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCoreByFile(this IServiceCollection services,
            FileConfigurationSource source,
            string basePath = null,
            Action<IConfigurationRoot> configureRoot = null,
            Action<ILibrameModuleCollection> configureModules = null)
        {
            return services.AddLibrameCoreByConfig(builder =>
            {
                var root = source.LoadRoot(basePath);

                configureRoot?.Invoke(root);

                return root;
            },
            configureModules);
        }

        /// <summary>
        /// 注册 LibrameCore 模块集合。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configureModules">给定的模块配置（可选）。</param>
        /// <param name="configureBuilder">给定的构建器配置（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCoreByConfig(this IServiceCollection services,
            Func<IConfigurationBuilder, IConfigurationRoot> configureBuilder,
            Action<ILibrameModuleCollection> configureModules = null)
        {
            configureBuilder.NotNull(nameof(configureBuilder));

            var builder = new ConfigurationBuilder();

            var root = configureBuilder.Invoke(builder);

            return services.AddLibrameCore(configureModules, root.GetSection(LibrameDefaults.NAME));
        }

        /// <summary>
        /// 注册 LibrameCore 模块集合。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configureModules">给定的模块配置（可选）。</param>
        /// <param name="configuration">给定的服务配置（可选）。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection AddLibrameCore(this IServiceCollection services,
            Action<ILibrameModuleCollection> configureModules = null,
            IConfiguration configuration = null)
        {
            services.AddLibrame(moduleCollection =>
            {
                if (configureModules == null)
                {
                    configureModules = modules =>
                    {
                        // Add Standard Modules
                        modules.AddStandard();

                        try
                        {
                            // Add Core Modules
                            modules.AddCore();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    };
                }

                configureModules.Invoke(moduleCollection);
            },
            configuration);

            return services;
        }

    }
}
