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

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 角色输入模型类型。
    /// </summary>
    public class RoleInputModelType : InputModelTypeBase<RoleModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UserInputModelType"/>。
        /// </summary>
        public RoleInputModelType()
            : base()
        {
            Field(f => f.Name);

            Field(f => f.RoleClaims, type: typeof(ListGraphType<RoleClaimInputModelType>), nullable: true);
        }

    }
}
