#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Extensions.Authentication;
using LibrameCore.Extensions.Authentication.Managers;
using LibrameCore.Extensions.Authentication.Policies;
using LibrameCore.Extensions.Authentication.Repositories;
using LibrameCore.Extensions.Authentication.Senders;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace LibrameStandard.Abstractions
{
    /// <summary>
    /// <see cref="IExtensionCollection"/> 认证静态扩展。
    /// </summary>
    public static class AuthenticationExtensionCollectionExtensions
    {

        /// <summary>
        /// 添加认证扩展。
        /// </summary>
        /// <param name="extensions">给定的 <see cref="IExtensionCollection"/>。</param>
        /// <param name="configureOptions">给定的后置配置选项动作（可选）。</param>
        /// <returns>返回 <see cref="IExtensionCollection"/>。</returns>
        public static IExtensionCollection AddAuthenticationExtension(this IExtensionCollection extensions,
            Action<AuthenticationExtensionOptions> configureOptions = null)
        {
            extensions.ConfigureOptions(configureOptions);
            
            extensions.Services.TryAddSingleton<IPasswordManager, PasswordManager>();
            extensions.Services.TryAddSingleton<ITokenManager, TokenManager>();
            extensions.Services.TryAddSingleton<IAuthenticationPolicy, LibrameAuthenticationPolicy>();
            extensions.Services.TryAddSingleton<ISmsSender, SmsSender>();
            extensions.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 注：因 SqlServerDbContext/Writer 在 EFCore 中为范围实例
            // 所以 IAuthenticationRepository 只能注册为范围实例
            extensions.Services.TryAddScoped(typeof(IAuthenticationRepository<,,,,,>), typeof(SqlServerAuthenticationRepository<,,,,,>));
            
            // Add Authentication
            extensions.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = AuthenticationExtensionOptions.DEFAULT_SCHEME;
                options.AddScheme<LibrameAuthenticationHandler>(AuthenticationExtensionOptions.DEFAULT_SCHEME, null);
            })
                .AddCookie()
                .AddJwtBearer();

            return extensions;
        }


        //private static void AddBaseAuthentication(this IServiceCollection services,
        //    AuthenticationOptions options)
        //{
        //// 默认以授权编号为密钥
        //var algorithmOptions = app.ApplicationServices.GetOptions<AlgorithmOptions>();
        //var secretKeyBytes = algorithmOptions.FromAuthIdAsBytes();

        //var tokenParameters = new TokenValidationParameters
        //{
        //    AuthenticationType = AuthenticationOptions.DEFAULT_SCHEME,

        //    ValidateIssuerSigningKey = true,
        //    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

        //    ValidateIssuer = true,
        //    ValidIssuer = TokenOptions.DefaultIssuer,

        //    ValidateAudience = true,
        //    ValidAudience = TokenOptions.DefaultAudience,

        //    // Validate the token expiry
        //    ValidateLifetime = true,

        //    // If you want to allow a certain amount of clock drift, set that here:
        //    ClockSkew = TimeSpan.Zero
        //};
        //}

        //private static async Task ValidatePrincipalAsync(CookieValidatePrincipalContext context)
        //{
        //    if (context.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        var identity = context.HttpContext.User.AsLibrameIdentity(context.HttpContext.RequestServices);

        //        if (!identity.IsAuthenticated)
        //        {
        //            context.RejectPrincipal();

        //            await context.HttpContext.LibrameSignOutAsync();
        //        }
        //    }
        //}

    }
}
