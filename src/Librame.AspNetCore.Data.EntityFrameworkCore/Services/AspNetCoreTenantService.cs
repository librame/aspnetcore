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
    internal class AspNetCoreTenantService<TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId>
        : TenantService<TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId>
        where TAudit : DataAudit<TGenId>
        where TAuditProperty : DataAuditProperty<TIncremId, TGenId>
        where TEntity : DataEntity<TGenId>
        where TMigration : DataMigration<TGenId>
        where TTenant : DataTenant<TGenId>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
    {
        private readonly HttpContext _httpContext;


        public AspNetCoreTenantService(IHttpContextAccessor httpContextAccessor,
            IOptions<DataBuilderOptions> options, ILoggerFactory logger)
            : base(options, logger)
        {
            _httpContext = httpContextAccessor.NotNull(nameof(httpContextAccessor)).HttpContext;
        }


        protected override ITenant GetSwitchTenantCore(DbContextAccessor<TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId> dbContextAccessor)
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

            return base.GetSwitchTenantCore(dbContextAccessor);
        }

        protected override Task<ITenant> GetSwitchTenantCoreAsync(DbContextAccessor<TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId> dbContextAccessor,
            CancellationToken cancellationToken = default)
        {
            try
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
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.AsInnerMessage());
            }

            return base.GetSwitchTenantCoreAsync(dbContextAccessor, cancellationToken);
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
