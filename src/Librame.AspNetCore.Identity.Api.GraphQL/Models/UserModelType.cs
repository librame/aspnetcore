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

namespace Librame.AspNetCore.Identity.Api.Types
{
    using AspNetCore.Api.Models;
    using AspNetCore.Identity.Api.Models;

    /// <summary>
    /// 用户模型类型。
    /// </summary>
    public class UserModelType : CreationIdentifierModelTypeBase<UserModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UserModelType"/>。
        /// </summary>
        public UserModelType()
            : base()
        {
            Field(f => f.UserName);
            Field(f => f.Email);
            Field(f => f.EmailConfirmed);
            Field(f => f.PhoneNumber);
            Field(f => f.PhoneNumberConfirmed);
            Field(f => f.AccessFailedCount);
            Field(f => f.LockoutEnabled);
            //Field(f => f.LockoutEnd);
            Field(f => f.Password);
            Field(f => f.RememberMe);
            Field(f => f.EmailConfirmationUrl);

            Field(f => f.Roles, type: typeof(ListGraphType<RoleModelType>), nullable: true);
            Field(f => f.UserClaims, type: typeof(ListGraphType<UserClaimModelType>), nullable: true);
            Field(f => f.UserLogins, type: typeof(ListGraphType<UserLoginModelType>), nullable: true);
            Field(f => f.UserTokens, type: typeof(ListGraphType<UserTokenModelType>), nullable: true);
        }

    }
}
