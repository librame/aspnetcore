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
using System.Threading.Tasks;

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    class ApplicationPrincipal : IApplicationPrincipal
    {
        private readonly dynamic _signInManager = null;


        public ApplicationPrincipal(ServiceFactoryDelegate serviceFactory)
        {
            _signInManager = serviceFactory.GetSignInMananger();
        }


        public bool IsSignedIn(HttpContext context)
        {
            return _signInManager.IsSignedIn(context.User);
        }


        public Task<dynamic> GetSignedUserAsync(HttpContext context)
        {
            return _signInManager.UserManager.GetUserAsync(context.User);
        }


        public string GetSignedUserId(HttpContext context)
        {
            return _signInManager.UserManager.GetUserId(context.User);
        }

        public async Task<string> GetSignedUserIdAsync(HttpContext context)
        {
            var user = await GetSignedUserAsync(context);

            return await _signInManager.UserManager.GetUserIdAsync(user);
        }


        public string GetSignedUserName(HttpContext context)
        {
            return _signInManager.UserManager.GetUserName(context.User);
        }

        public async Task<string> GetSignedUserNameAsync(HttpContext context)
        {
            var user = await GetSignedUserAsync(context);

            return await _signInManager.UserManager.GetUserNameAsync(user);
        }


        public async Task<string> GetSignedUserEmailAsync(HttpContext context)
        {
            var user = await GetSignedUserAsync(context);

            return await _signInManager.UserManager.GetEmailAsync(user);
        }

    }
}
