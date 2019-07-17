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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Api
{
    using Extensions;

    /// <summary>
    /// 内部 API 中间件。
    /// </summary>
    internal class InternalApiMiddleware : AbstractApiMiddleware
    {
        private readonly Schema _schema;
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executor;


        /// <summary>
        /// 构造一个 <see cref="InternalApiMiddleware"/> 中间件。
        /// </summary>
        /// <param name="next">给定的 <see cref="RequestDelegate"/>。</param>
        /// <param name="schema">给定的 <see cref="IApiSchema"/>。</param>
        /// <param name="writer">给定的 <see cref="IDocumentWriter"/>。</param>
        /// <param name="executor">给定的 <see cref="IDocumentExecuter"/>。</param>
        public InternalApiMiddleware(RequestDelegate next, IApiSchema schema,
            IDocumentWriter writer, IDocumentExecuter executor)
            : base(next)
        {
            _schema = schema.CastTo<IApiSchema, Schema>(nameof(schema));
            _writer = writer.NotNull(nameof(writer));
            _executor = executor.NotNull(nameof(executor));
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

            var request = JsonConvert.DeserializeObject<InternalApiRequest>(body);
            var result = await _executor.ExecuteAsync(doc =>
            {
                doc.Schema = _schema;
                doc.Query = request.Query;
            })
            .ConfigureAwait(false);

            await _writer.WriteAsync(context.Response.Body, result);
        }

    }
}
