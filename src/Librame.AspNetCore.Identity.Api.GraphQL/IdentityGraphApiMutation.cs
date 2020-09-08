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
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityGraphApiMutation<TRole, TUser, TGenId, TCreatedBy> : GraphApiMutationBase
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
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
        private IEnhancedStringLocalizer<UserModelResource> _localizer = null;

        [InjectionService]
        private IUserStore<TUser> _userStore = null;

        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        [InjectionService]
        private RoleManager<TRole> _roleManager = null;

        [InjectionService]
        private IdentityApiBuilderDependency _dependency = null;

        [InjectionService]
        private IdentityErrorDescriber _errorDescriber = null;


        private readonly IIdentityStoreIdentificationGenerator<TGenId> _identityGenerator;
        private readonly UserManager<TUser> _userManager;


        /// <summary>
        /// 构造一个身份图形 API 变化。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityGraphApiMutation(IInjectionService injectionService, ILoggerFactory loggerFactory)
            : base(injectionService, loggerFactory)
        {
            _identityGenerator = (IIdentityStoreIdentificationGenerator<TGenId>)_serviceFactory
                .GetRequiredService<IStoreIdentificationGenerator>();

            _userManager = _signInManager.UserManager;

            AddRoleInputTypeFields();
            AddUserInputTypeFields();
        }


        private void AddRoleInputTypeFields()
        {
            // "query": "mutation($role:RoleInput!) { addRoleClaims(role: $role) { id... name [roleClaims[{...}]] }}",
            // "variables": {
            //     "role": {
            //         "name": "",
            //         "roleClaims": null/[{claimType: "", claimValue: ""}]
            //     }
            // }
            FieldAsync<IdentityResultModelType>
            (
                name: "addRoleClaims",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RoleInputModelType>> { Name = "role" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<RoleModel>("role");

                    IdentityResult result = null;

                    // 向已存在的角色添加声明集合
                    var role = await _roleManager.FindByNameAsync(model.Name).ConfigureAwait();
                    if (role.IsNull())
                    {
                        // 支持添加不存在的新角色
                        role = typeof(TRole).EnsureCreate<TRole>();

                        role.Name = model.Name;
                        role.NormalizedName = _roleManager.NormalizeKey(role.Name);

                        role.Id = await _identityGenerator.GenerateRoleIdAsync().ConfigureAwait();

                        await role.PopulateCreationAsync(_clockService).ConfigureAwait();

                        result = await _roleManager.CreateAsync(role).ConfigureAwait();
                    }

                    if (model.RoleClaims.IsNotEmpty())
                    {
                        foreach (var claim in model.RoleClaims)
                        {
                            result = await _roleManager.AddClaimAsync(role, claim.ToClaim()).ConfigureAwait();
                        }
                    }

                    return (result ?? IdentityResult.Success).ToModel();
                }
            );
        }

        private void AddUserInputTypeFields()
        {
            // "query": "mutation($user:UserInput!) { login(user: $user) { succeeded isLockedOut isNotAllowed requiresTwoFactor }}",
            // "variables": {
            //     "user": {
            //         "userName": "",
            //         "password": "",
            //         "rememberMe": true
            //     }
            // }
            FieldAsync<SignInResultModelType>
            (
                name: "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputModelType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<UserModel>("user");

                    var result = await _signInManager.PasswordSignInAsync(model.UserName,
                        model.Password, model.RememberMe, lockoutOnFailure: false)
                    .ConfigureAwait();

                    return new SignInResultModel(result, model);
                }
            );

            // "query": "mutation($user:UserInput!) { register(user: $user) { succeeded errors { code description } }}",
            // "variables": {
            //     "user": {
            //         "userName": "",
            //         "password": "",
            //         "emailConfirmationUrl": ""
            //     }
            // }
            FieldAsync<IdentityResultModelType>
            (
                name: "register",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputModelType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<UserModel>("user");

                    var user = typeof(TUser).EnsureCreate<TUser>();
                    user.Id = await _identityGenerator.GenerateUserIdAsync().ConfigureAwait();

                    var result = await _userManager.CreateUserByEmail<TUser, TCreatedBy>(_userStore,
                        _clockService, user, model.UserName, model.Password).ConfigureAwait();

                    if (result.Succeeded)
                    {
                        var userIdString = user.Id.ToString();

                        // 确认邮件
                        if (_dependency.SupportConfirmEmail && model.EmailConfirmationUrl.IsNotEmpty())
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait();

                            var combiner = model.EmailConfirmationUrl.AsUriCombinerCore();
                            combiner.ChangeQueries(queries =>
                            {
                                queries.AddOrUpdate("userId", userIdString, (key, value) => userIdString);
                                queries.AddOrUpdate("code", code, (key, value) => code);
                            });
                            var externalLink = HtmlEncoder.Default.Encode(combiner.ToString());

                            await _emailService.SendAsync(model.UserName,
                                _localizer.GetString(r => r.ConfirmYourEmail)?.Value,
                                _localizer.GetString(r => r.ConfirmYourEmailFormat, externalLink)?.Value)
                            .ConfigureAwait();
                            //await userStore.GetUserEmailStore(signInManager).SetEmailAsync(user, model.Email, default);
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait();
                        Logger.LogInformation(3, "User created a new account with password.");
                    }

                    return result.ToModel();
                }
            );

            // "query": "mutation($user:UserInput!) { addUserClaims(user: $user) { id... name [userClaims[{...}]] }}",
            // "variables": {
            //     "user": {
            //         "name": "",
            //         "userClaims": null/[{claimType: "", claimValue: ""}]
            //     }
            // }
            FieldAsync<IdentityResultModelType>
            (
                name: "addUserClaims",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputModelType>> { Name = "user" }
                ),
                resolve: async context =>
                {
                    var model = context.GetArgument<UserModel>("user");

                    // 向已存在的用户添加声明集合
                    var user = await _userManager.FindByNameAsync(model.UserName).ConfigureAwait();
                    if (user.IsNull())
                    {
                        // 不支持添加不存在的新用户，新用户只能通过用户注册
                        return IdentityResult.Failed(_errorDescriber.InvalidUserName(model.UserName));
                    }

                    IdentityResult result = null;

                    if (model.UserClaims.IsNotEmpty())
                    {
                        foreach (var claim in model.UserClaims)
                        {
                            result = await _userManager.AddClaimAsync(user, claim.ToClaim()).ConfigureAwait();
                        }
                    }

                    return (result ?? IdentityResult.Success).ToModel();
                }
            );
        }

    }
}
