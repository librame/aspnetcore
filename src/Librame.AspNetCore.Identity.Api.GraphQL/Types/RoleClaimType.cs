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
using System;

namespace Librame.AspNetCore.Identity.Api.Types
{
    using AspNetCore.Api.Types;
    using AspNetCore.Identity.Stores;

    /// <summary>
    /// 角色声明类型。
    /// </summary>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TRoleId">指定的角色标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class RoleClaimType<TRoleClaim, TRoleId, TCreatedBy> : ApiTypeBase<TRoleClaim>
        where TRoleClaim : DefaultIdentityRoleClaim<TRoleId, TCreatedBy>
        where TRoleId : IEquatable<TRoleId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个角色声明类型。
        /// </summary>
        public RoleClaimType()
        {
            Field(f => f.Id, type: typeof(IdGraphType));
            Field(f => f.RoleId, type: typeof(IdGraphType));
            Field(f => f.ClaimType);
            Field(f => f.ClaimValue);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy, type: typeof(IdGraphType));
        }

    }
}
