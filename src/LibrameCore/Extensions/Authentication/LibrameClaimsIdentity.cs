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

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// Librame 声明标识。
    /// </summary>
    public class LibrameClaimsIdentity : ClaimsIdentity
    {
        private readonly AuthenticationExtensionOptions _options;


        /// <summary>
        /// 构造一个 <see cref="LibrameClaimsIdentity"/> 实例。
        /// </summary>
        /// <param name="jwt">给定的 JSON Web 令牌。</param>
        /// <param name="options">给定的认证选项。</param>
        public LibrameClaimsIdentity(JwtSecurityToken jwt, AuthenticationExtensionOptions options)
            : base(jwt.Claims, AuthenticationExtensionOptions.DEFAULT_SCHEME, DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options.NotNull(nameof(options));
        }
        /// <summary>
        /// 构造一个 <see cref="LibrameClaimsIdentity"/> 实例。
        /// </summary>
        /// <param name="claims">给定的声明集合。</param>
        /// <param name="options">给定的认证选项。</param>
        public LibrameClaimsIdentity(IEnumerable<Claim> claims, AuthenticationExtensionOptions options)
            : base(claims, AuthenticationExtensionOptions.DEFAULT_SCHEME, DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options.NotNull(nameof(options));
        }
        /// <summary>
        /// 构造一个 <see cref="LibrameClaimsIdentity"/> 实例。
        /// </summary>
        /// <param name="username">给定的用户名。</param>
        /// <param name="roles">给定的用户角色集合。</param>
        /// <param name="options">给定的认证选项。</param>
        public LibrameClaimsIdentity(string username, IEnumerable<string> roles, AuthenticationExtensionOptions options)
            : base(BuildClaims(username, roles, options), AuthenticationExtensionOptions.DEFAULT_SCHEME,
                  DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options; // BuildClaims 已检测是否为空
        }


        /// <summary>
        /// 是否已认证。
        /// </summary>
        public override bool IsAuthenticated
        {
            get
            {
                // 验证名称与标识
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(JwtId))
                    return false;

                // 验证签发者
                if (Issuer != _options.ClaimsIssuer)
                    return false;

                // 验证是否过期
                if (DateTimeOffset.UtcNow > ExpirationTimeUtc)
                    return false;
                
                return true;
            }
        }


        /// <summary>
        /// 签发者。
        /// </summary>
        public virtual string Issuer => FindFirst(JwtRegisteredClaimNames.Iss)?.Value;

        /// <summary>
        /// 接收者。
        /// </summary>
        public virtual string Audience => FindFirst(JwtRegisteredClaimNames.Aud)?.Value;

        /// <summary>
        /// UTC 签发时间。
        /// </summary>
        public virtual DateTimeOffset IssuedTimeUtc => TryGetDateTimeOffset(JwtRegisteredClaimNames.Iat);

        /// <summary>
        /// UTC 过期时间。
        /// </summary>
        public virtual DateTimeOffset ExpirationTimeUtc => TryGetDateTimeOffset(JwtRegisteredClaimNames.Exp);


        /// <summary>
        /// JWT 标识。
        /// </summary>
        public virtual string JwtId => FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

        /// <summary>
        /// 主题。
        /// </summary>
        public virtual string Subject => FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        /// <summary>
        /// 唯一名称。
        /// </summary>
        public virtual string UniqueName => FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

        /// <summary>
        /// 角色集合。
        /// </summary>
        public virtual IEnumerable<string> Roles => FindAll(DefaultRoleClaimType).Select(c => c.Value);


        private DateTimeOffset TryGetDateTimeOffset(string type)
        {
            DateTimeOffset offset = DateTimeOffset.Now;

            var claim = FindFirst(type);
            if (claim != null)
                DateTimeOffset.TryParse(claim.Value, out offset);

            return offset;
        }


        private static IList<Claim> BuildClaims(string username, IEnumerable<string> roles,
            AuthenticationExtensionOptions options)
        {
            username.NotEmpty(nameof(username));
            options.NotNull(nameof(options));

            var issuedTimeUtc = DateTimeOffset.UtcNow;
            var expirationTimeUtc = issuedTimeUtc.Add(options.Cookie.Expiration.Value);

            var claims = new List<Claim>
            {
                new Claim(DefaultNameClaimType, username),

                // 主题（如名称、邮箱等）
                new Claim(JwtRegisteredClaimNames.Sub, username),
                // 签发时间
                new Claim(JwtRegisteredClaimNames.Iat, issuedTimeUtc.ToString()),
                // 过期时间
                new Claim(JwtRegisteredClaimNames.Exp, expirationTimeUtc.ToString()),
                // 签发者
                new Claim(JwtRegisteredClaimNames.Iss, options.ClaimsIssuer),
                //// 接收者
                //new Claim(JwtRegisteredClaimNames.Aud, options.Audience),
                
                // 唯一名称
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                // 唯一标识符
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 角色集合
            roles.Invoke(r => claims.Add(new Claim(DefaultRoleClaimType, r)));

            return claims;
        }

    }
}
