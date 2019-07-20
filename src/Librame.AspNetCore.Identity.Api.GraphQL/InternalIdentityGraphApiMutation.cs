#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;

    /// <summary>
    /// 内部身份 Graph API 变化。
    /// </summary>
    internal class InternalIdentityGraphApiMutation : ObjectGraphType, IGraphApiMutation
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityGraphApiMutation"/> 实例。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{DefaultIdentityUser}"/></param>
        /// <param name="identifierService">给定的 <see cref="IIdentityIdentifierService"/>。</param>
        /// <param name="userEmailStore">给定的 <see cref="IUserEmailStore{TUser}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{InternalIdentityGraphApiMutation}"/>。</param>
        public InternalIdentityGraphApiMutation(SignInManager<DefaultIdentityUser> signInManager,
            IIdentityIdentifierService identifierService, IUserEmailStore<DefaultIdentityUser> userEmailStore,
            ILogger<InternalIdentityGraphApiMutation> logger)
        {
            Field<LoginMutationType>
            (
                name: "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginMutationType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var model = context.GetArgument<LoginModel>("user");

                    var result = signInManager.PasswordSignInAsync(model.Name,
                        model.Password, model.RememberMe, lockoutOnFailure: true).Result;

                    if (result.Succeeded)
                    {
                        model.Message = "User logged in.";
                    }
                    if (result.RequiresTwoFactor)
                    {
                        model.Message = "Need requires two factor.";
                        model.RedirectUrl = "./LoginWith2fa";
                    }
                    if (result.IsLockedOut)
                    {
                        model.Message = "User account locked out.";
                        model.RedirectUrl = "./Lockout";
                    }
                    else
                    {
                        model.IsError = true;
                        model.Message = "Invalid login attempt.";
                    }

                    return model.Log(logger);
                }
            );

            Field<RegisterMutationType>
            (
                name: "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RegisterMutationType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var model = context.GetArgument<RegisterModel>("user");
                    var user = new DefaultIdentityUser(model.Name)
                    {
                        Id = identifierService.GetUserIdAsync().Result,
                        Email = model.Email
                    };

                    var result = signInManager.UserManager.CreateAsync(user, model.Password).Result;
                    if (result.Succeeded)
                    {
                        model.Message = "User created a new account with password.";

                        var userId = signInManager.UserManager.GetUserIdAsync(user).Result;
                        var token = signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user).Result;

                        //var path = new PathString("/Account/ConfirmEmail");
                        //path.Add(QueryString.Create("userId", userId));
                        //path.Add(QueryString.Create("token", token));

                        //var url = httpContextAccessor.HttpContext.Request.NewUri(path);

                        //await _emailService.SendAsync(Input.Email,
                        //    Localizer[r => r.ConfirmYourEmail]?.Value,
                        //    Localizer[r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl)]?.Value);

                        userEmailStore.SetEmailAsync(user, model.Email, default).Wait();

                        signInManager.SignInAsync(user, isPersistent: false).Wait();
                    }
                    else
                    {
                        model.Errors.AddRange(result.Errors.Select(error =>
                        {
                            return new Exception($"Code: {error.Code}, Description: {error.Description}");
                        }));
                    }

                    return model.Log(logger);
                }
            );
        }

    }
}
