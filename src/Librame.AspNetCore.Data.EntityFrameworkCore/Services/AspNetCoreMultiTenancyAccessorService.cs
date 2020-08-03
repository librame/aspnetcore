#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Data.Services
{
    using Accessors;
    using Builders;
    using Stores;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class AspNetCoreMultiTenancyAccessorService<TAudit, TAuditProperty, TMigration, TTabulation, TTenant, TGenId, TIncremId, TCreatedBy>
        : MultiTenancyAccessorService<TAudit, TAuditProperty, TMigration, TTabulation, TTenant, TGenId, TIncremId, TCreatedBy>
        where TAudit : DataAudit<TGenId, TCreatedBy>
        where TAuditProperty : DataAuditProperty<TIncremId, TGenId>
        where TMigration : DataMigration<TGenId, TCreatedBy>
        where TTabulation : DataTabulation<TGenId, TCreatedBy>
        where TTenant : DataTenant<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        private readonly HttpContext _httpContext;


        public AspNetCoreMultiTenancyAccessorService(IHttpContextAccessor httpContextAccessor,
            IOptions<DataBuilderOptions> options, ILoggerFactory logger)
            : base(options, logger)
        {
            _httpContext = httpContextAccessor.NotNull(nameof(httpContextAccessor)).HttpContext;
        }


        [SuppressMessage("Design", "CA1031:不捕获常规异常类型")]
        protected override ITenant GetCurrentTenantCore
            (DataDbContextAccessor<TAudit, TAuditProperty, TMigration, TTabulation, TTenant, TGenId, TIncremId, TCreatedBy> dbContextAccessor)
        {
            try
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
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.AsInnerMessage());
            }

            return base.GetCurrentTenantCore(dbContextAccessor);
        }

        [SuppressMessage("Design", "CA1031:不捕获常规异常类型")]
        protected override Task<ITenant> GetCurrentTenantCoreAsync
            (DataDbContextAccessor<TAudit, TAuditProperty, TMigration, TTabulation, TTenant, TGenId, TIncremId, TCreatedBy> dbContextAccessor,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var options = _httpContext?.RequestServices?.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
                if (options.IsNotNull() && !RequestCultureEquals(options.DefaultRequestCulture) && dbContextAccessor.Tenants.Count() > 1)
                {
                    var name = options.DefaultRequestCulture.Culture.Name;
                    var host = options.DefaultRequestCulture.Culture.DisplayName;

                    return cancellationToken.RunOrCancelAsync(() =>
                    {
                        ITenant tenant = dbContextAccessor.Tenants.FirstOrDefault(t => t.Name == name && t.Host == host);
                        Logger.LogInformation($"Get database tenant: Name={tenant?.Name}, Host={tenant?.Host}");

                        return tenant ?? Options.DefaultTenant;
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.AsInnerMessage());
            }

            return base.GetCurrentTenantCoreAsync(dbContextAccessor, cancellationToken);
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
