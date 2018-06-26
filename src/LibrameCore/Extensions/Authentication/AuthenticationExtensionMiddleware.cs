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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Authentication
{
    using Descriptors;

    /// <summary>
    /// 认证扩展中间件。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TRoleId">指定的角色主键类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户主键类型。</typeparam>
    /// <typeparam name="TUserRoleId">指定的用户角色主键类型。</typeparam>
    public class AuthenticationExtensionMiddleware<TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId>
        where TRole : class, IRoleDescriptor<TRoleId>
        where TUser : class, IUserDescriptor<TUserId>
        where TUserRole : class, IUserRoleDescriptor<TUserRoleId, TUserId, TRoleId>
    {
        /// <summary>
        /// 构造一个 <see cref="AuthenticationExtensionMiddleware{TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId}"/> 实例。
        /// </summary>
        /// <param name="next">给定的下一步请求委托。</param>
        /// <param name="schemes">给定的认证方案提供程序。</param>
        /// <param name="options">给定的认证选项。</param>
        public AuthenticationExtensionMiddleware(RequestDelegate next, IAuthenticationSchemeProvider schemes,
            IOptions<AuthenticationExtensionOptions> options)
        {
            Options = options.NotNull(nameof(options)).Value;
            Next = next.NotNull(nameof(next));
            Schemes = schemes.NotNull(nameof(schemes));
        }


        /// <summary>
        /// 认证选项。
        /// </summary>
        public AuthenticationExtensionOptions Options { get; }


        /// <summary>
        /// 下一步请求委托。
        /// </summary>
        protected RequestDelegate Next { get; private set; }

        /// <summary>
        /// 认证方案提供程序。
        /// </summary>
        protected IAuthenticationSchemeProvider Schemes { get; private set; }


        private IAuthenticationPolicy _policy = null;
        private IAuthenticationRepository<TRole, TUser, TUserRole,
            TRoleId, TUserId, TUserRoleId> _repository = null;

        /// <summary>
        /// 异步执行。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回异步操作。</returns>
        public virtual Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(Options.Local.TokenPath, StringComparison.Ordinal))
            {
                return Next.Invoke(context);
            }

            _policy = context.RequestServices.GetRequiredService<IAuthenticationPolicy>();
            _repository = context.RequestServices.GetRequiredService<IAuthenticationRepository<TRole,
                TUser, TUserRole, TRoleId, TUserId, TUserRoleId>>();

            switch (context.Request.Method.ToLower())
            {
                case "get":
                    {
                        // Validate
                        return InvokeValidateToken(context);
                    }

                case "post":
                    {
                        // Login
                        return InvokeGenerateToken(context);
                    }

                default:
                    {
                        return context.Response.WriteErrorRequestAsync(Resources.Core.InvalidRequest);
                    }
            }
        }


        /// <summary>
        /// 异步验证令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回异步操作。</returns>
        protected virtual async Task InvokeValidateToken(HttpContext context)
        {
            var auth = _policy.Authenticate(context);
            if (auth.Status == AuthenticationStatus.Default || auth.Identity == null)
            {
                await context.Response.WriteErrorRequestAsync(Resources.Core.InvalidToken);
                return;
            }

            var json = new
            {
                authentication_type = auth.Identity.AuthenticationType,
                is_authenticated = auth.Identity.IsAuthenticated,
                name = auth.Identity.Name,
                issuer = auth.Identity.Issuer,
                audience = auth.Identity.Audience,
                issued_time = auth.Identity.IssuedTimeUtc.ToLocalTime().ToString(),
                expiration_time = auth.Identity.ExpirationTimeUtc.ToLocalTime().ToString(),
                id = auth.Identity.JwtId,
                subject = auth.Identity.Subject,
                uniqueName = auth.Identity.UniqueName,
                roles = string.Join(',', auth.Identity.Roles)
            };

            await context.Response.WriteJsonAsync(json);
            return;
        }


        /// <summary>
        /// 异步生成令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回异步操作。</returns>
        protected virtual async Task InvokeGenerateToken(HttpContext context)
        {
            if (!context.Request.HasFormContentType)
            {
                await context.Response.WriteErrorRequestAsync(Resources.Core.InvalidRequest);
                return;
            }

            // Login
            var username = context.Request.Form["username"].ToString();
            var password = context.Request.Form["password"].ToString();
            
            var account = await _repository.ValidateUserAsync(username, password);
            if (account.User == null)
            {
                var message = account.Result.Errors.FirstOrDefault()?.Description;
                message = message.AsOrDefault(Resources.Core.UsernameOrPasswordIsEmpty);

                await context.Response.WriteErrorRequestAsync(message);
                return;
            }

            // Login Failed
            if (!account.Result.Succeeded)
            {
                var message = account.Result.Errors.FirstOrDefault()?.Description;
                message = message.AsOrDefault(Resources.Core.InvalidUsernameOrPassword);

                await context.Response.WriteErrorRequestAsync(message);
                return;
            }

            // Login Successed
            //await LoginSuccessed(context, result);

            // Identity
            var roleNames = await _repository.GetRoleNamesAsync(account.User.Name);
            var identity = new LibrameClaimsIdentity(account.User.Name, roleNames, Options);

            // Token
            var token = _policy.TokenManager.Encode(identity);

            if (context.Request.IsAjaxRequest())
            {
                var expires = (identity.ExpirationTimeUtc - identity.IssuedTimeUtc);

                // Write Response
                var json = new
                {
                    access_token = token,
                    token_type = AuthenticationExtensionOptions.DEFAULT_SCHEME,
                    expires_in_seconds = (int)expires.TotalSeconds,
                };

                await context.Response.WriteJsonAsync(json);
            }
            else
            {
                // SignIn
                _policy.AddCookieToken(context, identity.ExpirationTimeUtc, token);

                // Redirect ReturnUrl
                var returnUrl = context.Request.Form["returnUrl"].ToString();
                returnUrl = returnUrl.AsOrDefault(context.Request.Query["returnUrl"].ToString());

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    // Format Token
                    if (returnUrl.Contains("{token}"))
                        returnUrl = returnUrl.Replace("{token}", token);

                    context.Response.Redirect(returnUrl);
                }
            }

            return;
        }

    }
}
