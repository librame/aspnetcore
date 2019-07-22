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
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Api
{
    /// <summary>
    /// 内部 API 中间件。
    /// </summary>
    internal class InternalApiMiddleware : AbstractApiMiddleware
    {
        /// <summary>
        /// 构造一个 <see cref="InternalApiMiddleware"/> 中间件。
        /// </summary>
        /// <param name="next">给定的 <see cref="RequestDelegate"/>。</param>
        public InternalApiMiddleware(RequestDelegate next)
            : base(next)
        {
        }


        /// <summary>
        /// 调用中间件核心。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected override async Task InvokeCore(HttpContext context)
        {
            string body;
            using (var sr = new StreamReader(context.Request.Body))
            {
                body = await sr.ReadToEndAsync();
            }

            var executor = context.RequestServices.GetRequiredService<IDocumentExecuter>();
            var schema = (Schema)context.RequestServices.GetRequiredService<IGraphApiSchema>();

            var request = JsonConvert.DeserializeObject<InternalApiRequest>(body);
            var result = await executor.ExecuteAsync(options =>
            {
                options.Schema = schema;
                options.Query = request.Query;
                options.OperationName = request.OperationName;
                options.Inputs = request.Variables.ToInputs();

                options.ComplexityConfiguration = new ComplexityConfiguration
                {
                    MaxDepth = 15
                };
            })
            .ConfigureAwait(false);

            var writer = context.RequestServices.GetRequiredService<IDocumentWriter>();
            await writer.WriteAsync(context.Response.Body, result);
        }

    }
}
