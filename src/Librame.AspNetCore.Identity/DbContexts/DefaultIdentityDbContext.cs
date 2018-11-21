#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Data;

    /// <summary>
    /// 默认身份数据库上下文。
    /// </summary>
    public class DefaultIdentityDbContext : AbstractIdentityDbContext<DefaultIdentityDbContext, IdentityRole, IdentityUser>, IDefaultIdentityDbContext
    {
        /// <summary>
        /// 构造一个 <see cref="DefaultIdentityDbContext"/> 实例。
        /// </summary>
        /// <param name="trackerContext">给定的 <see cref="IChangeTrackerContext"/>。</param>
        /// <param name="tenantContext">给定的 <see cref="ITenantContext"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{DefaultIdentityDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{DefaultIdentityDbContext}"/>。</param>
        public DefaultIdentityDbContext(IChangeTrackerContext trackerContext, ITenantContext tenantContext,
            IOptions<IdentityBuilderOptions> builderOptions, ILogger<DefaultIdentityDbContext> logger,
            DbContextOptions<DefaultIdentityDbContext> dbContextOptions)
            : base(trackerContext, tenantContext, builderOptions, logger, dbContextOptions)
        {
        }

    }
}
