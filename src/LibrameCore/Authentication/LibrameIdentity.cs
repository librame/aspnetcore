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
        private readonly TokenOptions _options;


        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="jwt">给定的 JSON Web 令牌。</param>
        /// <param name="options">给定的令牌提供程序选项。</param>
        public LibrameIdentity(JwtSecurityToken jwt, TokenOptions options)
            : base(jwt.Claims, AuthenticationOptions.DEFAULT_SCHEME, DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options.NotNull(nameof(options));
        }
        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="claims">给定的声明集合。</param>
        /// <param name="options">给定的令牌提供程序选项。</param>
        public LibrameIdentity(IEnumerable<Claim> claims, TokenOptions options)
            : base(claims, AuthenticationOptions.DEFAULT_SCHEME, DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options.NotNull(nameof(options));
        }
        /// <summary>
        /// 构造一个 Librame 身份标识实例。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <param name="roles">给定的用户角色集合。</param>
        /// <param name="options">给定的令牌提供程序选项。</param>
        public LibrameIdentity(IUserModel user, IEnumerable<string> roles, TokenOptions options)
            : base(BuildClaims(user, roles, options), AuthenticationOptions.DEFAULT_SCHEME, DefaultNameClaimType, DefaultRoleClaimType)
        {
            _options = options;
        }


        /// <summary>
        /// 认证类型。
        /// </summary>
        public override string AuthenticationType => AuthenticationOptions.DEFAULT_SCHEME;

        /// <summary>
        /// 用户名。
        /// </summary>
        public override string Name => TryGetValue(DefaultNameClaimType);

        /// <summary>
        /// 是否已认证。
        /// </summary>
        public override bool IsAuthenticated
        {
            get
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(JwtId))
                    return false;

                if (DateTime.UtcNow > ExpirationTimeUtc)
                    return false;

                if (Issuer != _options.Issuer || Audience != _options.Audience)
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
        public virtual DateTime IssuedTimeUtc
            => TryGetValue(JwtRegisteredClaimNames.Iat, c => new DateTime(c.AsLong()));

        /// <summary>
        /// UTC 过期时间。
        /// </summary>
        public virtual DateTime ExpirationTimeUtc
            => TryGetValue(JwtRegisteredClaimNames.Exp, c => new DateTime(c.AsLong()));


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


        private TValue TryGetValue<TValue>(string type, Func<string, TValue> converter, TValue defaultValue = default(TValue))
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


        private static IList<Claim> BuildClaims(IUserModel user, IEnumerable<string> roles, TokenOptions options)
        {
            user.NotNull(nameof(user));
            options.NotNull(nameof(options));

            var issuedTimeUtc = DateTime.UtcNow;
            var expirationTimeUtc = issuedTimeUtc.Add(options.Expiration);

            var claims = new List<Claim>
            {
                new Claim(DefaultNameClaimType, user.Name),

                // 主题（如名称、邮箱等）
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                // 签发时间
                new Claim(JwtRegisteredClaimNames.Iat, issuedTimeUtc.Ticks.ToString(), ClaimValueTypes.Integer64),
                // 过期时间
                new Claim(JwtRegisteredClaimNames.Exp, expirationTimeUtc.Ticks.ToString(), ClaimValueTypes.Integer64),
                // 签发者
                new Claim(JwtRegisteredClaimNames.Iss, options.Issuer),
                // 接收者
                new Claim(JwtRegisteredClaimNames.Aud, options.Audience),
                
                // 唯一名称
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                // 唯一标识符
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 角色集合
            roles.Invoke(r => claims.Add(new Claim(DefaultRoleClaimType, r)));

            return claims;
        }

    }
}
