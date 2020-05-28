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
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Web.Builders;
    using AspNetCore.Web.Services;
    using Extensions;
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ApplicationPrincipal : IApplicationPrincipal
    {
        private readonly IUserPortraitService _userPortrait;


        public ApplicationPrincipal(IWebBuilder webBuilder,
            IUserPortraitService userPortrait)
        {
            Builder = webBuilder.NotNull(nameof(webBuilder));
            _userPortrait = userPortrait.NotNull(nameof(userPortrait));
        }


        public IWebBuilder Builder { get; }


        private dynamic GetSignInManager(HttpContext context)
        {
            var managerType = typeof(SignInManager<>).MakeGenericType(Builder.UserType);
            return context?.RequestServices?.GetRequiredService(managerType);
        }

        private dynamic GetUserManager(HttpContext context)
            => GetSignInManager(context).UserManager;


        public bool IsSignedIn(HttpContext context)
            => GetSignInManager(context).IsSignedIn(context.User);


        public dynamic GetSignedUser(HttpContext context)
            => GetSignedUser(context, out _);

        private dynamic GetSignedUser(HttpContext context, out dynamic userManager)
        {
            userManager = GetUserManager(context);
            return userManager.GetUserAsync(context.User).Result;
        }


        public string GetSignedUserId(HttpContext context)
            => GetUserManager(context).GetUserId(context.User);

        public string GetSignedUserName(HttpContext context)
            => GetUserManager(context).GetUserName(context.User);


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

        public string GetSignedUserPortrait(HttpContext context)
        {
            var user = GetSignedUser(context, out _);
            return _userPortrait.GetPortraitPathAsync(user).Result;
        }

        public IList<string> GetSignedUserRoles(HttpContext context)
        {
            var user = GetSignedUser(context, out dynamic userManager);
            return userManager.GetRolesAsync(user).Result;
        }

    }
}
