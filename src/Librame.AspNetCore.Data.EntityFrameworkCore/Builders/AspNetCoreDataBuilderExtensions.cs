#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Data.Builders;
using Librame.Extensions.Data.Services;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IDataBuilder"/> for ASP.NET Core 静态扩展。
    /// </summary>
    public static class AspNetCoreDataBuilderExtensions
    {
        /// <summary>
        /// 添加 Data for ASP.NET Core。
        /// </summary>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureOptions">给定的配置选项动作方法。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore(this IExtensionBuilder parentBuilder,
            Action<DataBuilderOptions> configureOptions,
            Func<IExtensionBuilder, DataBuilderDependency, IDataBuilder> builderFactory = null)
        {
            configureOptions.NotNull(nameof(configureOptions));

            return parentBuilder.AddDataCore(dependency =>
            {
                dependency.Builder.ConfigureOptions = configureOptions;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加 Data for ASP.NET Core。
        /// </summary>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore(this IExtensionBuilder parentBuilder,
            Action<DataBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, DataBuilderDependency, IDataBuilder> builderFactory = null)
            => parentBuilder.AddDataCore<DataBuilderDependency>(configureDependency, builderFactory);

        /// <summary>
        /// 添加 Data for ASP.NET Core。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore<TDependency>(this IExtensionBuilder parentBuilder,
            Action<TDependency> configureDependency = null,
            Func<IExtensionBuilder, TDependency, IDataBuilder> builderFactory = null)
            where TDependency : DataBuilderDependency, new()
        {
            return parentBuilder.AddData(configureDependency, builderFactory)
                .AddServices();
        }

    }
}
