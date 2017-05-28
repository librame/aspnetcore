#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore;
using LibrameCore.Authentication;
using LibrameCore.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Librame MVC 服务集合静态扩展。
    /// </summary>
    public static class LibrameMvcServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Librame MVC 服务（通过配置内存源）。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="optionsAction">给定的键值对字典选项集合动作（可选）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameMvcByMemory(this IServiceCollection services,
            Action<IDictionary<string, string>> optionsAction = null)
        {
            var configurationSource = LibrameConfigurationExtensions.InitialLibrameOptions();

            // Authentication
            configurationSource.Add(AuthenticationOptions.UserAuthenticationTypeNameKey,
                AuthenticationOptions.DefaultUserAuthenticationTypeName);

            if (optionsAction != null)
                optionsAction.Invoke(configurationSource);

            return services.AddLibrameMvc(config =>
            {
                var builder = config.Add(new MemoryConfigurationSource { InitialData = configurationSource });

                return builder.Build();
            });
        }

        /// <summary>
        /// 注册 Librame MVC 服务（通过配置 JSON 文件源）。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="jsonFileRelativePath">给定的 JSON 文件相对路径配置源。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameMvcByJson(this IServiceCollection services, string jsonFileRelativePath)
        {
            return services.AddLibrameMvc(config =>
            {
                var builder = config.SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile(jsonFileRelativePath, optional: false, reloadOnChange: true);

                var configurationRoot = builder.Build();

                return configurationRoot.GetSection(LibrameConstants.NAME);
            });
        }

        /// <summary>
        /// 注册 Librame MVC 服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configFactory">配置构建器工厂方法。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameMvc(this IServiceCollection services,
            Func<IConfigurationBuilder, IConfiguration> configFactory)
        {
            configFactory.NotNull(nameof(configFactory));

            try
            {
                var configurationBuilder = new ConfigurationBuilder();

                var configuration = configFactory.Invoke(configurationBuilder);

                return services.AddLibrameMvc(configuration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 注册 Librame MVC 服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configuration">给定的 Librame 配置接口。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameMvc(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.NotNull(nameof(configuration));

            // 注入 LibrameOptions (使用 Options 模式)
            services.AddOptions()
                .Configure<LibrameMvcOptions>(configuration);

            // 构造并注入 LibrameBuilder
            var builder = new LibrameBuilder(services)
            {
                // 绑定配置，以便集成注入实体模块的连接字符串
                Configuration = configuration
            };

            // 如果还未注册记录器
            if (!builder.ContainsService<ILogger<ILibrameBuilder>>())
                builder.Services.AddLogging();

            // 添加适配器模块（默认添加当前程序集的所有适配器模块）
            builder.TryAddAdaptation(AssemblyUtil.CurrentAssembly,
                TypeUtil.GetAssembly<LibrameMvcOptions>());
            
            return builder;
        }

    }
}
