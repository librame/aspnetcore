#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 角色声明输入模型类型。
    /// </summary>
    public class RoleClaimInputModelType : InputModelTypeBase<RoleClaimModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UserInputModelType"/>。
        /// </summary>
        public RoleClaimInputModelType()
            : base()
        {
            Field(f => f.ClaimType);
            Field(f => f.ClaimValue);
        }

    }
}
