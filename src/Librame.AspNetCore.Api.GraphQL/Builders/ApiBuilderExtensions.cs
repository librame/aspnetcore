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
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Api
{
    using Extensions;
    using Extensions.Core;

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
            Func<IExtensionBuilder, IApiBuilder> builderFactory = null)
        {
            builderAction.NotNull(nameof(builderAction));

            return builder.AddApi(dependency =>
            {
                dependency.BuilderOptionsAction = builderAction;
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
            Func<IExtensionBuilder, IApiBuilder> builderFactory = null)
        {
            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependencyOptions();

            // Add Builder
            builder.Services.OnlyConfigure(dependency.BuilderOptionsAction,
                dependency.BuilderOptionsName);

            var apiBuilder = builderFactory.NotNullOrDefault(()
                => b => new ApiBuilder(b)).Invoke(builder);

            return apiBuilder
                .AddGraphQL();
        }

        private static IApiBuilder AddGraphQL(this IApiBuilder builder)
        {
            builder.Services.AddSingleton<IDocumentWriter, DocumentWriter>();
            builder.Services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            builder.Services.AddScoped<IGraphApiMutation, GraphApiMutation>();
            builder.Services.AddScoped<IGraphApiQuery, GraphApiQuery>();
            builder.Services.AddScoped<IGraphApiSubscription, GraphApiSubscription>();
            builder.Services.AddScoped<IGraphApiSchema, GraphApiSchema>();

            return builder;
        }

    }
}
