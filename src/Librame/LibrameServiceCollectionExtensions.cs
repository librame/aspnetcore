#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame;
using Librame.Algorithm;
using Librame.Entity;
using Librame.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Librame 服务集合静态扩展。
    /// </summary>
    public static class LibrameServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Librame 服务（通过配置内存源）。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="optionsAction">给定的键值对字典选项集合动作（可选）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameByMemory(this IServiceCollection services,
            Action<Dictionary<string, string>> optionsAction = null)
        {
            var configurationSource = new Dictionary<string, string>
            {
                { LibrameOptions.AuthIdKey, LibrameOptions.DefaultAuthId },
                { LibrameOptions.BaseDirectoryKey, LibrameOptions.DefaultBaseDirectory },
                { LibrameOptions.EncodingKey, LibrameOptions.DefaultEncoding },

                // Algorithm
                {
                    AlgorithmOptions.PlainTextCodecTypeNameKey,
                    AlgorithmOptions.DefaultPlainTextCodecTypeName
                },
                {
                    AlgorithmOptions.CipherTextCodecTypeNameKey,
                    AlgorithmOptions.DefaultCipherTextCodecTypeName
                },

                {
                    AlgorithmOptions.SymmetryKeyGeneratorTypeNameKey,
                    AlgorithmOptions.DefaultSymmetryKeyGeneratorTypeName
                },
                {
                    AlgorithmOptions.SymmetryAlgorithmTypeNameKey,
                    AlgorithmOptions.DefaultSymmetryAlgorithmTypeName
                },

                {
                    AlgorithmOptions.RsaPublicKeyStringKey,
                    AlgorithmOptions.DefaultRsaPublicKeyString
                },
                {
                    AlgorithmOptions.RsaPrivateKeyStringKey,
                    AlgorithmOptions.DefaultRsaPrivateKeyString
                },
                {
                    AlgorithmOptions.RsaAsymmetryKeyGeneratorTypeNameKey,
                    AlgorithmOptions.DefaultRsaAsymmetryKeyGeneratorTypeName
                },
                {
                    AlgorithmOptions.RsaAsymmetryAlgorithmTypeNameKey,
                    AlgorithmOptions.DefaultRsaAsymmetryAlgorithmTypeName
                },

                {
                    AlgorithmOptions.HashAlgorithmTypeNameKey,
                    AlgorithmOptions.DefaultHashAlgorithmTypeName
                },

                // Entity
                {
                    EntityOptions.AutomappingAssembliesKey,
                    EntityOptions.DefaultAutomappingAssemblies
                },
                {
                    EntityOptions.EnableAutomappingKey,
                    EntityOptions.DefaultEnableAutomapping.ToString()
                },
                {
                    EntityOptions.EntityProviderTypeNameKey,
                    EntityOptions.DefaultEntityProviderTypeName
                },
                {
                    EntityOptions.RepositoryTypeNameKey,
                    EntityOptions.DefaultRepositoryTypeName
                }
            };

            if (optionsAction != null)
                optionsAction.Invoke(configurationSource);

            return services.AddLibrame(config =>
            {
                var builder = config.Add(new MemoryConfigurationSource { InitialData = configurationSource });

                return builder.Build();
            });
        }

        /// <summary>
        /// 注册 Librame 服务（通过配置 JSON 文件源）。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="jsonFileRelativePath">给定的 JSON 文件相对路径配置源。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrameByJson(this IServiceCollection services, string jsonFileRelativePath)
        {
            return services.AddLibrame(config =>
            {
                var builder = config.SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile(jsonFileRelativePath, optional: false, reloadOnChange: true);

                var configurationRoot = builder.Build();

                return configurationRoot.GetSection(LibrameConstants.NAME);
            });
        }

        /// <summary>
        /// 注册 Librame 服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configFactory">配置构建器工厂方法。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrame(this IServiceCollection services,
            Func<IConfigurationBuilder, IConfiguration> configFactory)
        {
            configFactory.NotNull(nameof(configFactory));

            try
            {
                var configurationBuilder = new ConfigurationBuilder();

                var configuration = configFactory.Invoke(configurationBuilder);

                return services.AddLibrame(configuration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 注册 Librame 服务。
        /// </summary>
        /// <param name="services">给定的服务集合。</param>
        /// <param name="configuration">给定的 Librame 配置接口。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder AddLibrame(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.NotNull(nameof(configuration));

            // 注入 LibrameOptions (使用 Options 模式)
            services.AddOptions()
                .Configure<LibrameOptions>(configuration); // options => configuration.Bind(options)
            
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
            builder.TryAddAdaptation(AssemblyUtil.CurrentAssembly);

            return builder;
        }

    }
}
