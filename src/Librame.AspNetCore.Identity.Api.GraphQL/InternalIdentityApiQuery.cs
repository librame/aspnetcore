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
using System.Linq;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using Extensions.Data;

    /// <summary>
    /// 内部身份 API 查询。
    /// </summary>
    internal class InternalIdentityApiQuery : ObjectGraphType, IApiQuery
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityApiQuery"/> 实例。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub{IdentityDbContextAccessor}"/></param>
        public InternalIdentityApiQuery(IStoreHub<IdentityDbContextAccessor> stores)
        {
            Field<IdentityUserGraphType>
            (
                name: "user",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "name" }),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return stores.Accessor.Users.FirstOrDefault(p => p.UserName == name);
                }
            );
        }

    }
}
