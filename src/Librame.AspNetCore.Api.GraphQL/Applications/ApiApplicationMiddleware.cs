#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Api.Applications
{
    using Extensions;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ApiApplicationMiddleware : AbstractApiApplicationMiddleware
    {
        private readonly IDocumentWriter _writer;


        public ApiApplicationMiddleware(IDocumentWriter writer,
            RequestDelegate next)
            : base(next)
        {
            _writer = writer.NotNull(nameof(writer));
        }


        public override PathString RestrictRequestPath
            => $"{ApiSettings.Preference.RestrictRequestPath}/graphql";


        protected override async Task InvokeCore(HttpContext context)
        {
            // Transient Service
            var request = context.RequestServices.GetRequiredService<IApiRequest>();

            await request.PopulateAsync(context)
                .ConfigureAwait();

            var result = await request.ExecuteAsync(context: context)
                .ConfigureAwait();

            await WriteResponseAsync(context, result).ConfigureAwait();
        }

        private async Task WriteResponseAsync(HttpContext context, ApiRequestResult result)
        {
            context.Response.ContentType = ResponseContentType;
            context.Response.StatusCode = result.Succeeded
                ? (int)HttpStatusCode.OK
                : (int)HttpStatusCode.BadRequest;
            
            await _writer.WriteAsync(context.Response.Body, result)
                .ConfigureAwait();
        }

    }
}
