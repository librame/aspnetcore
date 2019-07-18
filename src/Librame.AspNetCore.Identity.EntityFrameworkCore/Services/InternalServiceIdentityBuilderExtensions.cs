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

namespace Librame.AspNetCore.Identity
{
    /// <summary>
    /// 内部服务身份构建器静态扩展。
    /// </summary>
    internal static class InternalServiceIdentityBuilderExtensions
    {
        /// <summary>
        /// 添加服务集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddServices(this IIdentityBuilder builder)
        {
            builder.Services.AddScoped<IIdentityIdentifierService, IdentityIdentifierService>();

            //builder.Services.TryReplace<IIdentifierService, IdentityIdentifierService>();
            //builder.Services.AddScoped(serviceProvider =>
            //{
            //    return (IIdentityIdentifierService)serviceProvider.GetRequiredService<IIdentifierService>();
            //});

            return builder;
        }

    }
}
