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
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using AspNetCore.Identity.Api.Models;
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
                // { pageRoles(index: 1, size: 10, includeRoleClaims: false, search: "") { id... name [roleClaims[{...}]] } }
                Field<ListGraphType<RoleModelType>>
                (
                    name: "pageRoles",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" },
                        new QueryArgument<BooleanGraphType> { Name = "includeRoleClaims" },
                        new QueryArgument<StringGraphType> { Name = "search" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");
                        var includeRoleClaims = context.GetArgument<bool>("includeRoleClaims");
                        var search = context.GetArgument<string>("search");

                        var query = IdentityAccessor.Roles.AsQueryable();

                        if (search.IsNotEmpty())
                            query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                        if (index > 0 && size > 0)
                        {
                            return query.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size).SelectPaging(s => ToRoleModel(s, includeRoleClaims));
                        }

                        return query.ToList().Select(s => ToRoleModel(s, includeRoleClaims));
                    }
                );
            }

            // { roleId(id: ?, includeRoleClaims) { id... name [roleClaims[{...}]] } }
            Field<RoleModelType>
            (
                name: "roleId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<BooleanGraphType> { Name = "includeRoleClaims" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TGenId>("id");
                    var includeRoleClaims = context.GetArgument<bool>("includeRoleClaims");

                    var role = IdentityAccessor.Roles.Find(id);
                    return ToRoleModel(role, includeRoleClaims);
                }
            );

            // { roleName(name: "", includeRoleClaims) { id... name [roleClaims[{...}]] } }
            Field<RoleModelType>
            (
                name: "roleName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<BooleanGraphType> { Name = "includeRoleClaims" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var includeRoleClaims = context.GetArgument<bool>("includeRoleClaims");

                    var role = IdentityAccessor.Roles.FirstOrDefault(p => p.Name == name);
                    return ToRoleModel(role, includeRoleClaims);
                }
            );
        }

        private void AddRoleClaimTypeFields()
        {
            // { pageRoleClaims(index: 1, size: 10, includeRole: false, search: "") { id... claimType claimValue [role{...}] } }
            Field<ListGraphType<RoleClaimModelType>>
            (
                name: "pageRoleClaims",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<BooleanGraphType> { Name = "includeRole" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var includeRole = context.GetArgument<bool>("includeRole");
                    var search = context.GetArgument<string>("search");

                    var query = IdentityAccessor.RoleClaims.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.ClaimType.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(s => ToRoleClaimModel(s, includeRole));
                    }

                    return query.ToList().Select(s => ToRoleClaimModel(s, includeRole));
                }
            );

            // { roleClaimId(id: n, includeRole: false) { id... claimType claimValue [role{...}] } }
            Field<RoleClaimModelType>
            (
                name: "roleClaimId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<BooleanGraphType> { Name = "includeRole" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var includeRole = context.GetArgument<bool>("includeRole");

                    var roleClaim = IdentityAccessor.RoleClaims.Find(id);
                    return ToRoleClaimModel(roleClaim, includeRole);
                }
            );
        }

        private void AddUserTypeFields()
        {
            if (Dependency.SupportsQueryUsers)
            {
                // { pageUsers(index: 1, size: 10, includeRoles: false, includeUserClaims: false, includeUserLogins: false, includeUserTokens: false, search: "") { id... name [roles[{...}]] [userClaims[{...}]] [userLogins[{...}]] [userTokens[{...}]] } }
                Field<ListGraphType<UserModelType>>
                (
                    name: "pageUsers",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" },
                        new QueryArgument<BooleanGraphType> { Name = "includeRoles" },
                        new QueryArgument<BooleanGraphType> { Name = "includeUserClaims" },
                        new QueryArgument<BooleanGraphType> { Name = "includeUserLogins" },
                        new QueryArgument<BooleanGraphType> { Name = "includeUserTokens" },
                        new QueryArgument<StringGraphType> { Name = "search" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");
                        var includeRoles = context.GetArgument<bool>("includeRoles");
                        var includeUserClaims = context.GetArgument<bool>("includeUserClaims");
                        var includeUserLogins = context.GetArgument<bool>("includeUserLogins");
                        var includeUserTokens = context.GetArgument<bool>("includeUserTokens");
                        var search = context.GetArgument<string>("search");

                        var query = IdentityAccessor.Users.AsQueryable();

                        if (search.IsNotEmpty())
                            query = query.Where(p => p.UserName.Contains(search, StringComparison.InvariantCulture));

                        if (index > 0 && size > 0)
                        {
                            return query.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size).SelectPaging(s => ToUserModel(s, includeRoles, includeUserClaims, includeUserLogins, includeUserTokens));
                        }

                        return query.ToList().Select(s => ToUserModel(s, includeRoles, includeUserClaims, includeUserLogins, includeUserTokens));
                    }
                );
            }

            // { userId(id: ?, includeRoles: false, includeUserClaims: false, includeUserLogins: false, includeUserTokens: false) { id... name [roles[{...}]] [userClaims[{...}]] [userLogins[{...}]] [userTokens[{...}]] } }
            Field<UserModelType>
            (
                name: "userId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<BooleanGraphType> { Name = "includeRoles" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUserClaims" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUserLogins" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUserTokens" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<TGenId>("id");
                    var includeRoles = context.GetArgument<bool>("includeRoles");
                    var includeUserClaims = context.GetArgument<bool>("includeUserClaims");
                    var includeUserLogins = context.GetArgument<bool>("includeUserLogins");
                    var includeUserTokens = context.GetArgument<bool>("includeUserTokens");

                    var user = IdentityAccessor.Users.Find(id);
                    return ToUserModel(user, includeRoles, includeUserClaims, includeUserLogins, includeUserTokens);
                }
            );

            // { userName(name: "", includeRoles: false, includeUserClaims: false, includeUserLogins: false, includeUserTokens: false) { id... name [roles[{...}]] [userClaims[{...}]] [userLogins[{...}]] [userTokens[{...}]] } }
            Field<UserModelType>
            (
                name: "userName",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<BooleanGraphType> { Name = "includeRoles" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUserClaims" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUserLogins" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUserTokens" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var includeRoles = context.GetArgument<bool>("includeRoles");
                    var includeUserClaims = context.GetArgument<bool>("includeUserClaims");
                    var includeUserLogins = context.GetArgument<bool>("includeUserLogins");
                    var includeUserTokens = context.GetArgument<bool>("includeUserTokens");

                    var user = IdentityAccessor.Users.FirstOrDefault(p => p.UserName == name);
                    return ToUserModel(user, includeRoles, includeUserClaims, includeUserLogins, includeUserTokens);
                }
            );
        }

        private void AddUserClaimTypeFields()
        {
            // { pageUserClaims(index: 1, size: 10, includeUser: false, search: "") { id... claimType claimValue [user{...}] } }
            Field<ListGraphType<UserClaimModelType>>
            (
                name: "pageUserClaims",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "index" },
                    new QueryArgument<IntGraphType> { Name = "size" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUser" },
                    new QueryArgument<StringGraphType> { Name = "search" }
                ),
                resolve: context =>
                {
                    var index = context.GetArgument<int>("index");
                    var size = context.GetArgument<int>("size");
                    var includeUser = context.GetArgument<bool>("includeUser");
                    var search = context.GetArgument<string>("search");

                    var query = IdentityAccessor.UserClaims.AsQueryable();

                    if (search.IsNotEmpty())
                        query = query.Where(p => p.ClaimType.Contains(search, StringComparison.InvariantCulture));

                    if (index > 0 && size > 0)
                    {
                        return query.AsPagingByIndex(ordered =>
                        {
                            return ordered.OrderBy(k => k.CreatedTimeTicks);
                        },
                        index, size).SelectPaging(s => ToUserClaimModel(s, includeUser));
                    }

                    return query.ToList().Select(s => ToUserClaimModel(s, includeUser));
                }
            );

            // { userClaimId(id: n, includeUser: false) { id... claimType claimValue [user{...}] } }
            Field<UserClaimModelType>
            (
                name: "userClaimId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUser" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var includeUser = context.GetArgument<bool>("includeUser");

                    var userClaim = IdentityAccessor.UserClaims.Find(id);
                    return ToUserClaimModel(userClaim, includeUser);
                }
            );
        }

        private void AddUserLoginTypeFields()
        {
            if (Dependency.SupportsQueryUserLogins)
            {
                // { pageUserLogins(index: 1, size: 10, includeUser: false, search: "") { ... loginProvider providerKey providerDisplayName [user{...}] } }
                Field<ListGraphType<UserLoginModelType>>
                (
                    name: "pageUserLogins",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" },
                        new QueryArgument<BooleanGraphType> { Name = "includeUser" },
                        new QueryArgument<StringGraphType> { Name = "search" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");
                        var includeUser = context.GetArgument<bool>("includeUser");
                        var search = context.GetArgument<string>("search");

                        var query = IdentityAccessor.UserLogins.AsQueryable();

                        if (search.IsNotEmpty())
                            query = query.Where(p => p.ProviderDisplayName.Contains(search, StringComparison.InvariantCulture));

                        if (index > 0 && size > 0)
                        {
                            return query.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size).SelectPaging(s => ToUserLoginModel(s, includeUser));
                        }

                        return query.ToList().Select(s => ToUserLoginModel(s, includeUser));
                    }
                );
            }

            // { userLoginId(loginProvider: "", providerKey: "", includeUser: false) { ... loginProvider providerKey providerDisplayName [user{...}] } }
            Field<UserLoginModelType>
            (
                name: "userLoginId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "loginProvider" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "providerKey" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUser" }
                ),
                resolve: context =>
                {
                    var loginProvider = context.GetArgument<string>("loginProvider");
                    var providerKey = context.GetArgument<string>("providerKey");
                    var includeUser = context.GetArgument<bool>("includeUser");

                    var userLogin = IdentityAccessor.UserLogins.FirstOrDefault(p
                        => p.LoginProvider == loginProvider && p.ProviderKey == providerKey);

                    return ToUserLoginModel(userLogin, includeUser);
                }
            );
        }

        private void AddUserTokenTypeFields()
        {
            if (Dependency.SupportsQueryUserTokens)
            {
                // { pageUserTokens(index: 1, size: 10, includeUser: false, search: "") { ... loginProvider name value [user{...}] } }
                Field<ListGraphType<UserTokenModelType>>
                (
                    name: "pageUserTokens",
                    arguments: new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "index" },
                        new QueryArgument<IntGraphType> { Name = "size" },
                        new QueryArgument<BooleanGraphType> { Name = "includeUser" },
                        new QueryArgument<StringGraphType> { Name = "search" }
                    ),
                    resolve: context =>
                    {
                        var index = context.GetArgument<int>("index");
                        var size = context.GetArgument<int>("size");
                        var includeUser = context.GetArgument<bool>("includeUser");
                        var search = context.GetArgument<string>("search");

                        var query = IdentityAccessor.UserTokens.AsQueryable();

                        if (search.IsNotEmpty())
                            query = query.Where(p => p.Name.Contains(search, StringComparison.InvariantCulture));

                        if (index > 0 && size > 0)
                        {
                            return query.AsPagingByIndex(ordered =>
                            {
                                return ordered.OrderBy(k => k.CreatedTimeTicks);
                            },
                            index, size).SelectPaging(s => ToUserTokenModel(s, includeUser));
                        }

                        return query.ToList().Select(s => ToUserTokenModel(s, includeUser));
                    }
                );
            }

            // { userTokenId(userId: ?, loginProvider: "", name: "", includeUser: false) { ... loginProvider name value [user{...}] } }
            Field<UserTokenModelType>
            (
                name: "userTokenId",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "loginProvider" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<BooleanGraphType> { Name = "includeUser" }
                ),
                resolve: context =>
                {
                    var userId = context.GetArgument<TGenId>("userId");
                    var loginProvider = context.GetArgument<string>("loginProvider");
                    var name = context.GetArgument<string>("name");
                    var includeUser = context.GetArgument<bool>("includeUser");

                    var userToken = IdentityAccessor.UserTokens.FirstOrDefault(p
                        => p.UserId.Equals(userId) && p.LoginProvider == loginProvider && p.Name == name);

                    return ToUserTokenModel(userToken, includeUser);
                }
            );
        }


        /// <summary>
        /// 转为角色模型。
        /// </summary>
        /// <param name="role">给定的 <typeparamref name="TRole"/>。</param>
        /// <param name="includeRoleClaims">包含角色声明集合。</param>
        /// <returns>返回 <see cref="RoleModel"/>。</returns>
        protected virtual RoleModel ToRoleModel(TRole role, bool includeRoleClaims)
        {
            var model = role.ToModel();
            if (model.IsNotNull() && includeRoleClaims)
            {
                var roleClaims = IdentityAccessor.RoleClaims.Where(p => p.RoleId.Equals(role.Id)).ToList();
                model.RoleClaims = roleClaims.Select(s => s.ToModel()).ToList();
            }
            
            return model;
        }

        /// <summary>
        /// 转换为角色声明模型。
        /// </summary>
        /// <param name="roleClaim">给定的 <typeparamref name="TRoleClaim"/>。</param>
        /// <param name="includeRole">包含角色。</param>
        /// <returns>返回 <see cref="RoleClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        protected virtual RoleClaimModel ToRoleClaimModel(TRoleClaim roleClaim, bool includeRole)
        {
            var model = roleClaim.ToModel();
            if (model.IsNotNull() && includeRole)
            {
                var role = IdentityAccessor.Roles.Find(roleClaim.RoleId);
                model.Role = role.ToModel();
            }

            return model;
        }

        /// <summary>
        /// 转为用户模型。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="includeRoles">包含角色集合。</param>
        /// <param name="includeUserClaims">包含用户声明集合。</param>
        /// <param name="includeUserLogins">包含用户登入集合。</param>
        /// <param name="includeUserTokens">包含用户令牌集合。</param>
        /// <returns>返回 <see cref="UserModel"/>。</returns>
        protected virtual UserModel ToUserModel(TUser user, bool includeRoles,
            bool includeUserClaims, bool includeUserLogins, bool includeUserTokens)
        {
            var model = user.ToModel();
            if (model.IsNull())
                return model;

            if (includeRoles)
            {
                var roleIds = IdentityAccessor.UserRoles.Where(p => p.UserId.Equals(user.Id))
                    .Select(s => s.RoleId).ToList();

                model.Roles = roleIds.Select(s => IdentityAccessor.Roles.Find(s).ToModel()).ToList();
            }

            if (includeUserClaims)
            {
                var userClaims = IdentityAccessor.UserClaims.Where(p => p.UserId.Equals(user.Id)).ToList();
                model.UserClaims = userClaims.Select(s => s.ToModel()).ToList();
            }

            if (includeUserLogins)
            {
                var userLogins = IdentityAccessor.UserLogins.Where(p => p.UserId.Equals(user.Id)).ToList();
                model.UserLogins = userLogins.Select(s => s.ToModel()).ToList();
            }

            if (includeUserTokens)
            {
                var userTokens = IdentityAccessor.UserTokens.Where(p => p.UserId.Equals(user.Id)).ToList();
                model.UserTokens = userTokens.Select(s => s.ToModel()).ToList();
            }

            return model;
        }

        /// <summary>
        /// 转换为用户声明模型。
        /// </summary>
        /// <param name="userClaim">给定的 <typeparamref name="TUserClaim"/>。</param>
        /// <param name="includeUser">包含用户。</param>
        /// <returns>返回 <see cref="UserClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        protected virtual UserClaimModel ToUserClaimModel(TUserClaim userClaim, bool includeUser)
        {
            var model = userClaim.ToModel();
            if (model.IsNotNull() && includeUser)
            {
                var user = IdentityAccessor.Users.Find(userClaim.UserId);
                model.User = user.ToModel();
            }

            return model;
        }

        /// <summary>
        /// 转换为用户登入模型。
        /// </summary>
        /// <param name="userLogin">给定的 <typeparamref name="TUserLogin"/>。</param>
        /// <param name="includeUser">包含用户。</param>
        /// <returns>返回 <see cref="UserLoginModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        protected virtual UserLoginModel ToUserLoginModel(TUserLogin userLogin, bool includeUser)
        {
            var model = userLogin.ToModel();
            if (model.IsNotNull() && includeUser)
            {
                var user = IdentityAccessor.Users.Find(userLogin.UserId);
                model.User = user.ToModel();
            }

            return model;
        }

        /// <summary>
        /// 转换为用户令牌模型。
        /// </summary>
        /// <param name="userToken">给定的 <typeparamref name="TUserToken"/>。</param>
        /// <param name="includeUser">包含用户。</param>
        /// <returns>返回 <see cref="UserTokenModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        protected virtual UserTokenModel ToUserTokenModel(TUserToken userToken, bool includeUser)
        {
            var model = userToken.ToModel();
            if (model.IsNotNull() && includeUser)
            {
                var user = IdentityAccessor.Users.Find(userToken.UserId);
                model.User = user.ToModel();
            }

            return model;
        }

    }
}
