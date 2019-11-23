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
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Encodings.Web;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using Extensions;
    using Extensions.Core;
    using Extensions.Data;
    using Extensions.Network;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class IdentityGraphApiMutation<TUser> : ObjectGraphType, IGraphApiMutation
        where TUser : class, IId<string>
    {
        [InjectionService]
        private ILogger<IdentityGraphApiMutation<TUser>> _logger = null;

        [InjectionService]
        private IdentityStoreIdentifier _identifier = null;

        [InjectionService]
        private IEmailService _emailService = null;

        [InjectionService]
        private IStringLocalizer<RegisterApiModelResource> _localizer = null;

        [InjectionService]
        private IUserStore<TUser> _userStore = null;

        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        private readonly UserManager<TUser> _userManager = null;


        public IdentityGraphApiMutation(IInjectionService injectionService)
        {
            injectionService.Inject(this);

            _userManager = _signInManager.UserManager;

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
                        model.Password, model.RememberMe, lockoutOnFailure: false).ConfigureAndResultAsync();

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

                    var user = typeof(TUser).EnsureCreate<TUser>();
                    user.Id = await _identifier.GetUserIdAsync().ConfigureAndResultAsync();

                    var result = await SignInManagerUtility.CreateUserByEmail(_userManager, _userStore,
                        model.Email, model.Password, user).ConfigureAndResultAsync();

                    if (result.Succeeded)
                    {
                        model.Message = "User created a new account with password.";
                        model.UserId = user.Id;

                        // 确认邮件
                        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAndResultAsync();

                        var confirmEmailLocator = model.ConfirmEmailUrl.AsUriCombinerCore();
                        confirmEmailLocator.ChangeQueries(queries =>
                        {
                            queries.AddOrUpdate("userId", model.UserId, (key, value) => model.UserId);
                            queries.AddOrUpdate("code", code, (key, value) => code);
                        });
                        var confirmEmailExternalLink = HtmlEncoder.Default.Encode(confirmEmailLocator.ToString());

                        await _emailService.SendAsync(model.Email,
                            _localizer.GetString(r => r.ConfirmYourEmail)?.Value,
                            _localizer.GetString(r => r.ConfirmYourEmailFormat, confirmEmailExternalLink)?.Value).ConfigureAndWaitAsync();
                        //await userStore.GetUserEmailStore(signInManager).SetEmailAsync(user, model.Email, default);

                        await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                        _logger.LogInformation(3, "User created a new account with password.");
                    }

                    IEnumerable<IdentityError> errors = result.Errors;
                    if (errors.IsNotEmpty())
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
