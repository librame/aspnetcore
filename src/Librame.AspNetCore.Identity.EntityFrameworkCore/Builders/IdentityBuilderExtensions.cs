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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;

    /// <summary>
    /// 身份构建器静态扩展。
    /// </summary>
    public static class IdentityBuilderExtensions
    {
        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{IdentityBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddIdentity<TAccessor>(this IBuilder builder,
            Action<IdentityBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
            where TAccessor : IdentityDbContextAccessor
        {
            return builder.AddIdentity<TAccessor, DefaultIdentityUser,
                DefaultIdentityRole>(configureOptions, configuration, configureBinderOptions);
        }

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{IdentityBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddIdentity<TAccessor, TUser, TRole>(this IBuilder builder,
            Action<IdentityBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
            where TAccessor : IdentityDbContextAccessor
            where TUser : class
            where TRole : class
        {
            var options = builder.Configure(configureOptions,
                configuration, configureBinderOptions);

            var identityBuilder = new InternalIdentityBuilder(builder, options);

            identityBuilder.AddIdentityCore<TUser>(core =>
            {
                core.AddRoles<TRole>()
                    .AddEntityFrameworkStores<TAccessor>();
            });

            identityBuilder.CoreIdentityBuilder.AddSignInManager();

            options.ConfigureUIMode?.Invoke(identityBuilder);

            identityBuilder.CoreIdentityBuilder.AddDefaultTokenProviders();

            return identityBuilder
                .AddServices();
        }

    }
}
