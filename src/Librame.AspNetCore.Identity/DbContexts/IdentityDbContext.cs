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
    /// 身份数据库上下文接口。
    /// </summary>
    public interface IIdentityDbContext : IIdentityDbContext<IdentityRole, IdentityUser>
    {
    }


    /// <summary>
    /// 身份数据库上下文。
    /// </summary>
    public class IdentityDbContext : AbstractIdentityDbContext<IdentityDbContext, IdentityRole, IdentityUser>, IIdentityDbContext
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityDbContext"/> 实例。
        /// </summary>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="trackerContext">给定的 <see cref="IChangeTrackerContext"/>。</param>
        /// <param name="tenantContext">给定的 <see cref="ITenantContext"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{IdentityDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{IdentityDbContext}"/>。</param>
        public IdentityDbContext(IOptions<IdentityBuilderOptions> builderOptions,
            IChangeTrackerContext trackerContext, ITenantContext tenantContext,
            ILogger<IdentityDbContext> logger, DbContextOptions<IdentityDbContext> dbContextOptions)
            : base(builderOptions, trackerContext, tenantContext, logger, dbContextOptions)
        {
        }

    }
}
