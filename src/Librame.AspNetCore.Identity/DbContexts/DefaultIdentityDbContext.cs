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
        /// <param name="auditResolver">给定的 <see cref="IAuditResolver"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TDbContext}"/>。</param>
        /// <param name="dbContextOptions">给定的 <see cref="DbContextOptions{TDbContext}"/>。</param>
        public DefaultIdentityDbContext(IAuditResolver auditResolver, IOptions<IdentityBuilderOptions> builderOptions,
            ILogger<DefaultIdentityDbContext> logger, DbContextOptions<DefaultIdentityDbContext> dbContextOptions)
            : base(auditResolver, builderOptions, logger, dbContextOptions)
        {
        }

    }
}
