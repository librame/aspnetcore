#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.Portal
{
    /// <summary>
    /// 存储门户构建器静态扩展。
    /// </summary>
    public static class StorePortalBuilderExtensions
    {
        /// <summary>
        /// 添加存储集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IPortalBuilder"/>。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        public static IPortalBuilder AddStores(this IPortalBuilder builder)
        {
            builder.Services.AddScoped(typeof(IPortalStoreHub<>), typeof(PortalStoreHub<>));

            return builder;
        }

    }
}
