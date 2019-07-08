#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 门户构建器静态扩展。
    /// </summary>
    public static class PortalBuilderExtensions
    {
        /// <summary>
        /// 添加门户扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{PortalBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        public static IPortalBuilder AddPortal<TAccessor>(this IBuilder builder,
            Action<PortalBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
            where TAccessor : DbContext, IAccessor
        {
            var options = builder.Configure(configureOptions,
                configuration, configureBinderOptions);

            var portalBuilder = new InternalPortalBuilder(builder, options);

            return portalBuilder
                .AddServices()
                .AddStores();
        }

    }
}
