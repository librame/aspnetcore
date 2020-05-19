#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
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
    /// 身份角色类型。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    public class IdentityRoleType<TRole> : ObjectGraphType<TRole>
        where TRole : class
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityRoleType{TRole}"/> 实例。
        /// </summary>
        public IdentityRoleType()
        {
            Field(f => nameof(DefaultIdentityRole<Guid>.Name), nullable: true);
        }

    }
}
