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
using Librame.Extensions.Core;
using Librame.Extensions.Data;
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
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="builderAction">给定的选项配置动作。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore(this IExtensionBuilder builder,
            Action<DataBuilderOptions> builderAction,
            Func<IExtensionBuilder, DataBuilderDependencyOptions, IDataBuilder> builderFactory = null)
        {
            builderAction.NotNull(nameof(builderAction));

            return builder.AddDataCore(dependency =>
            {
                dependency.Builder.Action = builderAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加 Data for ASP.NET Core。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore(this IExtensionBuilder builder,
            Action<DataBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, DataBuilderDependencyOptions, IDataBuilder> builderFactory = null)
            => builder.AddDataCore<DataBuilderDependencyOptions>(dependencyAction, builderFactory);

        /// <summary>
        /// 添加 Data for ASP.NET Core。
        /// </summary>
        /// <typeparam name="TDependencyOptions">指定的依赖类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder AddDataCore<TDependencyOptions>(this IExtensionBuilder builder,
            Action<TDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, TDependencyOptions, IDataBuilder> builderFactory = null)
            where TDependencyOptions : DataBuilderDependencyOptions, new()
        {
            return builder.AddData(dependencyAction, builderFactory)
                .AddServices();
        }

    }
}
