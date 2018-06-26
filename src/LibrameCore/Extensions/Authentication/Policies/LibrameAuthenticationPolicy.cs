#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;

namespace LibrameCore.Extensions.Authentication.Policies
{
    using Managers;

    /// <summary>
    /// Librame 认证策略。
    /// </summary>
    public class LibrameAuthenticationPolicy : IAuthenticationPolicy
    {

        /// <summary>
        /// 构造一个 <see cref="LibrameAuthenticationPolicy"/> 实例。
        /// </summary>
        /// <param name="tokenManager">给定的 <see cref="ITokenManager"/>。</param>
        /// <param name="cookieOptions">给定的 Cookie 选项。</param>
        public LibrameAuthenticationPolicy(ITokenManager tokenManager,
            IOptions<CookieAuthenticationOptions> cookieOptions)
        {
            TokenManager = tokenManager.NotNull(nameof(tokenManager));
            CookieOptions = cookieOptions.NotNull(nameof(cookieOptions)).Value;
        }


        /// <summary>
        /// 令牌管理器。
        /// </summary>
        public ITokenManager TokenManager { get; }

        /// <summary>
        /// Cookie 选项。
        /// </summary>
        public CookieAuthenticationOptions CookieOptions { get; }

        /// <summary>
        /// 认证选项。
        /// </summary>
        public AuthenticationExtensionOptions Options => TokenManager.Options;


        /// <summary>
        /// 认证身份。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回认证状态与身份标识。</returns>
        public virtual (AuthenticationStatus Status, LibrameClaimsIdentity Identity) Authenticate(HttpContext context)
        {
            // 优先检测 Cookie 是否存在
            var token = GetCookieToken(context);
            if (!string.IsNullOrEmpty(token))
                return (AuthenticationStatus.Cookie, TokenManager.Decode(token));

            // 检测请求头部信息是否存在
            var authentication = context.Request.Headers["Authentication"].ToString();
            if (!string.IsNullOrEmpty(authentication))
            {
                // 认证格式：Scheme<空格>Parameter
                var header = AuthenticationHeaderValue.Parse(authentication);
                if (header.Scheme == "Bearer" || header.Scheme == "Librame")
                    return (AuthenticationStatus.Header, TokenManager.Decode(header.Parameter));
                else
                    return (AuthenticationStatus.Header, null); // 不被支持的头部认证信息
            }

            // 最后检测查询参数
            token = context.Request.Query["token"].ToString();
            if (!string.IsNullOrEmpty(token))
                return (AuthenticationStatus.Query, TokenManager.Decode(token));

            // 表单无提交时会抛出异常
            //token = context.Request.Form["token"].ToString();
            //if (!string.IsNullOrEmpty(token))
            //    return (AuthenticationStatus.Form, TokenManager.Decode(token));

            return (AuthenticationStatus.Default, null);
        }


        /// <summary>
        /// 添加 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <param name="expires">给定的过期时间。</param>
        /// <param name="token">给定的令牌。</param>
        /// <returns>返回令牌。</returns>
        public virtual string AddCookieToken(HttpContext context, DateTimeOffset expires, string token)
        {
            //var cookies = CookieOptions.Cookie.Build(context, expires);
            var cookies = Options.Cookie.Build(context, expires);

            CookieOptions.CookieManager.AppendResponseCookie(context, Options.Cookie.Name, token, cookies);

            return token;
        }


        /// <summary>
        /// 删除 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回令牌。</returns>
        public virtual string DeleteCookieToken(HttpContext context)
        {
            var token = GetCookieToken(context);

            var cookies = CookieOptions.Cookie.Build(context);
            cookies.Expires = null;

            CookieOptions.CookieManager.DeleteCookie(context, Options.Cookie.Name, cookies);

            return token;
        }


        /// <summary>
        /// 获取 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回令牌。</returns>
        protected virtual string GetCookieToken(HttpContext context)
        {
            return CookieOptions.CookieManager.GetRequestCookie(context, Options.Cookie.Name);
        }

    }
}
