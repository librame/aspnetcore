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
    /// 用户登入类型。
    /// </summary>
    /// <typeparam name="TUserLogin">指定的用户登入类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class UserLoginType<TUserLogin, TUserId, TCreatedBy> : ApiTypeBase<TUserLogin>
        where TUserLogin : DefaultIdentityUserLogin<TUserId, TCreatedBy>
        where TUserId : IEquatable<TUserId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个用户登入类型。
        /// </summary>
        public UserLoginType()
        {
            Field(f => f.UserId, type: typeof(IdGraphType));
            Field(f => f.LoginProvider);
            Field(f => f.ProviderDisplayName);
            Field(f => f.ProviderKey);
            Field(f => f.CreatedTime);
            Field(f => f.CreatedBy, type: typeof(IdGraphType));
        }

    }
}
