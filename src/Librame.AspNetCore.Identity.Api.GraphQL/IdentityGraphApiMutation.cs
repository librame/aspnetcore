#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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
    using AspNetCore.Identity.Api.Models;
    using AspNetCore.Identity.Api.ModelTypes;
    using AspNetCore.Identity.Api.Resources;
    using AspNetCore.Identity.Stores;
    using Extensions;
    using Extensions.Core.Combiners;
    using Extensions.Core.Identifiers;
    using Extensions.Core.Services;
    using Extensions.Network.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityGraphApiMutation<TUser> : ObjectGraphType, IGraphApiMutation
        where TUser : class, IIdentifier
    {
        [InjectionService]
        private ILogger<IdentityGraphApiMutation<TUser>> _logger = null;

        [InjectionService]
        private IClockService _clock = null;

        [InjectionService]
        private IEmailService _emailService = null;

        [InjectionService]
        private ServiceFactory _serviceFactory = null;

        [InjectionService]
        private IStringLocalizer<RegisterApiModelResource> _localizer = null;

        [InjectionService]
        private IUserStore<TUser> _userStore = null;

        [InjectionService]
        private SignInManager<TUser> _signInManager = null;


        private readonly IIdentityStoreIdentifierGenerator _identifierGenerator = null;
        private readonly UserManager<TUser> _userManager;


        public IdentityGraphApiMutation(IInjectionService injectionService)
        {
            injectionService.Inject(this);

            _identifierGenerator = _serviceFactory.GetIdentityStoreIdentifierGeneratorByUser<TUser>();
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
                    var userId = await _identifierGenerator.GenerateUserIdAsync().ConfigureAndResultAsync();
                    await user.SetIdAsync(userId).ConfigureAndWaitAsync();

                    var result = await _userManager.CreateUserByEmail<IdentityGraphApiMutation<TUser>, TUser>(_userStore,
                        _clock, user, model.Email, model.Password).ConfigureAndResultAsync();

                    if (result.Succeeded)
                    {
                        model.Message = "User created a new account with password.";
                        model.UserId = userId.ToString();

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
