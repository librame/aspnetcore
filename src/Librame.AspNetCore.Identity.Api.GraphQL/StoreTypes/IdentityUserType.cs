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

namespace Librame.AspNetCore.Identity.Api.StoreTypes
{
    using Stores;

    /// <summary>
    /// 身份用户类型。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public class IdentityUserType<TUser> : ObjectGraphType<TUser>
        where TUser : class
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityUserType{TUser}"/> 实例。
        /// </summary>
        public IdentityUserType()
        {
            // 为保障隐私，仅支持用户名查询
            Field(f => nameof(DefaultIdentityUser<Guid, Guid>.UserName), nullable: true);
        }

    }
}
