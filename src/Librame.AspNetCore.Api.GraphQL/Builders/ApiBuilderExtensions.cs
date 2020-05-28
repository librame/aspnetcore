#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Api.Builders;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// API 构建器静态扩展。
    /// </summary>
    public static class ApiBuilderExtensions
    {
        /// <summary>
        /// 添加 API 扩展。
        /// </summary>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddApi(this IExtensionBuilder parentBuilder,
            Action<ApiBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, ApiBuilderDependency, IApiBuilder> builderFactory = null)
            => parentBuilder.AddApi<ApiBuilderDependency>(configureDependency, builderFactory);

        /// <summary>
        /// 添加 API 扩展。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IApiBuilder AddApi<TDependency>(this IExtensionBuilder parentBuilder,
            Action<TDependency> configureDependency = null,
            Func<IExtensionBuilder, TDependency, IApiBuilder> builderFactory = null)
            where TDependency : ApiBuilderDependency
        {
            // 如果已经添加过 API 扩展，则直接返回，防止出现重复配置的情况
            if (parentBuilder.TryGetParentBuilder(out IApiBuilder apiBuilder))
                return new ApiBuilderAdapter(parentBuilder, apiBuilder);

            // Clear Options Cache
            ConsistencyOptionsCache.TryRemove<ApiBuilderOptions>();

            // Add Builder Dependency
            var dependency = parentBuilder.AddBuilderDependency(out var dependencyType, configureDependency);
            parentBuilder.Services.TryAddReferenceBuilderDependency<ApiBuilderDependency>(dependency, dependencyType);

            // Create Builder
            return builderFactory.NotNullOrDefault(()
                => (b, d) => new ApiBuilder(b, d)).Invoke(parentBuilder, dependency);
        }

    }
}
