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
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Api.Utilities
{
    using Extensions;

    /// <summary>
    /// <see cref="SignInManager{TUser}"/> 实用工具。
    /// </summary>
    public static class SignInManagerUtility
    {
        /// <summary>
        /// 通过电话号码创建用户。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="userManager">给定的 <see cref="UserManager{TUser}"/>。</param>
        /// <param name="phoneNumber">给定的电话号码。</param>
        /// <param name="password">给定的密码（可选）。</param>
        /// <param name="user">给定的 <typeparamref name="TUser"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IdentityResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static async Task<IdentityResult> CreateUserByPhoneNumber<TUser>(IUserStore<TUser> userStore,
            UserManager<TUser> userManager, string phoneNumber, string password = null, TUser user = null)
            where TUser : class
        {
            userStore.NotNull(nameof(userStore));
            userManager.NotNull(nameof(userManager));

            await CreateUser(userStore, phoneNumber, user).ConfigureAndWaitAsync();

            if (!userManager.SupportsUserPhoneNumber)
                throw new NotSupportedException("The identity builder requires a user store with phone number support.");

            var phoneNumberStore = (IUserPhoneNumberStore<TUser>)userStore;
            await phoneNumberStore.SetPhoneNumberAsync(user, phoneNumber, CancellationToken.None).ConfigureAndWaitAsync();

            if (password.IsNotEmpty())
                return await userManager.CreateAsync(user, password).ConfigureAndResultAsync();

            return await userManager.CreateAsync(user).ConfigureAndResultAsync();
        }

        /// <summary>
        /// 通过电邮创建用户。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="userManager">给定的 <see cref="UserManager{TUser}"/>。</param>
        /// <param name="email">给定的电邮。</param>
        /// <param name="password">给定的密码（可选）。</param>
        /// <param name="user">给定的 <typeparamref name="TUser"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IdentityResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static async Task<IdentityResult> CreateUserByEmail<TUser>(UserManager<TUser> userManager,
            IUserStore<TUser> userStore, string email, string password = null, TUser user = null)
            where TUser : class
        {
            userStore.NotNull(nameof(userStore));
            userManager.NotNull(nameof(userManager));

            await CreateUser(userStore, email, user).ConfigureAndWaitAsync();

            if (!userManager.SupportsUserEmail)
                throw new NotSupportedException("The identity builder requires a user store with email support.");

            var emailStore = (IUserEmailStore<TUser>)userStore;
            await emailStore.SetEmailAsync(user, email, CancellationToken.None).ConfigureAndWaitAsync();

            if (password.IsNotEmpty())
                return await userManager.CreateAsync(user, password).ConfigureAndResultAsync();

            return await userManager.CreateAsync(user).ConfigureAndResultAsync();
        }

        private static async Task CreateUser<TUser>(IUserStore<TUser> userStore, string userName, TUser user = null)
            where TUser : class
        {
            if (user.IsNull())
                user = typeof(TUser).EnsureCreate<TUser>();

            await userStore.SetUserNameAsync(user, userName, CancellationToken.None).ConfigureAndWaitAsync();
        }

    }
}
