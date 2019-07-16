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
    /// 服务门户构建器静态扩展。
    /// </summary>
    public static class ServicePortalBuilderExtensions
    {
        /// <summary>
        /// 添加服务集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IPortalBuilder"/>。</param>
        /// <returns>返回 <see cref="IPortalBuilder"/>。</returns>
        public static IPortalBuilder AddServices(this IPortalBuilder builder)
        {
            builder.Services.AddScoped<IPortalIdentifierService, PortalIdentifierService>();

            //builder.Services.TryReplace<IIdentifierService, PortalIdentifierService>();
            //builder.Services.AddScoped(serviceProvider =>
            //{
            //    return (IPortalIdentifierService)serviceProvider.GetRequiredService<IIdentifierService>();
            //});

            return builder;
        }

    }
}
