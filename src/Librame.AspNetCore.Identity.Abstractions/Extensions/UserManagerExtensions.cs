#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity
{
    using Extensions;
    using Extensions.Core.Services;
    using Extensions.Data.Stores;

    /// <summary>
    /// <see cref="UserManager{TUser}"/> 静态扩展。
    /// </summary>
    public static class UserManagerExtensions
    {
        /// <summary>
        /// 通过电邮创建用户。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="userManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="email">给定的电邮。</param>
        /// <param name="password">给定的密码（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IdentityResult"/> 的异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递")]
        public static async Task<IdentityResult> CreateUserByEmail<TUser, TCreatedBy>(this UserManager<TUser> userManager,
            IUserStore<TUser> userStore, IClockService clock, TUser user, string email,
            string password = null, CancellationToken cancellationToken = default)
            where TUser : class, ICreation<TCreatedBy>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            userManager.NotNull(nameof(userManager));
            userStore.NotNull(nameof(userStore));

            await userStore.SetUserNameAsync(user, email, cancellationToken).ConfigureAwait();

            if (!userManager.SupportsUserEmail)
                throw new NotSupportedException("The identity builder requires a user store with email support.");

            var emailStore = (IUserEmailStore<TUser>)userStore;
            await emailStore.SetEmailAsync(user, email, cancellationToken).ConfigureAwait();

            await user.PopulateCreationAsync(clock).ConfigureAwait();

            if (password.IsNotEmpty())
                return await userManager.CreateAsync(user, password).ConfigureAwait();

            return await userManager.CreateAsync(user).ConfigureAwait();
        }

    }
}
