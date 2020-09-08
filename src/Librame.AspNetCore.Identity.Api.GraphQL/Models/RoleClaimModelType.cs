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
    /// 角色声明模型类型。
    /// </summary>
    public class RoleClaimModelType : CreationIdentifierModelTypeBase<RoleClaimModel>
    {
        /// <summary>
        /// 构造一个 <see cref="RoleClaimModelType"/>。
        /// </summary>
        public RoleClaimModelType()
        {
            Field(f => f.ClaimType);
            Field(f => f.ClaimValue);

            Field(f => f.Role, type: typeof(RoleModelType), nullable: true);
        }

    }
}
