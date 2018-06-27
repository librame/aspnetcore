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
        /// Cookie 认证选项。
        /// </summary>
        protected CookieAuthenticationOptions CookieAuthOptions;


        /// <summary>
        /// 构造一个 <see cref="LibrameAuthenticationPolicy"/> 实例。
        /// </summary>
        /// <param name="tokenManager">给定的 <see cref="ITokenManager"/>。</param>
        /// <param name="cookieOptions">给定的 Cookie 选项。</param>
        public LibrameAuthenticationPolicy(ITokenManager tokenManager,
            IOptions<CookieAuthenticationOptions> cookieOptions)
        {
            TokenManager = tokenManager.NotNull(nameof(tokenManager));
            CookieAuthOptions = cookieOptions.NotNull(nameof(cookieOptions)).Value;
        }


        /// <summary>
        /// 令牌管理器。
        /// </summary>
        public ITokenManager TokenManager { get; }
        
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
            // 优先检测 Cookie
            var token = GetCookieToken(context);
            if (!string.IsNullOrEmpty(token))
                return (AuthenticationStatus.Cookie, TokenManager.Decode(token));

            // 其次检测 Header
            token = GetHeaderToken(context);
            if (!string.IsNullOrEmpty(token))
                return (AuthenticationStatus.Header, TokenManager.Decode(token));

            // 最后检测 Query
            token = GetQueryToken(context);
            if (!string.IsNullOrEmpty(token))
                return (AuthenticationStatus.Query, TokenManager.Decode(token));

            // 表单无提交时会抛出异常
            //token = GetFormToken(context);
            //if (!string.IsNullOrEmpty(token))
            //    return (AuthenticationStatus.Form, TokenManager.Decode(token));

            return (AuthenticationStatus.Default, null);
        }


        /// <summary>
        /// 添加令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <param name="expires">给定的过期时间。</param>
        /// <param name="token">给定的令牌。</param>
        /// <returns>返回令牌。</returns>
        public virtual string AddToken(HttpContext context, DateTimeOffset expires, string token)
        {
            AddCookieToken(context, expires, token);
            AddHeaderToken(context, expires, token);

            return token;
        }


        /// <summary>
        /// 删除令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回令牌。</returns>
        public virtual string DeleteToken(HttpContext context)
        {
            var token = DeleteCookieToken(context);
            token = DeleteHeaderToken(context);

            return token;
        }


        #region Cookie

        /// <summary>
        /// 获取 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetCookieToken(HttpContext context)
        {
            return CookieAuthOptions.CookieManager.GetRequestCookie(context, Options.Cookie.Name);
        }

        /// <summary>
        /// 添加 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <param name="expires">给定的过期时间。</param>
        /// <param name="token">给定的令牌。</param>
        protected virtual void AddCookieToken(HttpContext context, DateTimeOffset expires, string token)
        {
            var cookies = Options.Cookie.Build(context, expires);

            CookieAuthOptions.CookieManager.AppendResponseCookie(context, Options.Cookie.Name, token, cookies);
        }

        /// <summary>
        /// 删除 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string DeleteCookieToken(HttpContext context)
        {
            var token = GetCookieToken(context);

            var cookies = CookieAuthOptions.Cookie.Build(context);
            cookies.Expires = null;

            CookieAuthOptions.CookieManager.DeleteCookie(context, Options.Cookie.Name, cookies);

            return token;
        }

        #endregion


        #region Header

        /// <summary>
        /// 获取 Header 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetHeaderToken(HttpContext context)
        {
            var authentication = context.Request.Headers["Authentication"].ToString();

            if (!string.IsNullOrEmpty(authentication))
            {
                // 认证格式：Scheme<空格>Parameter
                var headerValue = AuthenticationHeaderValue.Parse(authentication);

                // 仅支持 Bearer/Librame 格式
                if (headerValue.Scheme == "Bearer" || headerValue.Scheme == "Librame")
                    authentication = headerValue.Parameter;
            }

            return authentication;
        }

        /// <summary>
        /// 添加 Header 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <param name="expires">给定的过期时间。</param>
        /// <param name="token">给定的令牌。</param>
        protected virtual void AddHeaderToken(HttpContext context, DateTimeOffset expires, string token)
        {
            var headerValue = new AuthenticationHeaderValue("Bearer", token);

            context.Request.Headers.Add("Authentication", headerValue.ToString());
        }

        /// <summary>
        /// 删除 Header 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string DeleteHeaderToken(HttpContext context)
        {
            var token = GetHeaderToken(context);
            context.Request.Headers.Remove("Authentication");

            return token;
        }

        #endregion

        
        /// <summary>
        /// 获取 Query 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回令牌或空字符串。</returns>
        protected virtual string GetQueryToken(HttpContext context)
        {
            return context.Request.Query["token"].ToString();
        }

        ///// <summary>
        ///// 获取 Form 令牌。
        ///// </summary>
        ///// <param name="context">给定的 HTTP 上下文。</param>
        ///// <returns>返回令牌或空字符串。</returns>
        //protected virtual string GetFormToken(HttpContext context)
        //{
        //    return context.Request.Form["token"].ToString();
        //}

    }
}
