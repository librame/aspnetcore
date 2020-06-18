#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Web.Builders;
    using AspNetCore.Web.Services;
    using Extensions;

    /// <summary>
    /// 抽象应用当事人。
    /// </summary>
    public abstract class AbstractApplicationPrincipal : IApplicationPrincipal
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationPrincipal"/>。
        /// </summary>
        /// <param name="webBuilder">给定的 <see cref="IWebBuilder"/>。</param>
        /// <param name="userPortrait">给定的 <see cref="IUserPortraitService"/>。</param>
        protected AbstractApplicationPrincipal(IWebBuilder webBuilder,
            IUserPortraitService userPortrait)
        {
            Builder = webBuilder.NotNull(nameof(webBuilder));
            UserPortrait = userPortrait.NotNull(nameof(userPortrait));
        }


        /// <summary>
        /// Web 构建器。
        /// </summary>
        /// <value>返回 <see cref="IWebBuilder"/>。</value>
        public IWebBuilder Builder { get; }

        /// <summary>
        /// 用户头像。
        /// </summary>
        /// <value>返回 <see cref="IUserPortraitService"/>。</value>
        public IUserPortraitService UserPortrait { get; }


        /// <summary>
        /// 获取登入管理器。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回 <see cref="SignInManager{TUser}"/>。</returns>
        protected abstract dynamic GetSignInManager(HttpContext context);


        private dynamic GetUserManager(HttpContext context)
            => GetSignInManager(context).UserManager;

        private dynamic GetSignedUser(HttpContext context, out dynamic userManager)
        {
            userManager = GetUserManager(context);
            return userManager.GetUserAsync(context.User).Result;
        }


        /// <summary>
        /// 是否已登入。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual bool IsSignedIn(HttpContext context)
            => GetSignInManager(context).IsSignedIn(context.User);


        /// <summary>
        /// 获取已登入用户。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回用户对象。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual dynamic GetSignedUser(HttpContext context)
            => GetSignedUser(context, out _);

        /// <summary>
        /// 获取已登入用户标识。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回对象。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual dynamic GetSignedUserId(HttpContext context)
            => GetUserManager(context).GetUserId(context.User);

        /// <summary>
        /// 获取已登入用户名称。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual string GetSignedUserName(HttpContext context)
            => GetUserManager(context).GetUserName(context.User);

        /// <summary>
        /// 获取已登入用户电邮。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual string GetSignedUserEmail(HttpContext context)
        {
            var user = GetSignedUser(context, out dynamic userManager);
            return userManager.GetEmailAsync(user).Result;
        }

        /// <summary>
        /// 获取已登入用户手机号码。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual string GetSignedUserPhoneNumber(HttpContext context)
        {
            var user = GetSignedUser(context, out dynamic userManager);
            return userManager.GetPhoneNumberAsync(user).Result;
        }

        /// <summary>
        /// 获取已登入用户头像路径。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual string GetSignedUserPortraitPath(HttpContext context)
        {
            var user = GetSignedUser(context, out _);
            return UserPortrait.GetPortraitPathAsync(user).Result;
        }

        /// <summary>
        /// 获取已登入用户角色列表。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回 <see cref="IReadOnlyList{String}"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public virtual IReadOnlyList<string> GetSignedUserRoles(HttpContext context)
        {
            var user = GetSignedUser(context, out dynamic userManager);
            return userManager.GetRolesAsync(user).Result;
        }

    }
}
