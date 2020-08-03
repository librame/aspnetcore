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
    /// 角色类型。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class RoleType<TRole, TGenId, TCreatedBy> : ApiTypeBase<TRole>
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个用户类型。
        /// </summary>
        public RoleType()
        {
            Field(f => f.Id, type: typeof(IdGraphType));
            Field(f => f.Name);
            Field(f => f.NormalizedName);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy, type: typeof(IdGraphType));
        }

    }
}
