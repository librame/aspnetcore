#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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


        /// <summary>
        /// 是否包含指定服务。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <returns>返回布尔值。</returns>
        bool ContainsService<TService>();
        /// <summary>
        /// 是否包含指定服务。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回布尔值。</returns>
        bool ContainsService(Type serviceType);
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


        /// <summary>
        /// 是否包含指定服务。
        /// </summary>
        /// <typeparam name="TService">指定的服务类型。</typeparam>
        /// <returns>返回布尔值。</returns>
        public virtual bool ContainsService<TService>()
        {
            return ContainsService(typeof(TService));
        }
        /// <summary>
        /// 是否包含指定服务。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool ContainsService(Type serviceType)
        {
            return (ServiceProvider.GetService(serviceType) != null);
        }

    }


    /// <summary>
    /// Librame 构建器静态扩展。
    /// </summary>
    public static class LibrameBuilderExtensions
    {

        #region Adaptation

        /// <summary>
        /// 使用适配模块（注册指定程序集数组包含的所有适配器）。
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

        #endregion


        #region Algorithm

        /// <summary>
        /// 使用算法模块。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="byteConverterType">给定实现 <see cref="Algorithm.IByteConverter"/> 接口的字节转换器类型（可选）。</param>
        /// <param name="hashAlgorithmType">给定实现 <see cref="Algorithm.Hashes.IHashAlgorithm"/> 接口的散列算法类型（可选）。</param>
        /// <param name="saKeyGeneratorType">给定实现 <see cref="Algorithm.Symmetries.ISAKeyGenerator"/> 接口的对称算法密钥生成器类型（可选）。</param>
        /// <param name="symmetryAlgorithmType">给定实现 <see cref="Algorithm.Symmetries.ISymmetryAlgorithm"/> 接口的对称算法类型（可选）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder UseAlgorithm(this ILibrameBuilder builder, Type byteConverterType = null,
            Type hashAlgorithmType = null, Type saKeyGeneratorType = null, Type symmetryAlgorithmType = null)
        {
            builder.NotNull(nameof(builder));

            if (byteConverterType == null)
                byteConverterType = Type.GetType(builder.Options.Algorithm.ByteConverterTypeName, throwOnError: true);

            if (hashAlgorithmType == null)
                hashAlgorithmType = Type.GetType(builder.Options.Algorithm.HashAlgorithmTypeName, throwOnError: true);

            if (saKeyGeneratorType == null)
                saKeyGeneratorType = Type.GetType(builder.Options.Algorithm.SAKeyGeneratorTypeName, throwOnError: true);

            if (symmetryAlgorithmType == null)
                symmetryAlgorithmType = Type.GetType(builder.Options.Algorithm.SymmetryAlgorithmTypeName, throwOnError: true);

            // 验证类型
            var baseByteConverterType = typeof(Algorithm.IByteConverter);
            byteConverterType = baseByteConverterType.CanAssignableFromType(byteConverterType);

            var baseHashAlgorithmType = typeof(Algorithm.Hashes.IHashAlgorithm);
            hashAlgorithmType = baseHashAlgorithmType.CanAssignableFromType(hashAlgorithmType);

            var baseSAKeyGenerateType = typeof(Algorithm.Symmetries.ISAKeyGenerator);
            saKeyGeneratorType = baseSAKeyGenerateType.CanAssignableFromType(saKeyGeneratorType);

            var baseSymmetryAlgorithmType = typeof(Algorithm.Symmetries.ISymmetryAlgorithm);
            symmetryAlgorithmType = baseSymmetryAlgorithmType.CanAssignableFromType(symmetryAlgorithmType);

            // 注册类型
            builder.Services.AddSingleton(baseByteConverterType, byteConverterType);
            builder.Services.AddSingleton(baseHashAlgorithmType, hashAlgorithmType);
            builder.Services.AddSingleton(baseSAKeyGenerateType, saKeyGeneratorType);
            builder.Services.AddSingleton(baseSymmetryAlgorithmType, symmetryAlgorithmType);

            return builder;
        }

        #endregion


        #region Entity

        /// <summary>
        /// 使用实体模块。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="repositoryType">给定实现 <see cref="Entity.IRepository{TEntity}"/> 接口的仓库类型（可选）。</param>
        /// <param name="connectionString">给定的数据库连接字符串（可选；如果没有在外部注册实体框架数据源，则此处为必填）。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder UseEntity(this ILibrameBuilder builder, Type repositoryType = null,
            string connectionString = null)
        {
            builder.NotNull(nameof(builder));

            // 如果没有在外部注册实体框架数据源
            if (!builder.ContainsService<IDatabaseProvider>())
            {
                builder.Services.AddEntityFrameworkSqlServer().AddDbContext<Entity.Providers.DbContextProvider>(options =>
                {
                    options.UseSqlServer(connectionString.NotNullOrEmpty(nameof(connectionString)), sql =>
                    {
                        sql.UseRowNumberForPaging();
                        sql.MaxBatchSize(50);
                    });
                });
            }
            
            if (repositoryType == null)
                repositoryType = Type.GetType(builder.Options.Entity.RepositoryTypeName, throwOnError: true);
            
            // 验证类型
            var baseType = typeof(Entity.IRepository<>);
            repositoryType = baseType.CanAssignableFromType(repositoryType);

            // 注册类型
            builder.Services.AddTransient(baseType, repositoryType);

            return builder;
        }

        #endregion

    }
}
