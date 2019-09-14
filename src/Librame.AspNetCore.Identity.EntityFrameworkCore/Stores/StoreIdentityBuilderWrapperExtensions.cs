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

namespace Librame.AspNetCore.Identity
{
    static class StoreIdentityBuilderWrapperExtensions
    {
        /// <summary>
        /// 增加存储。
        /// </summary>
        /// <param name="builderWrapper">给定的 <see cref="IIdentityBuilderWrapper"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilderWrapper"/>。</returns>
        public static IIdentityBuilderWrapper AddStores(this IIdentityBuilderWrapper builderWrapper)
        {
            var userStoreServiceType = typeof(IUserStore<>).MakeGenericType(builderWrapper.RawBuilder.UserType);
            var userStoreType = typeof(DefaultUserStore);

            if (!builderWrapper.Services.TryReplace(userStoreServiceType, userStoreType))
                builderWrapper.Services.AddScoped(userStoreServiceType, userStoreType);

            var roleStoreServiceType = typeof(IRoleStore<>).MakeGenericType(builderWrapper.RawBuilder.RoleType);
            var roleStoreType = typeof(DefaultRoleStore);

            if (!builderWrapper.Services.TryReplace(roleStoreServiceType, roleStoreType))
                builderWrapper.Services.AddScoped(roleStoreServiceType, roleStoreType);

            return builderWrapper;
        }

    }
}
