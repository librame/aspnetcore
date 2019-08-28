#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// 身份 <see cref="IUserStore{TUser}"/> 静态扩展。
    /// </summary>
    public static class IdentityUserStoreExtensions
    {
        /// <summary>
        /// 获取用户电邮存储。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <returns>返回 <see cref="IUserEmailStore{TUser}"/>。</returns>
        public static IUserEmailStore<TUser> GetUserEmailStore<TUser>(this IUserStore<TUser> userStore, SignInManager<TUser> signInManager)
            where TUser : class
        {
            return userStore.GetUserEmailStore(signInManager.UserManager);
        }
        /// <summary>
        /// 获取用户电邮存储。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="userManager">给定的 <see cref="UserManager{TUser}"/>。</param>
        /// <returns>返回 <see cref="IUserEmailStore{TUser}"/>。</returns>
        public static IUserEmailStore<TUser> GetUserEmailStore<TUser>(this IUserStore<TUser> userStore, UserManager<TUser> userManager)
            where TUser : class
        {
            userManager.NotNull(nameof(userManager));

            if (!userManager.SupportsUserEmail)
                throw new NotSupportedException("The default UI requires a user store with email support.");

            return (IUserEmailStore<TUser>)userStore;
        }


        /// <summary>
        /// 获取用户电话号码存储。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <returns>返回 <see cref="IUserEmailStore{TUser}"/>。</returns>
        public static IUserPhoneNumberStore<TUser> GetUserPhoneNumberStore<TUser>(this IUserStore<TUser> userStore, SignInManager<TUser> signInManager)
            where TUser : class
        {
            return userStore.GetUserPhoneNumberStore(signInManager.UserManager);
        }
        /// <summary>
        /// 获取用户电话号码存储。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="userManager">给定的 <see cref="UserManager{TUser}"/>。</param>
        /// <returns>返回 <see cref="IUserPhoneNumberStore{TUser}"/>。</returns>
        public static IUserPhoneNumberStore<TUser> GetUserPhoneNumberStore<TUser>(this IUserStore<TUser> userStore, UserManager<TUser> userManager)
            where TUser : class
        {
            userManager.NotNull(nameof(userManager));

            if (!userManager.SupportsUserPhoneNumber)
                throw new NotSupportedException("The default UI requires a user store with phone number support.");

            return (IUserPhoneNumberStore<TUser>)userStore;
        }

    }
}
