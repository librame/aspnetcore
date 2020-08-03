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
    /// 用户类型。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class UserType<TUser, TGenId, TCreatedBy> : ApiTypeBase<TUser>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个用户类型。
        /// </summary>
        public UserType()
        {
            Field(f => f.Id, type: typeof(IdGraphType));
            Field(f => f.UserName);
            Field(f => f.NormalizedUserName);
            Field(f => f.EmailConfirmed);
            Field(f => f.Email);
            Field(f => f.PhoneNumber);
            Field(f => f.PhoneNumberConfirmed);
            Field(f => f.LockoutEnabled);
            //Field(f => f.LockoutEnd);
            Field(f => f.TwoFactorEnabled);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy, type: typeof(IdGraphType));
        }

    }
}
