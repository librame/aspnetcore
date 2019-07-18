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
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 身份构建器静态扩展。
    /// </summary>
    public static class IdentityBuilderExtensions
    {
        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <param name="setupCoreAction">给定的身份核心选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddIdentity<TAccessor>(this IExtensionBuilder builder,
            Action<IdentityBuilderOptions> setupAction = null,
            Action<IdentityOptions> setupCoreAction = null)
            where TAccessor : IdentityDbContextAccessor
        {
            return builder.AddIdentity<TAccessor, DefaultIdentityUser,
                DefaultIdentityRole>(b => new InternalIdentityBuilder(b), setupAction, setupCoreAction);
        }

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建身份构建器的工厂方法。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <param name="setupCoreAction">给定的身份核心选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddIdentity<TAccessor, TUser, TRole>(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IIdentityBuilder> createFactory,
            Action<IdentityBuilderOptions> setupAction = null,
            Action<IdentityOptions> setupCoreAction = null)
            where TAccessor : IdentityDbContextAccessor
            where TUser : class
            where TRole : class
        {
            createFactory.NotNull(nameof(createFactory));

            builder.Services.OnlyConfigure(setupAction);

            // Add IdentityCore
            var identityCore = builder.Services
                .AddIdentityCore<TUser>(setupCoreAction ?? (_ => { }))
                .AddRoles<TRole>()
                .AddEntityFrameworkStores<TAccessor>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            // Add IIdentityBuilder
            var identityBuilder = createFactory.Invoke(builder);

            return identityBuilder
                .AddIdentityCore(identityCore)
                .AddServices();
        }

    }
}
