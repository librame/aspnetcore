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
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Librame
{
    using Utility;

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


        /// <summary>
        /// 获取适配器集合。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="types">用于快速注册的适配器类型数组（如果之前使用适配模块时已注册，则此参数将会被忽略）。</param>
        /// <returns>返回集合。</returns>
        public static IEnumerable<Adaptation.IAdapter> GetAdapters(this ILibrameBuilder builder, params Type[] types)
        {
            if (!builder.ContainsService<Adaptation.IAdapter>())
                builder.UseAdaptation(types);

            return builder.ServiceProvider.GetServices<Adaptation.IAdapter>();
        }

        #endregion


        #region Algorithm

        /// <summary>
        /// 使用算法模块。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="options">给定的算法选项。</param>
        /// <returns>返回 Librame 构建器接口。</returns>
        public static ILibrameBuilder UseAlgorithm(this ILibrameBuilder builder, Algorithm.AlgorithmOptions options = null)
        {
            builder.NotNull(nameof(builder));

            if (options == null)
                options = builder.Options.Algorithm;
            
            // 验证类型
            var basePlainTextCodecType = typeof(Algorithm.TextCodecs.IPlainTextCodec);
            var implPlainTextCodecType = Type.GetType(options.PlainTextCodecTypeName, throwOnError: true);
            implPlainTextCodecType = basePlainTextCodecType.CanAssignableFromType(implPlainTextCodecType);

            var baseCipherTextCodecType = typeof(Algorithm.TextCodecs.ICipherTextCodec);
            var implCipherTextCodecType = Type.GetType(options.CipherTextCodecTypeName, throwOnError: true);
            implCipherTextCodecType = baseCipherTextCodecType.CanAssignableFromType(implCipherTextCodecType);

            // 对称算法
            var baseSymmetryKeyGenerateType = typeof(Algorithm.Symmetries.ISymmetryKeyGenerator);
            var implSymmetryKeyGenerateType = Type.GetType(options.SymmetryKeyGeneratorTypeName, throwOnError: true);
            implSymmetryKeyGenerateType = baseSymmetryKeyGenerateType.CanAssignableFromType(implSymmetryKeyGenerateType);

            var baseSymmetryAlgorithmType = typeof(Algorithm.Symmetries.ISymmetryAlgorithm);
            var implSymmetryAlgorithmType = Type.GetType(options.SymmetryAlgorithmTypeName, throwOnError: true);
            implSymmetryAlgorithmType = baseSymmetryAlgorithmType.CanAssignableFromType(implSymmetryAlgorithmType);

            // 非对称算法
            var baseRsaAsymmetryKeyGenerateType = typeof(Algorithm.Asymmetries.IRsaAsymmetryKeyGenerator);
            var implRsaAsymmetryKeyGenerateType = Type.GetType(options.RsaAsymmetryKeyGeneratorTypeName, throwOnError: true);
            implRsaAsymmetryKeyGenerateType = baseRsaAsymmetryKeyGenerateType.CanAssignableFromType(implRsaAsymmetryKeyGenerateType);

            var baseRsaAsymmetryAlgorithmType = typeof(Algorithm.Asymmetries.IRsaAsymmetryAlgorithm);
            var implRsaAsymmetryAlgorithmType = Type.GetType(options.RsaAsymmetryAlgorithmTypeName, throwOnError: true);
            implRsaAsymmetryAlgorithmType = baseRsaAsymmetryAlgorithmType.CanAssignableFromType(implRsaAsymmetryAlgorithmType);

            // 散列算法
            var baseHashAlgorithmType = typeof(Algorithm.Hashes.IHashAlgorithm);
            var implHashAlgorithmType = Type.GetType(options.HashAlgorithmTypeName, throwOnError: true);
            baseHashAlgorithmType = baseHashAlgorithmType.CanAssignableFromType(baseHashAlgorithmType);
            
            // 注册类型
            builder.Services.AddSingleton(basePlainTextCodecType, implPlainTextCodecType);
            builder.Services.AddSingleton(baseCipherTextCodecType, implCipherTextCodecType);

            builder.Services.AddSingleton(baseSymmetryKeyGenerateType, implSymmetryKeyGenerateType);
            builder.Services.AddSingleton(baseSymmetryAlgorithmType, implSymmetryAlgorithmType);

            builder.Services.AddSingleton(baseRsaAsymmetryKeyGenerateType, implRsaAsymmetryKeyGenerateType);
            builder.Services.AddSingleton(baseRsaAsymmetryAlgorithmType, implRsaAsymmetryAlgorithmType);

            builder.Services.AddSingleton(baseHashAlgorithmType, implHashAlgorithmType);
            
            return builder;
        }


        /// <summary>
        /// 获取明文编解码器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回算法接口。</returns>
        public static Algorithm.TextCodecs.IPlainTextCodec GetPlainTextCodec(this ILibrameBuilder builder)
        {
            if (!builder.ContainsService<Algorithm.TextCodecs.IPlainTextCodec>())
                builder.UseAlgorithm();

            return builder.ServiceProvider.GetService<Algorithm.TextCodecs.IPlainTextCodec>();
        }

        /// <summary>
        /// 获取密文编解码器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回算法接口。</returns>
        public static Algorithm.TextCodecs.ICipherTextCodec GetCipherTextCodec(this ILibrameBuilder builder)
        {
            if (!builder.ContainsService<Algorithm.TextCodecs.ICipherTextCodec>())
                builder.UseAlgorithm();

            return builder.ServiceProvider.GetService<Algorithm.TextCodecs.ICipherTextCodec>();
        }


        /// <summary>
        /// 获取 RSA 非对称算法。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回算法接口。</returns>
        public static Algorithm.Asymmetries.IRsaAsymmetryAlgorithm GetRsaAsymmetryAlgorithm(this ILibrameBuilder builder)
        {
            if (!builder.ContainsService<Algorithm.Asymmetries.IRsaAsymmetryAlgorithm>())
                builder.UseAlgorithm();

            return builder.ServiceProvider.GetService<Algorithm.Asymmetries.IRsaAsymmetryAlgorithm>();
        }


        /// <summary>
        /// 获取散列算法。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回算法接口。</returns>
        public static Algorithm.Hashes.IHashAlgorithm GetHashAlgorithm(this ILibrameBuilder builder)
        {
            if (!builder.ContainsService<Algorithm.Hashes.IHashAlgorithm>())
                builder.UseAlgorithm();

            return builder.ServiceProvider.GetService<Algorithm.Hashes.IHashAlgorithm>();
        }


        /// <summary>
        /// 获取对称算法。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <returns>返回算法接口。</returns>
        public static Algorithm.Symmetries.ISymmetryAlgorithm GetSymmetryAlgorithm(this ILibrameBuilder builder)
        {
            if (!builder.ContainsService<Algorithm.Symmetries.ISymmetryAlgorithm>())
                builder.UseAlgorithm();

            return builder.ServiceProvider.GetService<Algorithm.Symmetries.ISymmetryAlgorithm>();
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
                    options.UseSqlServer(connectionString.NotEmpty(nameof(connectionString)), sql =>
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


        /// <summary>
        /// 获取实体仓库。
        /// </summary>
        /// <typeparam name="TEntity">指定的实体类型。</typeparam>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="repositoryType">给定实现 <see cref="Entity.IRepository{TEntity}"/> 接口的仓库类型（如果之前使用实体模块时已注册，则此参数将会被忽略）。</param>
        /// <param name="connectionString">给定的数据库连接字符串（如果之前使用实体模块时已注册，则此参数将会被忽略）。</param>
        /// <returns>返回仓库实例。</returns>
        public static Entity.IRepository<TEntity> GetRepository<TEntity>(this ILibrameBuilder builder,
            Type repositoryType = null, string connectionString = null)
            where TEntity : class
        {
            if (!builder.ContainsService(typeof(Entity.IRepository<>)))
                builder.UseEntity(repositoryType, connectionString);

            return builder.ServiceProvider.GetService<Entity.IRepository<TEntity>>();
        }

        #endregion

    }
}
