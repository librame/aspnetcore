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

namespace Librame.AspNetCore.Identity.Api.StoreTypes
{
    using Stores;

    /// <summary>
    /// 身份用户类型。
    /// </summary>
    public class IdentityUserType : ObjectGraphType<DefaultIdentityUser<string>>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityUserType"/> 实例。
        /// </summary>
        public IdentityUserType()
        {
            // 支持三选一查询
            Field(f => f.NormalizedUserName, nullable: true);
            Field(f => f.Email, nullable: true);
            Field(f => f.PhoneNumber, nullable: true);
        }
    }
}
