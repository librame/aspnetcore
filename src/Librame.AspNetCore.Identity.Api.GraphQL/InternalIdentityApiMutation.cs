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

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;

    /// <summary>
    /// 内部身份 API 变化。
    /// </summary>
    internal class InternalIdentityApiMutation : ObjectGraphType, IApiMutation
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityApiMutation"/> 实例。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{DefaultIdentityUser}"/></param>
        public InternalIdentityApiMutation(SignInManager<DefaultIdentityUser> signInManager)
        {
            Field<IdentityUserGraphType>
            (
                name: "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdentityUserGraphType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var user = context.GetArgument<DefaultIdentityUser>("user");
                    var result = signInManager.UserManager.CreateAsync(user).Result;
                    return user;
                }
            );
        }

    }
}
