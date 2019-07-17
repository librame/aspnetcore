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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Api
{
    using Extensions.Core;

    /// <summary>
    /// API 构建器静态扩展。
    /// </summary>
    public static class ApiBuilderExtensions
    {
        /// <summary>
        /// 添加 API 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{ApiBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddApi(this IBuilder builder,
            Action<ApiBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
        {
            var options = builder.Configure(configureOptions,
                configuration, configureBinderOptions);

            var apiBuilder = new InternalApiBuilder(builder, options);

            return apiBuilder
                .AddGraphQL();
        }

        public static IApiBuilder AddApi(this IBuilder builder, Func<IBuilder, IApiBuilder> factory)
        {
            var apiBuilder = factory.Invoke(builder);

            return apiBuilder
                .AddGraphQL();
        }

        private static IApiBuilder AddGraphQL(this IApiBuilder builder)
        {
            builder.Services.AddSingleton<IDocumentWriter, DocumentWriter>();
            builder.Services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            builder.Services.AddSingleton<IApiQuery, InternalApiQuery>();
            builder.Services.AddSingleton<IApiSchema, InternalApiSchema>();

            return builder;
        }

    }
}
