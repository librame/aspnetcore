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
using System.Linq;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using Extensions.Data;

    /// <summary>
    /// 内部身份 Graph API 查询。
    /// </summary>
    internal class InternalIdentityGraphApiQuery : ObjectGraphType, IGraphApiQuery
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityGraphApiQuery"/> 实例。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub{IdentityDbContextAccessor}"/></param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{DefaultIdentityUser}"/></param>
        /// <param name="logger">给定的 <see cref="ILogger{InternalIdentityGraphApiMutation}"/>。</param>
        public InternalIdentityGraphApiQuery(IStoreHub<IdentityDbContextAccessor> stores,
            SignInManager<DefaultIdentityUser> signInManager,
            ILogger<InternalIdentityGraphApiQuery> logger)
        {
            Name = nameof(GraphQL.Types.ISchema.Query);

            Field<IdentityUserQueryType>
            (
                name: "user",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return stores.Accessor.Users.FirstOrDefault(p => p.UserName == name);
                }
            );

            Field<LoginInputType>
            (
                name: "login",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "user" }
                    new QueryArgument<StringGraphType> { Name = "name" },
                    new QueryArgument<StringGraphType> { Name = "pwd" }
                ),
                resolve: context =>
                {
                    //var model = context.GetArgument<LoginApiModel>("user");
                    var model = new LoginApiModel();
                    model.Name = context.GetArgument<string>("name");
                    model.Password = context.GetArgument<string>("pwd");

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
        }

    }
}
