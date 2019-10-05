#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.IdentityServer
{
    using Identity;
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 身份服务器存储标识符。
    /// </summary>
    public class IdentityServerStoreInitializer : IdentityStoreInitializer<IdentityServerDbContextAccessor, IdentityServerStoreIdentifier>
    {
        //private readonly string _defaultCreatedBy
        //    = nameof(IdentityServerStoreInitializer);


        /// <summary>
        /// 构造一个 <see cref="IdentityServerStoreInitializer"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="identifier">给定的 <see cref="IStoreIdentifier"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityServerStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
            RoleManager<DefaultIdentityRole<string>> roleMananger,
            IUserStore<DefaultIdentityUser<string>> userStore,
            IOptions<IdentityBuilderOptions> options,
            IClockService clock, IStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, options, clock, identifier, loggerFactory)
        {
        }


        /// <summary>
        /// 初始化核心。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub{IdentityServerDbContextAccessor, TAudit, TEntity, TMigration, TTenant}"/>。</param>
        protected override void InitializeCore<TAudit, TEntity, TMigration, TTenant>(IStoreHub<IdentityServerDbContextAccessor, TAudit, TEntity, TMigration, TTenant> stores)
        {
            base.InitializeCore(stores);
        }

    }
}
