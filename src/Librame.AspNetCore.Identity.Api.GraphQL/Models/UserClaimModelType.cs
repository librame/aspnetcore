#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Types
{
    using AspNetCore.Api.Models;
    using AspNetCore.Identity.Api.Models;

    /// <summary>
    /// 用户声明模型类型。
    /// </summary>
    public class UserClaimModelType : CreationIdentifierModelTypeBase<UserClaimModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UserClaimModelType"/>。
        /// </summary>
        public UserClaimModelType()
        {
            Field(f => f.ClaimType);
            Field(f => f.ClaimValue);

            Field(f => f.User, type: typeof(UserModelType), nullable: true);
        }

    }
}
