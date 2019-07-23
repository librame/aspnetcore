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
        /// <param name="identifier">给定的 <see cref="IIdentityIdentifierService"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILogger{InternalIdentityGraphApiMutation}"/>。</param>
        public InternalIdentityGraphApiMutation(SignInManager<DefaultIdentityUser> signInManager,
            IIdentityIdentifierService identifier, IUserStore<DefaultIdentityUser> userStore,
            ILogger<InternalIdentityGraphApiMutation> logger)
        {
            Name = nameof(ISchema.Mutation);

            Field<LoginInputType>
            (
                name: "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var model = context.GetArgument<LoginApiModel>("user");

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

            Field<RegisterInputType>
            (
                name: "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RegisterInputType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var model = context.GetArgument<RegisterApiModel>("user");
                    var user = new DefaultIdentityUser(model.Name)
                    {
                        Id = identifier.GetUserIdAsync().Result,
                        Email = model.Email
                    };

                    var result = signInManager.UserManager.CreateAsync(user, model.Password).Result;
                    if (result.Succeeded)
                    {
                        model.Message = "User created a new account with password.";
                        model.UserId = signInManager.UserManager.GetUserIdAsync(user).Result;
                        model.Token = signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user).Result;

                        //var path = new PathString("/Account/ConfirmEmail");
                        //path.Add(QueryString.Create("userId", model.UserId));
                        //path.Add(QueryString.Create("token", model.Token));

                        //var url = httpContextAccessor.HttpContext.Request.NewUri(path);

                        //await _emailService.SendAsync(Input.Email,
                        //    Localizer[r => r.ConfirmYourEmail]?.Value,
                        //    Localizer[r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl)]?.Value);

                        userStore.GetUserEmailStore(signInManager).SetEmailAsync(user, model.Email, default).Wait();

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
