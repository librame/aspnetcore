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
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 内部身份 Graph API 查询。
    /// </summary>
    internal class InternalIdentityGraphApiQuery : ObjectGraphType, IGraphApiQuery
    {
        private readonly IStoreHub<IdentityDbContextAccessor> _stores;


        /// <summary>
        /// 构造一个 <see cref="InternalIdentityGraphApiQuery"/> 实例。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub{IdentityDbContextAccessor}"/></param>
        public InternalIdentityGraphApiQuery(IStoreHub<IdentityDbContextAccessor> stores)
        {
            _stores = stores.NotNull(nameof(stores));

            Name = nameof(GraphQL.Types.ISchema.Query);

            AddIdentityUserTypeFields();
        }


        private void AddIdentityUserTypeFields()
        {
            var prefixName = "user";

            Field<IdentityUserType>
            (
                name: $"{prefixName}Name",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return _stores.Accessor.Users.FirstOrDefault(p => p.NormalizedUserName == name);
                }
            );

            Field<IdentityUserType>
            (
                name: $"{prefixName}Email",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "email" }
                ),
                resolve: context =>
                {
                    var email = context.GetArgument<string>("email");
                    return _stores.Accessor.Users.FirstOrDefault(p => p.NormalizedEmail == email);
                }
            );

            Field<IdentityUserType>
            (
                name: $"{prefixName}Phone",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "phone" }
                ),
                resolve: context =>
                {
                    var phone = context.GetArgument<string>("phone");
                    return _stores.Accessor.Users.FirstOrDefault(p => p.PhoneNumber == phone);
                }
            );
        }

    }
}
