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
    /// 用户声明类型。
    /// </summary>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class UserClaimType<TUserClaim, TUserId, TCreatedBy> : ApiTypeBase<TUserClaim>
        where TUserClaim : DefaultIdentityUserClaim<TUserId, TCreatedBy>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个用户声明类型。
        /// </summary>
        public UserClaimType()
        {
            Field(f => f.Id, type: typeof(IdGraphType));
            Field(f => f.UserId, type: typeof(IdGraphType));
            Field(f => f.ClaimType);
            Field(f => f.ClaimValue);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy, type: typeof(IdGraphType));
        }

    }
}
