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
using Librame.Extensions;
using Librame.Extensions.Core;
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
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="builderAction">给定的选项配置动作。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddApi(this IExtensionBuilder builder,
            Action<ApiBuilderOptions> builderAction,
            Func<IExtensionBuilder, ApiBuilderDependencyOptions, IApiBuilder> builderFactory = null)
        {
            builderAction.NotNull(nameof(builderAction));

            return builder.AddApi(dependency =>
            {
                dependency.Builder.Action = builderAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加 API 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddApi(this IExtensionBuilder builder,
            Action<ApiBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, ApiBuilderDependencyOptions, IApiBuilder> builderFactory = null)
            => builder.AddApi<ApiBuilderDependencyOptions>(dependencyAction, builderFactory);

        /// <summary>
        /// 添加 API 扩展。
        /// </summary>
        /// <typeparam name="TDependencyOptions">指定的依赖类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IApiBuilder AddApi<TDependencyOptions>(this IExtensionBuilder builder,
            Action<TDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, TDependencyOptions, IApiBuilder> builderFactory = null)
            where TDependencyOptions : ApiBuilderDependencyOptions, new()
        {
            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependency();
            builder.Services.AddAllOptionsConfigurators(dependency);

            // Create Builder
            var apiBuilder = builderFactory.NotNullOrDefault(()
                => (b, d) => new ApiBuilder(b, d)).Invoke(builder, dependency);

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
