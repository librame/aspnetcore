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
using LibrameStandard.Authentication;
using LibrameStandard.Entity;
using LibrameStandard.Entity.DbContexts;
using LibrameStandard.Handlers;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
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
        /// 注册 Librame 核心服务（通过配置内存源）。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="initialDataAction">给定的初始化选项数据动作（可选）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCoreByMemory(this IServiceCollection services,
            Action<IDictionary<string, string>> initialDataAction = null)
        {
            return services.AddLibrameCoreByMemory<LibrameCoreOptions>(initialDataAction);
        }
        /// <summary>
        /// 注册 Librame 核心服务（通过配置内存源）。
        /// </summary>
        /// <typeparam name="TLibrameOptions">指定的 Librame 选项类型。</typeparam>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="initialDataAction">给定的初始化选项数据动作（可选）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCoreByMemory<TLibrameOptions>(this IServiceCollection services,
            Action<IDictionary<string, string>> initialDataAction = null)
            where TLibrameOptions : class, ILibrameOptions, new()
        {
            var initialData = LibrameConfigurationExtensions.InitialLibrameOptions();

            // 修改默认的数据库上下文类型名
            initialData[EntityAutomappingSetting.GetAutomappingDbContextTypeNameKey(0)]
                = typeof(SqlServerDbContextReader).AsAssemblyQualifiedNameWithoutVCP();
            initialData[EntityAutomappingSetting.GetAutomappingDbContextTypeNameKey(1)]
                = typeof(SqlServerDbContextWriter).AsAssemblyQualifiedNameWithoutVCP();

            // Authentication
            initialData.Add(AuthenticationHandlersSetting.TokenHandlerTypeNameKey,
                AuthenticationHandlersSetting.DefaultTokenHandlerTypeName);

            initialData.Add(AuthenticationManagersSetting.PasswordManagerTypeNameKey,
                AuthenticationManagersSetting.DefaultPasswordManagerTypeName);
            initialData.Add(AuthenticationManagersSetting.RoleManagerTypeNameKey,
                AuthenticationManagersSetting.DefaultRoleManagerTypeName);
            initialData.Add(AuthenticationManagersSetting.TokenManagerTypeNameKey,
                AuthenticationManagersSetting.DefaultTokenManagerTypeName);
            initialData.Add(AuthenticationManagersSetting.UserManagerTypeNameKey,
                AuthenticationManagersSetting.DefaultUserManagerTypeName);

            initialData.Add(AuthenticationModelsSetting.RoleModelTypeNameKey,
                AuthenticationModelsSetting.DefaultRoleModelTypeName);
            initialData.Add(AuthenticationModelsSetting.TokenModelTypeNameKey,
                AuthenticationModelsSetting.DefaultTokenModelTypeName);
            initialData.Add(AuthenticationModelsSetting.UserModelTypeNameKey,
                AuthenticationModelsSetting.DefaultUserModelTypeName);

            initialData.Add(AuthenticationSendersSetting.EmailSenderTypeNameKey,
                AuthenticationSendersSetting.DefaultEmailSenderTypeName);
            initialData.Add(AuthenticationSendersSetting.SmsSenderTypeNameKey,
                AuthenticationSendersSetting.DefaultSmsSenderTypeName);

            if (initialDataAction != null)
                initialDataAction.Invoke(initialData);

            return services.AddLibrameCore(config =>
            {
                var builder = config.Add(new MemoryConfigurationSource { InitialData = initialData });

                return builder.Build();
            });
        }

        /// <summary>
        /// 注册 Librame 核心服务（通过配置 JSON 文件源）。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="fileName">给定的 JSON 文件名。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCoreByJson(this IServiceCollection services,
            string fileName, string basePath = null)
        {
            return services.AddLibrameCoreByJson<LibrameCoreOptions>(fileName, basePath);
        }
        /// <summary>
        /// 注册 Librame 核心服务（通过配置 JSON 文件源）。
        /// </summary>
        /// <typeparam name="TLibrameOptions">指定的 Librame 选项类型。</typeparam>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="fileName">给定的 JSON 文件名。</param>
        /// <param name="basePath">给定的基础路径（可选；默认为 <see cref="PathUtility.BaseDirectory"/>）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCoreByJson<TLibrameOptions>(this IServiceCollection services,
            string fileName, string basePath = null)
            where TLibrameOptions : class, ILibrameOptions, new()
        {
            basePath = basePath.AsOrDefault(PathUtility.BaseDirectory);

            return services.AddLibrameCore<TLibrameOptions>(config =>
            {
                var builder = config.SetBasePath(basePath)
                    .AddJsonFile(fileName, optional: false, reloadOnChange: true);

                var configurationRoot = builder.Build();

                return configurationRoot.GetSection(LibrameConstants.NAME);
            });
        }

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configFactory">配置构建器工厂方法。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCore(this IServiceCollection services,
            Func<IConfigurationBuilder, IConfiguration> configFactory)
        {
            return services.AddLibrameCore<LibrameCoreOptions>(configFactory);
        }
        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <typeparam name="TLibrameOptions">指定的 Librame 选项类型。</typeparam>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configFactory">配置构建器工厂方法。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCore<TLibrameOptions>(this IServiceCollection services,
            Func<IConfigurationBuilder, IConfiguration> configFactory)
            where TLibrameOptions : class, ILibrameOptions, new()
        {
            configFactory.NotNull(nameof(configFactory));

            try
            {
                var configurationBuilder = new ConfigurationBuilder();

                var configuration = configFactory.Invoke(configurationBuilder);

                return services.AddLibrameCore(configuration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configuration">给定的 Librame 配置接口。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCore(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddLibrameCore<LibrameCoreOptions>(configuration);
        }
        /// <summary>
        /// 注册 Librame 核心服务。
        /// </summary>
        /// <typeparam name="TLibrameOptions">指定的 Librame 选项类型。</typeparam>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configuration">给定的 Librame 配置接口。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameCore<TLibrameOptions>(this IServiceCollection services,
            IConfiguration configuration)
            where TLibrameOptions : class, ILibrameOptions, new()
        {
            var builder = services.AddLibrame<TLibrameOptions>(configuration);
            
            // 添加核心程序集的所有适配器模块
            builder.TryAddAdaptation(typeof(HandlerSettings).AsAssembly());
            
            return builder;
        }

    }
}
