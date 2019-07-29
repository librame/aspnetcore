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
using Microsoft.EntityFrameworkCore;
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
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="setupAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="rawBuilderAction">给定的原始构建器选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderWrapper"/>。</returns>
        public static IIdentityBuilderWrapper AddIdentity<TAccessor>(this IExtensionBuilder builder,
            Action<IdentityBuilderOptions> setupAction = null,
            Action<IdentityOptions> rawBuilderAction = null)
            where TAccessor : IdentityDbContextAccessor
        {
            return builder.AddIdentity<TAccessor, DefaultIdentityUser, DefaultIdentityRole, string>((b, r) =>
            {
                return new InternalIdentityBuilderWrapper(b, r);
            },
            setupAction, rawBuilderAction);
        }

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建身份构建器的工厂方法。</param>
        /// <param name="setupAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="rawBuilderAction">给定的原始构建器选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilderWrapper"/>。</returns>
        public static IIdentityBuilderWrapper AddIdentity<TAccessor, TUser, TRole, TGenId>(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IdentityBuilder, IIdentityBuilderWrapper> createFactory,
            Action<IdentityBuilderOptions> setupAction = null,
            Action<IdentityOptions> rawBuilderAction = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor<TRole, TUser, TGenId>
            where TUser : class
            where TRole : class
            where TGenId : IEquatable<TGenId>
        {
            createFactory.NotNull(nameof(createFactory));

            builder.Services.OnlyConfigure(setupAction);

            // Add IdentityBuilder
            var rawBuilder = builder.Services
                .AddIdentityCore<TUser>(rawBuilderAction ?? (_ => { }))
                .AddRoles<TRole>()
                .AddEntityFrameworkStores<TAccessor>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            // Add IIdentityBuilderWrapper
            var builderWrapper = createFactory.Invoke(builder, rawBuilder);

            return builderWrapper
                .AddServices();
        }

    }
}
