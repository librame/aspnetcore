#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// 声明当事人静态扩展。
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {

        /// <summary>
        /// 当作 Librame 身份标识。
        /// </summary>
        /// <param name="principal">给定的声明当事人。</param>
        /// <param name="serviceProvider">给定的服务提供程序。</param>
        /// <returns>返回 Librame 身份标识。</returns>
        public static LibrameClaimsIdentity AsLibrameIdentity(this ClaimsPrincipal principal, IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<IOptionsMonitor<AuthenticationExtensionOptions>>();

            return principal.AsLibrameIdentity(options.CurrentValue);
        }

        /// <summary>
        /// 当作 Librame 身份标识。
        /// </summary>
        /// <param name="principal">给定的声明当事人。</param>
        /// <param name="options">给定的认证选项管理器。</param>
        /// <returns>返回 Librame 身份标识。</returns>
        public static LibrameClaimsIdentity AsLibrameIdentity(this ClaimsPrincipal principal, AuthenticationExtensionOptions options)
        {
            var identity = principal.Identities.FirstOrDefault(id
                => id.AuthenticationType == AuthenticationExtensionOptions.DEFAULT_SCHEME);

            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            if (identity is LibrameClaimsIdentity)
                return (identity as LibrameClaimsIdentity);

            return new LibrameClaimsIdentity(identity.Claims, options);
        }

    }
}
