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
using System.Linq;
using System.Text.Encodings.Web;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using AspNetCore.Identity.Api.Models;
    using AspNetCore.Identity.Api.Resources;
    using AspNetCore.Identity.Api.Types;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using Extensions;
    using Extensions.Core.Combiners;
    using Extensions.Core.Localizers;
    using Extensions.Core.Services;
    using Extensions.Data.Stores;
    using Extensions.Network.Services;

    /// <summary>
    /// 身份图形 API 变化。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityGraphApiMutation<TUser, TGenId, TCreatedBy> : GraphApiMutationBase
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        [InjectionService]
        private IClockService _clockService = null;

        [InjectionService]
        private IEmailService _emailService = null;

        [InjectionService]
        private ServiceFactory _serviceFactory = null;

        [InjectionService]
        private IEnhancedStringLocalizer<RegisterModelResource> _localizer = null;

        [InjectionService]
        private IUserStore<TUser> _userStore = null;

        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        [InjectionService]
        private IdentityApiBuilderDependency _dependency = null;


        private readonly IIdentityStoreIdentityGenerator<TGenId> _identityGenerator = null;
        private readonly UserManager<TUser> _userManager;


        /// <summary>
        /// 构造一个身份图形 API 变化。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityGraphApiMutation(IInjectionService injectionService, ILoggerFactory loggerFactory)
            : base(injectionService, loggerFactory)
        {
            _identityGenerator = (IIdentityStoreIdentityGenerator<TGenId>)_serviceFactory
                .GetRequiredService<IStoreIdentityGenerator>();

            _userManager = _signInManager.UserManager;

            Name = nameof(ISchema.Mutation);

            AddLoginTypeField();
            AddRegisterTypeField();
        }


        private void AddLoginTypeField()
        {
            // "query": "mutation($user:LoginInput!) { login(user: $user) { succeeded isLockedOut isNotAllowed requiresTwoFactor }}",
            // "variables": {
            //     "user": {
            //         "username": "",
            //         "password": "",
            //         "rememberMe": true
            //     }
            // }
            FieldAsync<SignInResultType>
            (
                name: "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<LoginModel>("user");

                    var result = await _signInManager.PasswordSignInAsync(model.Username,
                        model.Password, model.RememberMe, lockoutOnFailure: false)
                    .ConfigureAwait();

                    return result;
                }
            );
        }

        private void AddRegisterTypeField()
        {
            // "query": "mutation($user:RegisterInput!) { register(user: $user) { succeeded errors { code description } }}",
            // "variables": {
            //     "user": {
            //         "username": "",
            //         "password": "",
            //         "confirmEmailUrl": ""
            //     }
            // }
            FieldAsync<IdentityResultType>
            (
                name: "register",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RegisterInputType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<RegisterModel>("user");

                    var user = typeof(TUser).EnsureCreate<TUser>();
                    user.Id = await _identityGenerator.GenerateUserIdAsync().ConfigureAwait();

                    var result = await _userManager.CreateUserByEmail<TUser, TCreatedBy>(_userStore,
                        _clockService, user, model.Username, model.Password).ConfigureAwait();

                    if (result.Succeeded)
                    {
                        var userIdString = user.Id.ToString();

                        // 确认邮件
                        if (_dependency.SupportConfirmEmail)
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait();

                            var combiner = model.ConfirmEmailUrl.AsUriCombinerCore();
                            combiner.ChangeQueries(queries =>
                            {
                                queries.AddOrUpdate("userId", userIdString, (key, value) => userIdString);
                                queries.AddOrUpdate("code", code, (key, value) => code);
                            });
                            var externalLink = HtmlEncoder.Default.Encode(combiner.ToString());

                            await _emailService.SendAsync(model.Username,
                                _localizer.GetString(r => r.ConfirmYourEmail)?.Value,
                                _localizer.GetString(r => r.ConfirmYourEmailFormat, externalLink)?.Value)
                            .ConfigureAwait();
                            //await userStore.GetUserEmailStore(signInManager).SetEmailAsync(user, model.Email, default);
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait();
                        Logger.LogInformation(3, "User created a new account with password.");
                    }

                    return new IdentityResultModel
                    {
                        Succeeded = result.Succeeded,
                        Errors = result.Errors.ToList()
                    };
                }
            );
        }

    }
}
