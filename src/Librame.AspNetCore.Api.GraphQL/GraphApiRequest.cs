#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL;
using GraphQL.Instrumentation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Api
{
    using AspNetCore.Api.Builders;
    using Extensions;

    /// <summary>
    /// 图形 API 请求。
    /// </summary>
    public class GraphApiRequest : AbstractApiRequest
    {
        /// <summary>
        /// 构造一个 <see cref="GraphApiRequest"/>。
        /// </summary>
        /// <param name="schema">给定的 <see cref="IGraphApiSchema"/>。</param>
        /// <param name="executer">给定的 <see cref="IDocumentExecuter"/>。</param>
        /// <param name="dependency">给定的 <see cref="ApiBuilderDependency"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GraphApiRequest(IGraphApiSchema schema,
            IDocumentExecuter executer,
            ApiBuilderDependency dependency,
            ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            Schema = schema.NotNull(nameof(schema));
            Executer = executer.NotNull(nameof(executer));
            Dependency = dependency.NotNull(nameof(dependency));
        }


        /// <summary>
        /// API 架构。
        /// </summary>
        protected IGraphApiSchema Schema { get; }

        /// <summary>
        /// 文档执行器。
        /// </summary>
        protected IDocumentExecuter Executer { get; }

        /// <summary>
        /// 构建器依赖。
        /// </summary>
        protected ApiBuilderDependency Dependency { get; }


        /// <summary>
        /// 配置执行选项。
        /// </summary>
        /// <param name="options">给定的 <see cref="ExecutionOptions"/>。</param>
        /// <param name="query">给定的查询字符串（可选；默认为当前查询内容）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void ConfigureOptions(ExecutionOptions options,
            string query = null, HttpContext context = null)
        {
            options.NotNull(nameof(options));

            options.Query = query ?? Query;
            options.OperationName = OperationName;
            options.Inputs = Variables.ToInputs();

            options.UserContext = Dependency.BuildUserContext?.Invoke(context);
            options.EnableMetrics = Dependency.EnableMetrics;

            if (Dependency.EnableMetrics)
                options.FieldMiddleware.Use<InstrumentFieldsMiddleware>();

            options.ComplexityConfiguration = Dependency.Complexity;
        }


        /// <summary>
        /// 执行请求的 JSON 结果。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="IApiRequest.Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回字符串。</returns>
        public override string ExecuteJson(string query = null, HttpContext context = null)
        {
            return Schema.Execute(options =>
            {
                ConfigureOptions(options, query, context);
            });
        }

        /// <summary>
        /// 异步执行请求的 JSON 结果。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="IApiRequest.Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ApiRequestResult"/> 的异步操作。</returns>
        public override Task<string> ExecuteJsonAsync(string query = null, HttpContext context = null)
        {
            return Schema.ExecuteAsync(options =>
            {
                ConfigureOptions(options, query, context);
            });
        }


        /// <summary>
        /// 异步执行请求。
        /// </summary>
        /// <param name="query">给定的查询内容（可选；默认为 <see cref="IApiRequest.Query"/>）。</param>
        /// <param name="context">给定的 <see cref="HttpContext"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ApiRequestResult"/> 的异步操作。</returns>
        public override async Task<ApiRequestResult> ExecuteAsync(string query = null,
            HttpContext context = null)
        {
            var result = await Executer.ExecuteAsync(options =>
            {
                options.Schema = Schema;

                ConfigureOptions(options, query, context);
            })
            .ConfigureAwait();

            if (result.Errors.IsEmpty())
                return ApiRequestResult.Success(result.Data);

            return ApiRequestResult.Failed(result.Data, result.Errors);
        }

    }
}
