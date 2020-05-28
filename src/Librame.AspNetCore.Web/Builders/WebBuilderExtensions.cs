#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Web.Builders;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Web 构建器静态扩展。
    /// </summary>
    public static class WebBuilderExtensions
    {
        /// <summary>
        /// 启用支持泛型控制器。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder EnableSupportedGenericController(this IWebBuilder builder)
            => builder.SetProperty(p => p.SupportedGenericController, true);

        /// <summary>
        /// 添加用户类型。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="userType">给定的用户类型。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddUser(this IWebBuilder builder, Type userType)
            => builder.SetProperty(p => p.UserType, userType);


        /// <summary>
        /// 添加 Web 扩展。
        /// </summary>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 Web 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddWeb(this IExtensionBuilder parentBuilder,
            Action<WebBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, WebBuilderDependency, IWebBuilder> builderFactory = null)
            => parentBuilder.AddWeb<WebBuilderDependency>(configureDependency, builderFactory);

        /// <summary>
        /// 添加 Web 扩展。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 Web 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static IWebBuilder AddWeb<TDependency>(this IExtensionBuilder parentBuilder,
            Action<TDependency> configureDependency = null,
            Func<IExtensionBuilder, TDependency, IWebBuilder> builderFactory = null)
            where TDependency : WebBuilderDependency
        {
            // 如果已经添加过 Web 扩展，则直接返回，防止出现重复配置的情况
            if (parentBuilder.TryGetParentBuilder(out IWebBuilder webBuilder))
                return new WebBuilderAdapter(parentBuilder, webBuilder);

            // Clear Options Cache
            ConsistencyOptionsCache.TryRemove<WebBuilderOptions>();

            // Add Builder Dependency
            var dependency = parentBuilder.AddBuilderDependency(out var dependencyType, configureDependency);
            parentBuilder.Services.TryAddReferenceBuilderDependency<WebBuilderDependency>(dependency, dependencyType);

            // Create Builder
            return builderFactory.NotNullOrDefault(()
                => (b, d) => new WebBuilder(b, d)).Invoke(parentBuilder, dependency);
        }

    }
}
