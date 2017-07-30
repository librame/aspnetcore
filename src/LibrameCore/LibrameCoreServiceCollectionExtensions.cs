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
using LibrameStandard.Algorithm;
using LibrameCore.Authentication;
using LibrameStandard.Entity;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Librame 核心服务集合静态扩展。
    /// </summary>
    public static class LibrameCoreServiceCollectionExtensions
    {

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="initialDataAction">给定的初始化数据动作方法（可选）。</param>
        /// <param name="algorithmOptionsAction">给定的算法选项动作方法（可选）。</param>
        /// <param name="entityOptionsAction">给定的实体选项动作方法（可选）。</param>
        /// <param name="authenticationAction">给定的认证选项动作方法（可选）。</param>
        /// <param name="builderAction">给定的自定义配置 Librame 构建器动作方法（可选）。</param>
        /// <returns>返回服务集合。</returns>
        public static IServiceCollection AddLibrameCoreByMemory(this IServiceCollection services,
            Action<Dictionary<string, string>> initialDataAction = null,
            Action<AlgorithmOptions> algorithmOptionsAction = null,
            Action<EntityOptions> entityOptionsAction = null,
            Action<AuthenticationOptions> authenticationAction = null,
            Action<ILibrameBuilder> builderAction = null)
        {
            var authenticationKeyPrefix = AuthenticationOptions.KeyPrefix;

            Action<Dictionary<string, string>> coreInitialDataAction = initialData =>
            {
                // Authentication
                initialData.Add(authenticationKeyPrefix + TokenOptions.IssuerKey,
                    TokenOptions.DefaultIssuer);

                initialData.Add(authenticationKeyPrefix + TokenOptions.AudienceKey,
                    TokenOptions.DefaultAudience);

                initialDataAction?.Invoke(initialData);
            };

            return services.AddLibrameByMemory(coreInitialDataAction,
                algorithmOptionsAction, entityOptionsAction, builder =>
                {
                    // Add Authentication Services
                    builder.AddAuthentication(authenticationAction);

                    builderAction?.Invoke(builder);
                });
        }


        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="fileName">给定的 JSON 文件名（可选；默认为 <see cref="LibrameDefaults.JSON_CONFIGURATION_FILENAME"/>）。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <param name="key">给定的 Librame 配置部分键名（可选；默认为 <see cref="LibrameDefaults.NAME"/>）。</param>
        /// <param name="algorithmOptionsAction">给定的算法选项动作方法（可选）。</param>
        /// <param name="entityOptionsAction">给定的实体选项动作方法（可选）。</param>
        /// <param name="authenticationAction">给定的认证选项动作方法（可选）。</param>
        /// <param name="builderAction">给定的自定义配置 Librame 构建器动作方法（可选）。</param>
        /// <returns>返回服务集合。</returns>
        public static IServiceCollection AddLibrameCoreByJsonFile(this IServiceCollection services,
            string fileName = LibrameDefaults.JSON_CONFIGURATION_FILENAME,
            string basePath = null,
            string key = LibrameDefaults.NAME,
            Action<AlgorithmOptions> algorithmOptionsAction = null,
            Action<EntityOptions> entityOptionsAction = null,
            Action<AuthenticationOptions> authenticationAction = null,
            Action<ILibrameBuilder> builderAction = null)
        {
            return services.AddLibrameByJsonFile(fileName, basePath, key, algorithmOptionsAction, entityOptionsAction, builder =>
            {
                // Add Authentication Services
                builder.AddAuthentication(authenticationAction);

                builderAction?.Invoke(builder);
            });
        }

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="source">给定的文件配置源。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <param name="key">给定的 Librame 配置部分键名（可选；默认为 <see cref="LibrameDefaults.NAME"/>）。</param>
        /// <param name="algorithmOptionsAction">给定的算法选项动作方法（可选）。</param>
        /// <param name="entityOptionsAction">给定的实体选项动作方法（可选）。</param>
        /// <param name="authenticationAction">给定的认证选项动作方法（可选）。</param>
        /// <param name="builderAction">给定的自定义配置 Librame 构建器动作方法（可选）。</param>
        /// <returns>返回服务集合。</returns>
        public static IServiceCollection AddLibrameCoreByFile(this IServiceCollection services,
            FileConfigurationSource source,
            string basePath = null,
            string key = LibrameDefaults.NAME,
            Action<AlgorithmOptions> algorithmOptionsAction = null,
            Action<EntityOptions> entityOptionsAction = null,
            Action<AuthenticationOptions> authenticationAction = null,
            Action<ILibrameBuilder> builderAction = null)
        {
            return services.AddLibrameByFile(source, basePath, key, algorithmOptionsAction, entityOptionsAction, builder =>
            {
                // Add Authentication Services
                builder.AddAuthentication(authenticationAction);

                builderAction?.Invoke(builder);
            });
        }


        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configuration">给定的 Librame 配置对象。</param>
        /// <param name="algorithmOptionsAction">给定的算法选项动作方法（可选）。</param>
        /// <param name="entityOptionsAction">给定的实体选项动作方法（可选）。</param>
        /// <param name="authenticationAction">给定的认证选项动作方法（可选）。</param>
        /// <param name="builderAction">给定的自定义配置 Librame 构建器动作方法（可选）。</param>
        /// <returns>返回服务集合。</returns>
        public static IServiceCollection AddLibrameCore(this IServiceCollection services,
            IConfiguration configuration,
            Action<AlgorithmOptions> algorithmOptionsAction = null,
            Action<EntityOptions> entityOptionsAction = null,
            Action<AuthenticationOptions> authenticationAction = null,
            Action<ILibrameBuilder> builderAction = null)
        {
            services.AddAuthorization();

            return services.AddLibrame(configuration, algorithmOptionsAction, entityOptionsAction, builder =>
            {
                // Add Authentication
                builder.AddAuthentication(authenticationAction);

                builderAction?.Invoke(builder);
            });
        }

    }
}
