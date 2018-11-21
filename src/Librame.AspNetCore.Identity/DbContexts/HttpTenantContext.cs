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
using System.Linq;

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// HTTP 租户上下文。
    /// </summary>
    public class HttpTenantContext : ITenantContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// 构造一个 <see cref="HttpTenantContext"/> 实例。
        /// </summary>
        /// <param name="httpContextAccessor">给定的 <see cref="IHttpContextAccessor"/>。</param>
        public HttpTenantContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor.NotDefault(nameof(httpContextAccessor));
        }


        /// <summary>
        /// 获取租户。
        /// </summary>
        /// <param name="tenants">给定的 <see cref="IQueryable{Tenant}"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="DataBuilderOptions"/>。</param>
        /// <returns>返回 <see cref="Tenant"/>。</returns>
        public Tenant GetTenant(IQueryable<Tenant> tenants, DataBuilderOptions builderOptions)
        {
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var tenant = tenants.FirstOrDefault(t => t.Host == host);

            return tenant ?? builderOptions.LocalTenant;
        }

    }
}
