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
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using Extensions;
    using Extensions.Core;
    using Extensions.Network;

    class IdentityGraphApiMutation : ObjectGraphType, IGraphApiMutation
    {
        [InjectionService]
        private ILogger<IdentityGraphApiMutation> _logger = null;

        [InjectionService]
        private IdentityStoreIdentifier _identifier = null;

        [InjectionService]
        private IEmailService _emailService = null;

        [InjectionService]
        private IExpressionStringLocalizer<RegisterApiModelResource> _localizer = null;

        [InjectionService]
        private IIdentityBuilderWrapper _builderWrapper = null;

        [InjectionService]
        private IServiceProvider _serviceProvider = null;

        private readonly dynamic _signInManager = null;
        private readonly dynamic _userManager = null;
        private readonly dynamic _userStore = null;
        private readonly dynamic _emailStore;


        public IdentityGraphApiMutation(IInjectionService injectionService)
        {
            injectionService.Inject(this);

            _signInManager = _serviceProvider.GetService(typeof(SignInManager<>)
                .MakeGenericType(_builderWrapper.RawBuilder.UserType));
            _userManager = _signInManager.UserManager;
            _userStore = _serviceProvider.GetService(typeof(IUserStore<>)
                .MakeGenericType(_builderWrapper.RawBuilder.UserType));
            _emailStore = _userStore.GetUserEmailStore(_userManager);

            Name = nameof(ISchema.Mutation);

            AddLoginTypeField();

            AddRegisterTypeField();
        }


        private void AddLoginTypeField()
        {
            FieldAsync<LoginType>
            (
                name: "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<LoginApiModel>("user");

                    var result = await _signInManager.PasswordSignInAsync(model.Email,
                        model.Password, model.RememberMe, lockoutOnFailure: true);

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

                    return model.Log(_logger);
                }
            );
        }

        private void AddRegisterTypeField()
        {
            FieldAsync<RegisterType>
            (
                name: "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RegisterInputType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<RegisterApiModel>("user");

                    var user = new DefaultIdentityUser();
                    user.Id = await _identifier.GetUserIdAsync();

                    var result = await _userManager.CreateUserByEmail(_userStore, model.Email, model.Password, user);
                    if (result.Succeeded)
                    {
                        model.Message = "User created a new account with password.";
                        model.UserId = user.Id;

                        // 确认邮件
                        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmEmailLocator = model.ConfirmEmailUrl.AsUriLocator();
                        confirmEmailLocator.ChangeQueries(queries =>
                        {
                            queries.AddOrUpdate("userId", model.UserId, (key, value) => model.UserId);
                            queries.AddOrUpdate("code", code, (key, value) => code);
                        });
                        var confirmEmailExternalLink = HtmlEncoder.Default.Encode(confirmEmailLocator.ToString());

                        await _emailService.SendAsync(user.Email,
                            _localizer[r => r.ConfirmYourEmail]?.Value,
                            _localizer[r => r.ConfirmYourEmailFormat, confirmEmailExternalLink]?.Value);
                        //await userStore.GetUserEmailStore(signInManager).SetEmailAsync(user, model.Email, default);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(3, "User created a new account with password.");
                    }

                    IEnumerable<IdentityError> errors = result.Errors;
                    if (errors.IsNotNullOrEmpty())
                    {
                        model.Errors.AddRange(errors.Select(error =>
                        {
                            return new Exception($"Code: {error.Code}, Description: {error.Description}");
                        }));
                    }

                    return model.Log(_logger);
                }
            );
        }

    }
}
