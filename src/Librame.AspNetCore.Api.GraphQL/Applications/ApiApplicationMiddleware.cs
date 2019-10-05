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
    using Extensions;

    class ApiApplicationMiddleware : AbstractApiApplicationMiddleware
    {
        public ApiApplicationMiddleware(RequestDelegate next)
            : base(next)
        {
        }


        protected override async Task InvokeCore(HttpContext context)
        {
            string body;
            using (var sr = new StreamReader(context.Request.Body))
            {
                body = await sr.ReadToEndAsync().ConfigureAndResultAsync();
            }

            var executor = context.RequestServices.GetRequiredService<IDocumentExecuter>();
            var schema = (Schema)context.RequestServices.GetRequiredService<IGraphApiSchema>();

            var request = JsonConvert.DeserializeObject<ApiRequest>(body);
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
            .ConfigureAndResultAsync();

            var writer = context.RequestServices.GetRequiredService<IDocumentWriter>();
            await writer.WriteAsync(context.Response.Body, result).ConfigureAndWaitAsync();
        }

    }
}
