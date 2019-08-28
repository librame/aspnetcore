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
using System;

namespace Librame.Extensions.Core
{
    using AspNetCore.UI;

    /// <summary>
    /// 用户界面服务工厂委托静态扩展。
    /// </summary>
    public static class UiServiceFactoryDelegateExtensions
    {
        /// <summary>
        /// 获取登入管理器。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        /// <returns>返回 <see cref="SignInManager{TUser}"/>。</returns>
        public static dynamic GetSignInMananger(this ServiceFactoryDelegate serviceFactory)
        {
            return serviceFactory.GetSignInMananger(out _);
        }

        /// <summary>
        /// 获取登入管理器。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        /// <param name="builder">输出 <see cref="IUiBuilder"/>。</param>
        /// <returns>返回 <see cref="SignInManager{TUser}"/>。</returns>
        public static dynamic GetSignInMananger(this ServiceFactoryDelegate serviceFactory, out IUiBuilder builder)
        {
            builder = serviceFactory.GetRequiredService<IUiBuilder>();

            return serviceFactory.GetSignInMananger(builder.UserType);
        }

        /// <summary>
        /// 获取登入管理器。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        /// <param name="userType">给定的用户类型。</param>
        /// <returns>返回 <see cref="SignInManager{TUser}"/>。</returns>
        public static dynamic GetSignInMananger(this ServiceFactoryDelegate serviceFactory, Type userType)
        {
            return serviceFactory.GetRequiredService(typeof(SignInManager<>).MakeGenericType(userType));
        }

    }
}
