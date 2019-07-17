#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore;
using Librame.Extensions.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ASP.NET 构建器服务集合静态扩展。
    /// </summary>
    public static class AspNetBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Librame for ASP.NET Core 服务集合。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{CoreBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<CoreBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
        {
            var builder = services.AddLibrame(configureOptions,
                configuration, configureBinderOptions);

            return builder
                .AddAspNetCoreLocalizations()
                .AddPartApplications()
                .AddHttpContextAccessor();
        }


        private static ICoreBuilder AddHttpContextAccessor(this ICoreBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return builder;
        }

    }
}
