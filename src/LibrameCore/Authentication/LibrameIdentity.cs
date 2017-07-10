#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace LibrameCore.Authentication
{
    using Models;
    
    /// <summary>
    /// Librame 身份标识。
    /// </summary>
    public class LibrameIdentity : ClaimsIdentity
    {
        /// <summary>
        /// 角色集合声明名称。
        /// </summary>
        public const string ROLES_CLAIM_NAME = "roles";


        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="jwt">给定的 JSON Web 令牌。</param>
        public LibrameIdentity(JwtSecurityToken jwt)
            : base(jwt.Claims)
        {
        }

        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <param name="roles">给定的用户角色集合。</param>
        /// <param name="settings">给定的令牌处理程序设置。</param>
        public LibrameIdentity(IUserModel user, IEnumerable<string> roles, TokenHandlerSettings settings)
        {
            user.NotNull(nameof(user));
            settings.NotNull(nameof(settings));

            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                // 用户（如名称、邮箱等）
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                // 过期时间
                new Claim(JwtRegisteredClaimNames.Exp, now.Add(settings.Expiration).ToString(), ClaimValueTypes.Integer64),
                // 签发时间
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64),
                // 签发者
                new Claim(JwtRegisteredClaimNames.Iss, settings.Issuer),
                // 接收者
                new Claim(JwtRegisteredClaimNames.Aud, settings.Audience),

                // 角色集合
                new Claim(ROLES_CLAIM_NAME, roles.JoinString(",")),

                // 唯一名称
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                // 唯一标识符
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            AddClaims(claims);
        }


        /// <summary>
        /// 解析用户模型与角色集合。
        /// </summary>
        /// <param name="jwt">给定的 JSON Web 令牌。</param>
        /// <returns>返回用户模型与角色集合。</returns>
        public static (IUserModel User, IEnumerable<string> Roles) ParseUserRoles(JwtSecurityToken jwt)
        {
            jwt.NotNull(nameof(jwt));

            var user = new UserModel()
            {
                Name = jwt.Subject
            };

            var roles = jwt.Claims.FirstOrDefault(p => p.Type == ROLES_CLAIM_NAME)?.Value;

            if (!string.IsNullOrEmpty(roles))
                return (user, roles.Split(','));
            else
                return (user, null);
        }

    }
}
