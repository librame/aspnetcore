#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Identity.Stores;
    using Extensions;

    /// <summary>
    /// 身份 API 模型静态扩展。
    /// </summary>
    public static class IdentityApiModelExtensions
    {
        /// <summary>
        /// 转为身份结果模型。
        /// </summary>
        /// <param name="result">给定的 <see cref="IdentityResult"/>。</param>
        /// <returns>返回 <see cref="IdentityResultModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static IdentityResultModel ToModel(this IdentityResult result)
        {
            result.NotNull(nameof(result));

            return new IdentityResultModel()
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.ToList()
            };
        }


        #region Role

        /// <summary>
        /// 转为角色模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{TGenId, TCreatedBy}"/>。</param>
        /// <param name="roleClaims">给定的 <see cref="IReadOnlyList{RoleClaimModel}"/>（可选）。</param>
        /// <returns>返回 <see cref="RoleModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static RoleModel ToModel<TGenId, TCreatedBy>
            (this DefaultIdentityRole<TGenId, TCreatedBy> role,
            IReadOnlyList<RoleClaimModel> roleClaims = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (role.IsNull())
                return null;

            var model = new RoleModel();
            model.UpdateModel(role, roleClaims);

            return model;
        }

        /// <summary>
        /// 更新角色模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="RoleModel"/>。</param>
        /// <param name="role">给定的 <see cref="DefaultIdentityRole{TGenId, TCreatedBy}"/>。</param>
        /// <param name="roleClaims">给定的 <see cref="IReadOnlyList{RoleClaimModel}"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TGenId, TCreatedBy>(this RoleModel model,
            DefaultIdentityRole<TGenId, TCreatedBy> role,
            IReadOnlyList<RoleClaimModel> roleClaims = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            role.NotNull(nameof(role));

            if (model.Name != role.Name)
                model.Name = role.Name;

            if (roleClaims.IsNotNull())
                model.RoleClaims = roleClaims;

            model.Populate(role);
        }

        #endregion


        #region RoleClaim

        /// <summary>
        /// 转为角色声明模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="roleClaim">给定的 <see cref="DefaultIdentityRoleClaim{TRoleId, TCreatedBy}"/>。</param>
        /// <param name="role">给定的 <see cref="RoleModel"/>（可选）。</param>
        /// <returns>返回 <see cref="RoleClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static RoleClaimModel ToModel<TGenId, TCreatedBy>
            (this DefaultIdentityRoleClaim<TGenId, TCreatedBy> roleClaim, RoleModel role = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (roleClaim.IsNull())
                return null;

            var model = new RoleClaimModel();
            model.UpdateModel(roleClaim, role);

            return model;
        }

        /// <summary>
        /// 更新角色声明模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="RoleClaimModel"/>。</param>
        /// <param name="roleClaim">给定的 <see cref="DefaultIdentityRoleClaim{TRoleId, TCreatedBy}"/>。</param>
        /// <param name="role">给定的 <see cref="RoleModel"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TGenId, TCreatedBy>(this RoleClaimModel model,
            DefaultIdentityRoleClaim<TGenId, TCreatedBy> roleClaim, RoleModel role = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            roleClaim.NotNull(nameof(roleClaim));

            if (model.ClaimType != roleClaim.ClaimType)
                model.ClaimType = roleClaim.ClaimType;

            if (model.ClaimValue != roleClaim.ClaimValue)
                model.ClaimValue = roleClaim.ClaimValue;

            if (role.IsNotNull())
                model.Role = role;

            model.Populate(roleClaim);
        }

        #endregion


        #region User

        /// <summary>
        /// 转为用户模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId, TCreatedBy}"/>。</param>
        /// <param name="roles">给定的 <see cref="IReadOnlyList{RoleModel}"/>（可选）。</param>
        /// <param name="userClaims">给定的 <see cref="IReadOnlyList{UserClaimModel}"/>（可选）。</param>
        /// <param name="userLogins">给定的 <see cref="IReadOnlyList{UserLoginModel}"/>（可选）。</param>
        /// <param name="userTokens">给定的 <see cref="IReadOnlyList{UserTokenModel}"/>（可选）。</param>
        /// <returns>返回 <see cref="UserModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UserModel ToModel<TGenId, TCreatedBy>
            (this DefaultIdentityUser<TGenId, TCreatedBy> user,
            IReadOnlyList<RoleModel> roles = null,
            IReadOnlyList<UserClaimModel> userClaims = null,
            IReadOnlyList<UserLoginModel> userLogins = null,
            IReadOnlyList<UserTokenModel> userTokens = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (user.IsNull())
                return null;

            var model = new UserModel();
            model.UpdateModel(user, roles, userClaims, userLogins, userTokens);

            return model;
        }

        /// <summary>
        /// 更新用户模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="UserModel"/>。</param>
        /// <param name="user">给定的 <see cref="DefaultIdentityUser{TGenId, TCreatedBy}"/>。</param>
        /// <param name="roles">给定的 <see cref="IReadOnlyList{RoleModel}"/>（可选）。</param>
        /// <param name="userClaims">给定的 <see cref="IReadOnlyList{UserClaimModel}"/>（可选）。</param>
        /// <param name="userLogins">给定的 <see cref="IReadOnlyList{UserLoginModel}"/>（可选）。</param>
        /// <param name="userTokens">给定的 <see cref="IReadOnlyList{UserTokenModel}"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TGenId, TCreatedBy>(this UserModel model,
            DefaultIdentityUser<TGenId, TCreatedBy> user,
            IReadOnlyList<RoleModel> roles = null,
            IReadOnlyList<UserClaimModel> userClaims = null,
            IReadOnlyList<UserLoginModel> userLogins = null,
            IReadOnlyList<UserTokenModel> userTokens = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            user.NotNull(nameof(user));

            if (model.UserName != user.UserName)
                model.UserName = user.UserName;

            if (model.Email != user.Email)
                model.Email = user.Email;

            if (model.EmailConfirmed != user.EmailConfirmed)
                model.EmailConfirmed = user.EmailConfirmed;

            if (model.PhoneNumber != user.PhoneNumber)
                model.PhoneNumber = user.PhoneNumber;

            if (model.PhoneNumberConfirmed != user.PhoneNumberConfirmed)
                model.PhoneNumberConfirmed = user.PhoneNumberConfirmed;

            if (model.AccessFailedCount != user.AccessFailedCount)
                model.AccessFailedCount = user.AccessFailedCount;

            if (model.LockoutEnd != user.LockoutEnd)
                model.LockoutEnd = user.LockoutEnd;

            if (model.LockoutEnabled != user.LockoutEnabled)
                model.LockoutEnabled = user.LockoutEnabled;

            if (roles.IsNotNull())
                model.Roles = roles;

            if (userClaims.IsNotNull())
                model.UserClaims = userClaims;

            if (userLogins.IsNotNull())
                model.UserLogins = userLogins;

            if (userTokens.IsNotNull())
                model.UserTokens = userTokens;

            model.Populate(user);
        }

        #endregion


        #region UserClaim

        /// <summary>
        /// 转为用户声明模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="userClaim">给定的 <see cref="DefaultIdentityUserClaim{TUserId, TCreatedBy}"/>。</param>
        /// <param name="user">给定的 <see cref="UserModel"/>（可选）。</param>
        /// <returns>返回 <see cref="UserClaimModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UserClaimModel ToModel<TGenId, TCreatedBy>
            (this DefaultIdentityUserClaim<TGenId, TCreatedBy> userClaim, UserModel user = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (userClaim.IsNull())
                return null;

            var model = new UserClaimModel();
            model.UpdateModel(userClaim, user);

            return model;
        }

        /// <summary>
        /// 更新用户声明模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="UserClaimModel"/>。</param>
        /// <param name="userClaim">给定的 <see cref="DefaultIdentityUserClaim{TUserId, TCreatedBy}"/>。</param>
        /// <param name="user">给定的 <see cref="UserModel"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TGenId, TCreatedBy>(this UserClaimModel model,
            DefaultIdentityUserClaim<TGenId, TCreatedBy> userClaim, UserModel user = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            userClaim.NotNull(nameof(userClaim));

            if (model.ClaimType != userClaim.ClaimType)
                model.ClaimType = userClaim.ClaimType;

            if (model.ClaimValue != userClaim.ClaimValue)
                model.ClaimValue = userClaim.ClaimValue;

            if (user.IsNotNull())
                model.User = user;

            model.Populate(userClaim);
        }

        #endregion


        #region UserLogin

        /// <summary>
        /// 转为用户登入模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="userLogin">给定的 <see cref="DefaultIdentityUserLogin{TUserId, TCreatedBy}"/>。</param>
        /// <param name="user">给定的 <see cref="UserModel"/>（可选）。</param>
        /// <returns>返回 <see cref="UserLoginModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UserLoginModel ToModel<TGenId, TCreatedBy>
            (this DefaultIdentityUserLogin<TGenId, TCreatedBy> userLogin, UserModel user = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (userLogin.IsNull())
                return null;

            var model = new UserLoginModel();
            model.UpdateModel(userLogin, user);

            return model;
        }

        /// <summary>
        /// 更新用户登入模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="UserLoginModel"/>。</param>
        /// <param name="userLogin">给定的 <see cref="DefaultIdentityUserLogin{TUserId, TCreatedBy}"/>。</param>
        /// <param name="user">给定的 <see cref="UserModel"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TGenId, TCreatedBy>(this UserLoginModel model,
            DefaultIdentityUserLogin<TGenId, TCreatedBy> userLogin, UserModel user = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            userLogin.NotNull(nameof(userLogin));

            if (model.LoginProvider != userLogin.LoginProvider)
                model.LoginProvider = userLogin.LoginProvider;

            if (model.ProviderDisplayName != userLogin.ProviderDisplayName)
                model.ProviderDisplayName = userLogin.ProviderDisplayName;

            if (model.ProviderKey != userLogin.ProviderKey)
                model.ProviderKey = userLogin.ProviderKey;

            if (user.IsNotNull())
                model.User = user;

            model.Populate(userLogin);
        }

        #endregion


        #region UserToken

        /// <summary>
        /// 转为用户令牌模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="userToken">给定的 <see cref="DefaultIdentityUserToken{TUserId, TCreatedBy}"/>。</param>
        /// <param name="user">给定的 <see cref="UserModel"/>（可选）。</param>
        /// <returns>返回 <see cref="UserTokenModel"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static UserTokenModel ToModel<TGenId, TCreatedBy>
            (this DefaultIdentityUserToken<TGenId, TCreatedBy> userToken, UserModel user = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            if (userToken.IsNull())
                return null;

            var model = new UserTokenModel();
            model.UpdateModel(userToken, user);

            return model;
        }

        /// <summary>
        /// 更新用户令牌模型。
        /// </summary>
        /// <typeparam name="TGenId">指定的增量式标识类型。</typeparam>
        /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
        /// <param name="model">给定的 <see cref="UserTokenModel"/>。</param>
        /// <param name="userToken">给定的 <see cref="DefaultIdentityUserToken{TUserId, TCreatedBy}"/>。</param>
        /// <param name="user">给定的 <see cref="UserModel"/>（可选）。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数", Justification = "<挂起>")]
        public static void UpdateModel<TGenId, TCreatedBy>(this UserTokenModel model,
            DefaultIdentityUserToken<TGenId, TCreatedBy> userToken, UserModel user = null)
            where TGenId : IEquatable<TGenId>
            where TCreatedBy : IEquatable<TCreatedBy>
        {
            model.NotNull(nameof(model));
            userToken.NotNull(nameof(userToken));

            if (model.LoginProvider != userToken.LoginProvider)
                model.LoginProvider = userToken.LoginProvider;

            if (model.Name != userToken.Name)
                model.Name = userToken.Name;

            if (model.Value != userToken.Value)
                model.Value = userToken.Value;

            if (user.IsNotNull())
                model.User = user;

            model.Populate(userToken);
        }

        #endregion

    }
}
