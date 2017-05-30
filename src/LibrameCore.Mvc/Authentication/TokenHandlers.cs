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

namespace LibrameCore.Authentication
{
    using Managers;
    using Handlers;
    using Utilities;

    /// <summary>
    /// 令牌处理程序接口。
    /// </summary>
    public interface ITokenHandler : IHander<TokenOptions>
    {
        /// <summary>
        /// 令牌生成器。
        /// </summary>
        ITokenGenerator Generator { get; }

        /// <summary>
        /// 用户管理器。
        /// </summary>
        IUserManager UserManager { get; }
    }


    /// <summary>
    /// 令牌处理程序。
    /// </summary>
    public class TokenHandler : AbstractHander<TokenOptions>, ITokenHandler
    {
        /// <summary>
        /// 构建一个默认处理程序实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public TokenHandler(ILibrameBuilder builder)
            : base(builder)
        {
        }


        /// <summary>
        /// 令牌生成器。
        /// </summary>
        public ITokenGenerator Generator => Builder.GetService<ITokenGenerator>();

        /// <summary>
        /// 用户管理器。
        /// </summary>
        public IUserManager UserManager => Builder.GetService<IUserManager>();


        /// <summary>
        /// 开始处理令牌。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        public override void OnHandling(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
                {
                    await context.Response.WriteBadRequestAsync("Invalid request.");
                    return;
                }

                IUserModel user;
                if (!TryValidateUser(context, out user))
                {
                    await context.Response.WriteBadRequestAsync("Invalid username or password.");
                    return;
                }

                var token = Generator.Generate(user);
                var result = new
                {
                    access_token = token,
                    expires_in = (int)Options.Expiration.TotalSeconds,
                };

                // Serialize and return the response
                await context.Response.WriteJsonAsync(result);
            });
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

            return UserManager.Validate(username, password, out user);
        }

    }
}
