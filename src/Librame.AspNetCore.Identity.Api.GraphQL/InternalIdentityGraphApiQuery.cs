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
    /// 内部身份 Graph API 查询。
    /// </summary>
    internal class InternalIdentityGraphApiQuery : ObjectGraphType, IGraphApiQuery
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityGraphApiQuery"/> 实例。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub{IdentityDbContextAccessor}"/></param>
        public InternalIdentityGraphApiQuery(IStoreHub<IdentityDbContextAccessor> stores)
        {
            Name = nameof(GraphQL.Types.ISchema.Query);

            Field<IdentityUserQueryType>
            (
                name: "user",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return stores.Accessor.Users.FirstOrDefault(p => p.UserName == name);
                }
            );
        }

    }
}
