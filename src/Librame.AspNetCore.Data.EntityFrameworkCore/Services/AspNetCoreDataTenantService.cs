#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Data
{
    using Core;

    class AspNetCoreDataTenantService : DataTenantService, IDataTenantService
    {
        private readonly HttpContext _httpContext;


        public AspNetCoreDataTenantService(IHttpContextAccessor httpContextAccessor,
            IOptions<DataBuilderOptions> options, ILoggerFactory logger)
            : base(options, logger)
        {
            _httpContext = httpContextAccessor.NotNull(nameof(httpContextAccessor)).HttpContext;
        }


        protected override ITenant GetSwitchTenantCore(DbContextAccessor dbContextAccessor)
        {
            var options = _httpContext?.RequestServices?.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
            if (options.IsNotNull() && !RequestCultureEquals(options.DefaultRequestCulture) && dbContextAccessor.Tenants.Count() > 1)
            {
                var name = options.DefaultRequestCulture.Culture.Name;
                var host = options.DefaultRequestCulture.Culture.DisplayName;

                ITenant tenant = dbContextAccessor.Tenants.FirstOrDefault(t => t.Name == name && t.Host == host);
                Logger.LogInformation($"Get database tenant: Name={tenant?.Name}, Host={tenant?.Host}");

                return tenant ?? Options.DefaultTenant;
            }

            return Options.DefaultTenant;
        }

        protected override Task<ITenant> GetSwitchTenantCoreAsync(DbContextAccessor dbContextAccessor,
            CancellationToken cancellationToken = default)
        {
            var options = _httpContext?.RequestServices?.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
            if (options.IsNotNull() && !RequestCultureEquals(options.DefaultRequestCulture) && dbContextAccessor.Tenants.Count() > 1)
            {
                var name = options.DefaultRequestCulture.Culture.Name;
                var host = options.DefaultRequestCulture.Culture.DisplayName;

                return cancellationToken.RunFactoryOrCancellationAsync(() =>
                {
                    ITenant tenant = dbContextAccessor.Tenants.FirstOrDefault(t => t.Name == name && t.Host == host);
                    Logger.LogInformation($"Get database tenant: Name={tenant?.Name}, Host={tenant?.Host}");

                    return tenant ?? Options.DefaultTenant;
                });
            }

            return Task.FromResult(Options.DefaultTenant);
        }


        private static bool RequestCultureEquals(RequestCulture requestCulture)
        {
            if (requestCulture.IsNull() || requestCulture.Culture.IsNull() || requestCulture.UICulture.IsNull())
                return false;

            if (CultureInfo.CurrentCulture.IsNull() || CultureInfo.CurrentUICulture.IsNull())
                return false;

            return CultureInfo.CurrentCulture.Name.Equals(requestCulture.Culture.Name, StringComparison.OrdinalIgnoreCase)
                && CultureInfo.CurrentUICulture.Name.Equals(requestCulture.UICulture.Name, StringComparison.OrdinalIgnoreCase);
        }

    }
}
