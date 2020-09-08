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
    /// 角色模型类型。
    /// </summary>
    public class RoleModelType : CreationIdentifierModelTypeBase<RoleModel>
    {
        /// <summary>
        /// 构造一个 <see cref="RoleModelType"/>。
        /// </summary>
        public RoleModelType()
        {
            Field(f => f.Name);

            Field(f => f.RoleClaims, type: typeof(ListGraphType<RoleClaimModelType>), nullable: true);
        }

    }
}
