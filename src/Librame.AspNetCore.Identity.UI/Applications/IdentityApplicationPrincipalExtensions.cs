#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore;
    using Extensions;

    /// <summary>
    /// 身份应用程序负责人静态扩展。
    /// </summary>
    public static class IdentityApplicationPrincipalExtensions
    {

        /// <summary>
        /// 绑定身份当事人。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="principal">给定的 <see cref="IApplicationPrincipal"/>。</param>
        /// <returns>返回 <see cref="IApplicationPrincipal"/>。</returns>
        public static IApplicationPrincipal BindIdentityPrincipal<TUser>(this IApplicationPrincipal principal)
            where TUser : class
        {
            principal.NotNull(nameof(principal));

            principal.IsSignedIn = context => context.GetSignInManager<TUser>().IsSignedIn(context.User);
            principal.User = context => context.GetSignInManager<TUser>().UserManager.GetUserAsync(context.User).Result;
            principal.UserName = context => context.GetSignInManager<TUser>().UserManager.GetUserName(context.User);

            principal.UserEmail = context =>
            {
                var user = context.GetSignInManager<TUser>().UserManager.GetUserAsync(context.User).Result;
                return context.GetSignInManager<TUser>().UserManager.GetEmailAsync(user as TUser).Result;
            };

            return principal;
        }


        private static SignInManager<TUser> GetSignInManager<TUser>(this HttpContext context)
            where TUser : class
        {
            return (SignInManager<TUser>)context.RequestServices?.GetService(typeof(SignInManager<TUser>));
        }

    }
}
