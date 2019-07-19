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
    /// 内部身份 Graph API 变化。
    /// </summary>
    internal class InternalIdentityGraphApiMutation : ObjectGraphType, IGraphApiMutation
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityGraphApiMutation"/> 实例。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{DefaultIdentityUser}"/></param>
        /// <param name="identifierService">给定的 <see cref="IIdentityIdentifierService"/>。</param>
        public InternalIdentityGraphApiMutation(SignInManager<DefaultIdentityUser> signInManager,
            IIdentityIdentifierService identifierService)
        {
            Field<IdentityUserMutationType>
            (
                name: "addUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdentityUserMutationType>> { Name = "user" }
                ),
                resolve: context =>
                {
                    var user = context.GetArgument<DefaultIdentityUser>("user");
                    user.Id = identifierService.GetUserIdAsync().Result;

                    var result = signInManager.UserManager.CreateAsync(user, "Password!123456").Result;
                    return user;
                }
            );
        }

    }
}
