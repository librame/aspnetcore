#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL;
using GraphQL.Http;
using Librame.AspNetCore.Api;
using Librame.AspNetCore.Api.Builders;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    using Extensions;

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
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "parentBuilder")]
        public static IApiBuilder AddApi<TDependency>(this IExtensionBuilder parentBuilder,
            Action<TDependency> configureDependency = null,
            Func<IExtensionBuilder, TDependency, IApiBuilder> builderFactory = null)
            where TDependency : ApiBuilderDependency, new()
        {
            parentBuilder.NotNull(nameof(parentBuilder));

            // Configure Dependency
            var dependency = configureDependency.ConfigureDependency(parentBuilder);

            // Create Builder
            var apiBuilder = builderFactory.NotNullOrDefault(()
                => (b, d) => new ApiBuilder(b, d)).Invoke(parentBuilder, dependency);

            // Configure Builder
            return apiBuilder
                .AddGraphQL();
        }

        private static IApiBuilder AddGraphQL(this IApiBuilder builder)
        {
            builder.Services.TryAddSingleton<IDocumentWriter, DocumentWriter>();
            builder.Services.TryAddSingleton<IDocumentExecuter, DocumentExecuter>();

            builder.Services.TryAddScoped<IGraphApiMutation, GraphApiMutation>();
            builder.Services.TryAddScoped<IGraphApiQuery, GraphApiQuery>();
            builder.Services.TryAddScoped<IGraphApiSubscription, GraphApiSubscription>();
            builder.Services.TryAddScoped<IGraphApiSchema, GraphApiSchema>();

            return builder;
        }

    }
}
