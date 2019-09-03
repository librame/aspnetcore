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
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    class ApplicationPrincipal : IApplicationPrincipal
    {
        private readonly IUiBuilder _builder;


        public ApplicationPrincipal(IUiBuilder builder)
        {
            _builder = builder;
        }


        private dynamic GetSignInManager(HttpContext context)
        {
            var managerType = typeof(SignInManager<>).MakeGenericType(_builder.UserType);

            return context?.RequestServices?.GetRequiredService(managerType);
        }

        private dynamic GetUserManager(HttpContext context)
        {
            return GetSignInManager(context).UserManager;
        }


        public bool IsSignedIn(HttpContext context)
        {
            return GetSignInManager(context).IsSignedIn(context.User);
        }

        public dynamic GetSignedUser(HttpContext context)
        {
            return GetSignedUser(context, out _);
        }
        private dynamic GetSignedUser(HttpContext context, out dynamic userManager)
        {
            userManager = GetUserManager(context);
            return userManager.GetUserAsync(context.User).Result;
        }

        public string GetSignedUserId(HttpContext context)
        {
            return GetUserManager(context).GetUserId(context.User);
        }

        public string GetSignedUserName(HttpContext context)
        {
            return GetUserManager(context).GetUserName(context.User);
        }

        public string GetSignedUserEmail(HttpContext context)
        {
            var user = GetSignedUser(context, out dynamic userManager);
            return userManager.GetEmailAsync(user).Result;
        }

        public string GetSignedUserPhoneNumber(HttpContext context)
        {
            var user = GetSignedUser(context, out dynamic userManager);
            return userManager.GetPhoneNumberAsync(user).Result;
        }

        public IList<string> GetSignedUserRoles(HttpContext context)
        {
            var user = GetSignedUser(context, out dynamic userManager);
            return userManager.GetRolesAsync(user).Result;
        }

    }
}
