#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Algorithm;
using LibrameStandard.Algorithm.Symmetries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    /// <summary>
    /// 令牌管理器接口。
    /// </summary>
    public interface ITokenManager : IManager
    {
        /// <summary>
        /// 算法选项。
        /// </summary>
        AlgorithmOptions AlgorithmOptions { get; }


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="identity">给定的 Librame 身份标识。</param>
        /// <returns>返回令牌字符串。</returns>
        string Encode(LibrameIdentity identity);


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <returns>返回 Librame 身份标识。</returns>
        LibrameIdentity Decode(string token);


        /// <summary>
        /// 异步验证令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <param name="requiredRoles">需要的角色集合。</param>
        /// <returns>返回 Librame 身份结果与标识。</returns>
        Task<(IdentityResult identityResult, LibrameIdentity identity)> ValidateAsync(string token,
            IEnumerable<string> requiredRoles);
    }


    /// <summary>
    /// 令牌管理器静态扩展。
    /// </summary>
    public static class TokenManagerExtensions
    {

        /// <summary>
        /// 附加令牌 COOKIE。
        /// </summary>
        /// <param name="response">给定的 HTTP 响应。</param>
        /// <param name="token">给定的令牌。</param>
        /// <param name="optionsAction">自定义选项动作方法。</param>
        public static void AppendTokenCookie(this HttpResponse response, string token,
            Action<CookieOptions> optionsAction = null)
        {
            var sa = response.HttpContext.RequestServices.GetService<ISymmetryAlgorithm>();
            token = sa.ToAes(token);

            var options = new CookieOptions
            {
                // 已启用对称加密，此处开启客户端访问（方便跨平台使用）
                HttpOnly = false
            };

            optionsAction?.Invoke(options);

            response.Cookies.Append(AuthenticationOptions.DEFAULT_TOKEN_COOKIE_NAME, token, options);
        }


        /// <summary>
        /// 移除令牌 COOKIE。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        public static void RemoveTokenCookie(this HttpContext context)
        {
            context.Response.Cookies.Delete(AuthenticationOptions.DEFAULT_TOKEN_COOKIE_NAME);
        }


        /// <summary>
        /// 解析经过加密的令牌 COOKIE。
        /// </summary>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回令牌字符串。</returns>
        public static string ResolveTokenCookie(this HttpRequest request)
        {
            return request.Cookies[AuthenticationOptions.DEFAULT_TOKEN_COOKIE_NAME];
        }

        /// <summary>
        /// 解析经过解密的令牌 COOKIE。
        /// </summary>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回令牌字符串。</returns>
        public static string ResolveDecryptTokenCookie(this HttpRequest request)
        {
            var token = request.ResolveTokenCookie();

            var sa = request.HttpContext.RequestServices.GetService<ISymmetryAlgorithm>();
            token = sa.FromAes(token);

            return token;
        }


        /// <summary>
        /// 异步 Librame 登入。
        /// </summary>
        /// <param name="manager">给定的认证管理器。</param>
        /// <param name="options">给定的令牌选项。</param>
        /// <param name="identity">给定的 Librame 身份标识。</param>
        /// <param name="token">给定的令牌。</param>
        /// <returns>返回一个异步操作。</returns>
        public static Task LibrameSignInAsync(this AuthenticationManager manager, TokenOptions options,
            LibrameIdentity identity, string token)
        {
            var utcNow = DateTimeOffset.UtcNow;
            var utcExpires = utcNow.Add(options.Expiration);

            // Add Cookie
            manager.HttpContext.Response.AppendTokenCookie(token, opts =>
            {
                opts.Expires = utcExpires;
            });

            return manager.SignInAsync(AuthenticationOptions.DEFAULT_SCHEME,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    IssuedUtc = utcNow,
                    ExpiresUtc = utcExpires,
                    AllowRefresh = false
                });
        }

        /// <summary>
        /// 异步 Librame 登出。
        /// </summary>
        /// <param name="manager">给定的认证管理器。</param>
        /// <returns>返回一个异步操作。</returns>
        public static Task LibrameSignOutAsync(this AuthenticationManager manager)
        {
            manager.HttpContext.RemoveTokenCookie();

            return manager.SignOutAsync(AuthenticationOptions.DEFAULT_SCHEME);
        }

    }
}
