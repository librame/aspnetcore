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
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.Extensions.Data
{
    /// <summary>
    /// 内部 HTTP 租户服务。
    /// </summary>
    internal class InternalHttpTenantService : DataServiceBase, ITenantService
    {
        private readonly IHttpContextAccessor _accessor;


        /// <summary>
        /// 构造一个 <see cref="InternalHttpTenantService"/> 实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IHttpContextAccessor"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{DataBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILoggerFactory"/>。</param>
        public InternalHttpTenantService(IHttpContextAccessor accessor,
            IOptions<DataBuilderOptions> options, ILoggerFactory logger)
            : base(options, logger)
        {
            _accessor = accessor.NotNull(nameof(accessor));
        }


        /// <summary>
        /// 获取租户。
        /// </summary>
        /// <typeparam name="TTenant">指定的租户类型。</typeparam>
        /// <param name="queryable">给定的 <see cref="IQueryable{TTenant}"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="ITenant"/> 的异步操作。</returns>
        public Task<ITenant> GetTenantAsync<TTenant>(IQueryable<TTenant> queryable,
            CancellationToken cancellationToken = default)
            where TTenant : ITenant
        {
            cancellationToken.ThrowIfCancellationRequested();

            ITenant tenant = null;

            var request = _accessor.HttpContext?.Request;
            if (request.IsNotNull() && request.Query.TryGetValue("name", out StringValues value))
            {
                var name = (string)value;
                var host = request.Host.Value;

                tenant = queryable.FirstOrDefault(t => t.Name == name && t.Host == host);
                Logger.LogInformation($"Queryable database Tenant: {tenant?.Name}.{tenant?.Host}");
            }
            
            return Task.FromResult(tenant ?? Options.DefaultTenant);
        }

    }
}
