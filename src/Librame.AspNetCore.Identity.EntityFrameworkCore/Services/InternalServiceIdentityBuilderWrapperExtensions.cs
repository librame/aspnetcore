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
    /// 内部服务 <see cref="IIdentityBuilderWrapper"/> 静态扩展。
    /// </summary>
    internal static class InternalServiceIdentityBuilderWrapperExtensions
    {
        /// <summary>
        /// 添加服务集合。
        /// </summary>
        /// <param name="builderWrapper">给定的 <see cref="IIdentityBuilderWrapper"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilderWrapper"/>。</returns>
        public static IIdentityBuilderWrapper AddServices(this IIdentityBuilderWrapper builderWrapper)
        {
            builderWrapper.Services.AddScoped<IIdentityIdentifierService, IdentityIdentifierService>();

            //builderWrapper.Services.TryReplace<IIdentifierService, IdentityIdentifierService>();
            //builderWrapper.Services.AddScoped(serviceProvider =>
            //{
            //    return (IIdentityIdentifierService)serviceProvider.GetRequiredService<IIdentifierService>();
            //});

            return builderWrapper;
        }

    }
}
