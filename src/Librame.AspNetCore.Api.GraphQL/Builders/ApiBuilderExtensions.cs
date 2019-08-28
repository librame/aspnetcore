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
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddApi(this IExtensionBuilder builder,
            Action<ApiBuilderOptions> setupAction = null)
        {
            return builder.AddApi(b => new ApiBuilder(b), setupAction);
        }

        /// <summary>
        /// 添加 API 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建 API 构建器的工厂方法。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddApi(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IApiBuilder> createFactory,
            Action<ApiBuilderOptions> setupAction = null)
        {
            createFactory.NotNull(nameof(createFactory));

            builder.Services.OnlyConfigure(setupAction);

            var apiBuilder = createFactory.Invoke(builder);

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
