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
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using AspNetCore.Identity.Api.Types;
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using Extensions;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;

    /// <summary>
    /// 身份图形 API 查询。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityGraphApiQuery<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken, TGenId, TCreatedBy>
        : GraphApiQueryBase
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TRoleClaim : DefaultIdentityRoleClaim<TGenId, TCreatedBy>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TUserClaim : DefaultIdentityUserClaim<TGenId, TCreatedBy>
        where TUserLogin : DefaultIdentityUserLogin<TGenId, TCreatedBy>
        where TUserRole : DefaultIdentityUserRole<TGenId, TCreatedBy>
        where TUserToken : DefaultIdentityUserToken<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份图形 API 查询。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        /// <param name="dependency">给定的 <see cref="IdentityApiBuilderDependency"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityGraphApiQuery(IAccessor accessor,
            IdentityApiBuilderDependency dependency,
            ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            IdentityAccessor = accessor.CastTo<IAccessor,
                IIdentityAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken>>(nameof(accessor));

            Dependency = dependency.NotNull(nameof(dependency));

            AddRoleTypeFields();
            AddRoleClaimTypeFields();
            AddUserTypeFields();
            AddUserClaimTypeFields();
            AddUserLoginTypeFields();
            AddUserRoleTypeFields();
            AddUserTokenTypeFields();
        }


        /// <summary>
        /// 构建器依赖。
        /// </summary>
        protected IdentityApiBuilderDependency Dependency { get; }

        /// <summary>
        /// 身份访问器。
        /// </summary>
        protected IIdentityAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken> IdentityAccessor { get; }


        private void AddRoleTypeFields()
        {
            if (Dependency.SupportsQueryRoles)
            {
                // { pageRoles(index: 1, size: 10) { id name createdTime createdBy } }
                Field<ListGraphType<RoleType<TRole, TGenId, TCreatedBy>>>
                (
                    name: "pageRoles",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");

                        if (index > 0 && size > 0)
                        {
                            return IdentityAccessor.Roles.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size);
                        }

                        return IdentityAccessor.Roles.ToList();
                    }
                );
            }

            // { roleId(id: "") { id name createdTime createdBy } }
            Field<RoleType<TRole, TGenId, TCreatedBy>>
            (
                name: "roleId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TGenId>("id");
                    return IdentityAccessor.Roles.FirstOrDefault(p => p.Id.Equals(id));
                }
            );

            // { roleName(name: "") { id name createdTime createdBy } }
            Field<RoleType<TRole, TGenId, TCreatedBy>>
            (
                name: "roleName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return IdentityAccessor.Roles.FirstOrDefault(p => p.Name == name);
                }
            );
        }

        private void AddRoleClaimTypeFields()
        {
            // { roleClaimId(id: "") { id roleId claimType claimValue createdTime createdBy } }
            Field<RoleClaimType<TRoleClaim, TGenId, TCreatedBy>>
            (
                name: "roleClaimId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return IdentityAccessor.RoleClaims.FirstOrDefault(p => p.Id == id);
                }
            );

            // { roleClaimRoleId(roleId: "") { id roleId claimType claimValue createdTime createdBy } }
            Field<RoleClaimType<TRoleClaim, TGenId, TCreatedBy>>
            (
                name: "roleClaimRoleId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "roleId" }
                ),
                resolve: context =>
                {
                    var roleId = context.GetArgument<TGenId>("roleId");
                    return IdentityAccessor.RoleClaims.FirstOrDefault(p => p.RoleId.Equals(roleId));
                }
            );
        }

        private void AddUserTypeFields()
        {
            if (Dependency.SupportsQueryUsers)
            {
                // { pageUsers(index: 1, size: 10) { id userName createdTime createdBy } }
                Field<ListGraphType<UserType<TUser, TGenId, TCreatedBy>>>
                (
                    name: "pageUsers",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");

                        if (index > 0 && size > 0)
                        {
                            return IdentityAccessor.Users.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size);
                        }

                        return IdentityAccessor.Users.ToList();
                    }
                );
            }

            // { userId(id: "") { id userName createdTime createdBy } }
            Field<UserType<TUser, TGenId, TCreatedBy>>
            (
                name: "userId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TGenId>("id");
                    return IdentityAccessor.Users.FirstOrDefault(p => p.Id.Equals(id));
                }
            );

            // { userName(name: "") { id userName createdTime createdBy } }
            Field<UserType<TUser, TGenId, TCreatedBy>>
            (
                name: "userName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return IdentityAccessor.Users.FirstOrDefault(p => p.UserName == name);
                }
            );
        }

        private void AddUserClaimTypeFields()
        {
            // { userClaimId(id: "") { id userId claimType claimValue createdTime createdBy } }
            Field<UserClaimType<TUserClaim, TGenId, TCreatedBy>>
            (
                name: "userClaimId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return IdentityAccessor.UserClaims.FirstOrDefault(p => p.Id == id);
                }
            );

            // { userClaimUserId(userId: "") { id userId claimType claimValue createdTime createdBy } }
            Field<UserClaimType<TUserClaim, TGenId, TCreatedBy>>
            (
                name: "userClaimUserId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }
                ),
                resolve: context =>
                {
                    var userId = context.GetArgument<TGenId>("userId");
                    return IdentityAccessor.UserClaims.FirstOrDefault(p => p.UserId.Equals(userId));
                }
            );
        }

        private void AddUserLoginTypeFields()
        {
            if (Dependency.SupportsQueryUserLogins)
            {
                // { pageUserLogins(index: 1, size: 10) { userId loginProvider providerDisplayName providerKey createdTime createdBy } }
                Field<ListGraphType<UserLoginType<TUserLogin, TGenId, TCreatedBy>>>
                (
                    name: "pageUserLogins",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");

                        if (index > 0 && size > 0)
                        {
                            return IdentityAccessor.UserLogins.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size);
                        }

                        return IdentityAccessor.UserLogins.ToList();
                    }
                );
            }
        }

        private void AddUserRoleTypeFields()
        {
            // { userRoles(userId: "") { id name createdTime createdBy } }
            Field<ListGraphType<RoleType<TRole, TGenId, TCreatedBy>>>
            (
                name: "userRoles",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }
                ),
                resolve: context =>
                {
                    var userId = context.GetArgument<TGenId>("userId");
                    return IdentityAccessor.UserRoles
                        .Where(p => p.UserId.Equals(userId))
                        .Select(s => IdentityAccessor.Roles.FirstOrDefault(p => p.Id.Equals(s.RoleId)))
                        .ToList();
                }
            );
        }

        private void AddUserTokenTypeFields()
        {
            if (Dependency.SupportsQueryUserTokens)
            {
                // { pageUserTokens(index: 1, size: 10) { userId loginProvider name value createdTime createdBy } }
                Field<ListGraphType<UserTokenType<TUserToken, TGenId, TCreatedBy>>>
                (
                    name: "pageUserTokens",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");

                        if (index > 0 && size > 0)
                        {
                            return IdentityAccessor.UserTokens.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size);
                        }

                        return IdentityAccessor.UserTokens.ToList();
                    }
                );
            }
        }

    }
}
