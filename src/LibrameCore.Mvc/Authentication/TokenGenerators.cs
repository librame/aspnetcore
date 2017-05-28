#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrameCore.Authentication
{
    using Utility;

    /// <summary>
    /// 令牌生成器接口。
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// 令牌生成选项。
        /// </summary>
        TokenGenerateOptions Options { get; }

        /// <summary>
        /// 用户认证接口。
        /// </summary>
        IUserAuthentication UserAuthentication { get; }


        /// <summary>
        /// 生成令牌。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        void Generate(IApplicationBuilder app);
    }


    /// <summary>
    /// 令牌生成器。
    /// </summary>
    public class TokenGenerator : ITokenGenerator
    {
        /// <summary>
        /// 构造一个令牌生成器。
        /// </summary>
        /// <param name="option">给定的令牌生成器选项。</param>
        /// <param name="userAuthentication">给定的用户认证接口。</param>
        public TokenGenerator(TokenGenerateOptions option, IUserAuthentication userAuthentication)
        {
            Options = option.NotNull(nameof(option));
            UserAuthentication = userAuthentication.NotNull(nameof(userAuthentication));
        }


        /// <summary>
        /// 令牌生成选项。
        /// </summary>
        public TokenGenerateOptions Options { get; }

        /// <summary>
        /// 用户认证接口。
        /// </summary>
        public IUserAuthentication UserAuthentication { get; }


        /// <summary>
        /// 错误请求。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <param name="message">给定的错误消息。</param>
        /// <returns>返回异步操作。</returns>
        protected virtual async Task BadRequest(HttpContext context, string message)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(message);
        }

        /// <summary>
        /// 尝试根据 HTTP 上下文信息认证用户。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <param name="user">输出用户模型接口。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        protected virtual bool TryValidateUser(HttpContext context, out IUserModel user)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            return UserAuthentication.Validate(username, password, out user);
        }

        /// <summary>
        /// 生成令牌。
        /// </summary>
        /// <param name="user">给定的用户模型接口。</param>
        /// <returns>返回令牌字符串。</returns>
        protected virtual string GenerateToken(IUserModel user)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UniqueId),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, user.UniqueId), // Guid.NewGuid().ToString()
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                Options.Issuer,
                Options.Audience,
                claims,
                now,
                now.Add(Options.Expiration),
                Options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }


        /// <summary>
        /// 生成令牌。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        public virtual void Generate(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
                {
                    await BadRequest(context, "format not corrent");
                    return;
                }

                IUserModel user;
                if (!TryValidateUser(context, out user))
                {
                    await BadRequest(context, "Invalid username or password.");
                    return;
                }

                var token = GenerateToken(user);
                var result = new
                {
                    access_token = token,
                    expires_in = (int)Options.Expiration.TotalSeconds,
                };

                // Serialize and return the response
                context.Response.ContentType = "application/json";
                string json = JsonConvert.SerializeObject(result, new JsonSerializerSettings { Formatting = Formatting.Indented });
                await context.Response.WriteAsync(json);
            });
        }

    }
}
