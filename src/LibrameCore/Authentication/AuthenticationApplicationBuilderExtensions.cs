#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard;
using LibrameStandard.Algorithm;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;

namespace LibrameCore
{
    using Authentication;

    /// <summary>
    /// 认证应用构建器静态扩展。
    /// </summary>
    public static class AuthenticationApplicationBuilderExtensions
    {

        /// <summary>
        /// 使用 Librame 认证。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        /// <param name="jwtBearerOptionsAction">给定的 JWT 选项方法。</param>
        /// <param name="cookieOptionsAction">给定的 Cookie 认证选项方法。</param>
        public static void UseLibrameAuthentication(this IApplicationBuilder app,
            Action<JwtBearerOptions> jwtBearerOptionsAction = null,
            Action<CookieAuthenticationOptions> cookieOptionsAction = null)
        {
            var options = app.ApplicationServices.GetOptions<AuthenticationOptions>();

            // Use JWT
            app.UseLibrameJwtBearerAuthentication(options, jwtBearerOptionsAction);

            // Use Cookie
            app.UseLibrameCookieAuthentication(options, cookieOptionsAction);
        }


        private static void UseLibrameJwtBearerAuthentication(this IApplicationBuilder app,
            AuthenticationOptions options, Action<JwtBearerOptions> jwtBearerOptionsAction = null)
        {
            // 默认以授权编号为密钥
            var algorithmOptions = app.ApplicationServices.GetOptions<AlgorithmOptions>();
            var secretKeyBytes = algorithmOptions.FromAuthIdAsBytes();

            var parameters = new TokenValidationParameters
            {
                AuthenticationType = AuthenticationOptions.DEFAULT_SCHEME,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                
                ValidateIssuer = true,
                ValidIssuer = TokenOptions.DefaultIssuer,
                
                ValidateAudience = true,
                ValidAudience = TokenOptions.DefaultAudience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            var jwtBearerOptions = new JwtBearerOptions
            {
                AuthenticationScheme = AuthenticationOptions.DEFAULT_SCHEME,
                // 是否自动启用验证，如果不启用，则即便客服端传输了Cookie信息，服务端也不会主动解析。
                // 除了明确配置了 [Authorize(ActiveAuthenticationSchemes = "上面的方案名")] 属性的地方，才会解析，此功能一般用在需要在同一应用中启用多种验证方案的时候。比如分Area.
                //AutomaticAuthenticate = true,
                //AutomaticChallenge = true,
                TokenValidationParameters = parameters
            };

            // Custom Configure Options
            jwtBearerOptionsAction?.Invoke(jwtBearerOptions);

            // Use Options
            app.UseJwtBearerAuthentication(jwtBearerOptions);
        }


        private static void UseLibrameCookieAuthentication(this IApplicationBuilder app,
            AuthenticationOptions options, Action<CookieAuthenticationOptions> cookieOptionsAction = null)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationScheme = AuthenticationOptions.DEFAULT_SCHEME,
                // 是否自动启用验证，如果不启用，则即便客服端传输了 Cookie 信息，服务端也不会主动解析。
                // 除了明确配置了 [Authorize(ActiveAuthenticationSchemes = "上面的方案名")] 属性的地方，才会解析，此功能一般用在需要在同一应用中启用多种验证方案的时候。比如分Area.
                //AutomaticAuthenticate = true,
                //AutomaticChallenge = true,
                CookieName = AuthenticationOptions.DEFAULT_COOKIE_NAME,
                LoginPath = new PathString(AuthenticationOptions.DEFAULT_LOGIN_PATH),
                ExpireTimeSpan = options.Token.Expiration,
            };

            // Custom Configure Options
            cookieOptionsAction?.Invoke(cookieOptions);

            // Use Options
            app.UseCookieAuthentication(cookieOptions);
        }

    }
}
