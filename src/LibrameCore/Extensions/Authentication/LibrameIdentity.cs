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
    /// Librame 身份标识。
    /// </summary>
    public class LibrameIdentity : ClaimsIdentity
    {
        private readonly AuthenticationExtensionOptions _options;


        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="jwt">给定的 JSON Web 令牌。</param>
        /// <param name="options">给定的认证选项。</param>
        public LibrameIdentity(JwtSecurityToken jwt, AuthenticationExtensionOptions options)
            : base(jwt.Claims, AuthenticationExtensionOptions.DEFAULT_SCHEME, DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options.NotNull(nameof(options));
        }
        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="claims">给定的声明集合。</param>
        /// <param name="options">给定的认证选项。</param>
        public LibrameIdentity(IEnumerable<Claim> claims, AuthenticationExtensionOptions options)
            : base(claims, AuthenticationExtensionOptions.DEFAULT_SCHEME, DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options.NotNull(nameof(options));
        }
        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="username">给定的用户名。</param>
        /// <param name="roles">给定的用户角色集合。</param>
        /// <param name="options">给定的认证选项。</param>
        public LibrameIdentity(string username, IEnumerable<string> roles, AuthenticationExtensionOptions options)
            : base(BuildClaims(username, roles, options), AuthenticationExtensionOptions.DEFAULT_SCHEME,
                  DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options;
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
        public virtual string Issuer => TryGetValue(JwtRegisteredClaimNames.Iss);

        /// <summary>
        /// 接收者。
        /// </summary>
        public virtual string Audience => TryGetValue(JwtRegisteredClaimNames.Aud);

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
        public virtual string JwtId => TryGetValue(JwtRegisteredClaimNames.Jti);

        /// <summary>
        /// 主题。
        /// </summary>
        public virtual string Subject => TryGetValue(JwtRegisteredClaimNames.Sub);

        /// <summary>
        /// 唯一名称。
        /// </summary>
        public virtual string UniqueName => TryGetValue(JwtRegisteredClaimNames.UniqueName);

        /// <summary>
        /// 角色集合。
        /// </summary>
        public virtual IEnumerable<string> Roles => TryGetValues(DefaultRoleClaimType);


        private DateTimeOffset TryGetDateTimeOffset(string type)
        {
            DateTimeOffset offset;

            var claim = FindFirst(type);

            if (claim != null)
                DateTimeOffset.TryParse(claim.Value, out offset);

            return offset;
        }

        private TValue TryGetValue<TValue>(string type, Func<string, TValue> converter, TValue defaultValue = default)
        {
            var claim = FindFirst(type);

            if (claim != null)
                return converter.Invoke(claim.Value);

            return defaultValue;
        }

        private string TryGetValue(string type)
        {
            var claim = FindFirst(type);

            if (claim != null)
                return claim.Value;

            return string.Empty;
        }

        private IEnumerable<string> TryGetValues(string type)
        {
            var claims = FindAll(type);

            if (claims.Count() < 1)
                return Enumerable.Empty<string>();

            return claims.Select(s => s.Value);
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
