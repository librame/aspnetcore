#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Data
{
    using Core;

    class HttpTenantService : ExtensionBuilderServiceBase<DataBuilderOptions>, ITenantService
    {
        private readonly HttpContext _httpContext;


        public HttpTenantService(IHttpContextAccessor httpContextAccessor,
            IOptions<DataBuilderOptions> options, ILoggerFactory logger)
            : base(options, logger)
        {
            _httpContext = httpContextAccessor.NotNull(nameof(httpContextAccessor)).HttpContext;
        }


        public Task<ITenant> GetTenantAsync<TTenant>(IQueryable<TTenant> queryable,
            CancellationToken cancellationToken = default)
            where TTenant : ITenant
        {
            cancellationToken.ThrowIfCancellationRequested();

            ITenant tenant = null;

            var host = _httpContext?.Request?.Host;
            if (host.HasValue && !host.Value.IsIPAddress())
            {
                var twoLevels = host.Value.Host.AsDomainNameLocator().GetOnlyTwoLevels();
                var name = twoLevels.Child ?? "www";

                tenant = queryable.FirstOrDefault(t => t.Name == name && t.Host == twoLevels.Parent);
                Logger.LogInformation($"Queryable database Tenant: {tenant?.Name}.{tenant?.Host}");
            }
            
            return Task.FromResult(tenant ?? Options.DefaultTenant);
        }

    }
}
