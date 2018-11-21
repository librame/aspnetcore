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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.Builders
{
    using AspNetCore.Identity;
    using Extensions.Data;

    /// <summary>
    /// 身份构建器静态扩展。
    /// </summary>
    public static class IdentityBuilderExtensions
    {
        
        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IdentityBuilderOptions"/>（可选）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选）。</param>
        /// <param name="postConfigureOptions">给定的 <see cref="Action{IdentityBuilderOptions}"/>（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddIdentity(this IBuilder builder, IdentityBuilderOptions builderOptions = null,
            IConfiguration configuration = null, Action<IdentityBuilderOptions> postConfigureOptions = null)
        {
            return builder.AddIdentity<IdentityBuilderOptions>(builderOptions ?? new IdentityBuilderOptions(),
                configuration, postConfigureOptions);
        }
        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="builderOptions">给定的构建器选项。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选）。</param>
        /// <param name="postConfigureOptions">给定的 <see cref="Action{TBuilderOptions}"/>（可选）。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddIdentity<TBuilderOptions>(this IBuilder builder, TBuilderOptions builderOptions,
            IConfiguration configuration = null, Action<TBuilderOptions> postConfigureOptions = null)
            where TBuilderOptions : IdentityBuilderOptions
        {
            var dataBuilder = builder.AddData(builderOptions, configuration, postConfigureOptions).ResetData();
            
            return dataBuilder.AddBuilder(b =>
            {
                return b.AsIdentityBuilder();
            },
            typeof(IdentityBuilderOptions), builderOptions, configuration, postConfigureOptions);
        }

        /// <summary>
        /// 重置数据扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDataBuilder"/>。</param>
        /// <returns>返回 <see cref="IDataBuilder"/>。</returns>
        public static IDataBuilder ResetData(this IDataBuilder builder)
        {
            builder.Services.Replace(ServiceDescriptor.Singleton<ITenantContext, HttpTenantContext>());

            return builder;
        }


        /// <summary>
        /// 转换为内部身份构建器。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDataBuilder"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AsIdentityBuilder(this IDataBuilder builder)
        {
            return new InternalIdentityBuilder(builder);
        }

        /// <summary>
        /// 添加核心与角色。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{IdentityOptions}"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddCore<TUser, TRole, TDbContext>(this IIdentityBuilder builder, Action<IdentityOptions> configureOptions = null)
            where TUser : class
            where TRole : class
            where TDbContext : DbContext
        {
            builder.RegisterCore<TUser>(configureOptions);
            
            builder.Core.AddRoles<TRole>()
                .AddEntityFrameworkStores<TDbContext>();

            return builder;
        }

        /// <summary>
        /// 使用用户界面。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="configureAction">给定的 <see cref="Action{IIdentityBuilder}"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder WithUI(this IIdentityBuilder builder, Action<IIdentityBuilder> configureAction)
        {
            builder.ConfigureCore(identity =>
            {
                identity.AddSignInManager();
            });

            configureAction.Invoke(builder);

            builder.ConfigureCore(identity =>
            {
                identity.AddDefaultTokenProviders();
            });

            return builder;
        }

    }
}
