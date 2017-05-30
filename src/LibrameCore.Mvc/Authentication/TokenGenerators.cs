#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibrameCore.Authentication
{
    using Utilities;

    /// <summary>
    /// 令牌生成器接口。
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// 令牌生成选项。
        /// </summary>
        TokenOptions Options { get; }


        /// <summary>
        /// 生成令牌。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回令牌字符串。</returns>
        string Generate(IUserModel user);
    }


    /// <summary>
    /// 令牌生成器。
    /// </summary>
    public class TokenGenerator : ITokenGenerator
    {
        /// <summary>
        /// 构造一个令牌生成器。
        /// </summary>
        /// <param name="option">给定的令牌生成器选项。</param>
        public TokenGenerator(TokenOptions option)
        {
            Options = option.NotNull(nameof(option));
        }


        /// <summary>
        /// 令牌生成选项。
        /// </summary>
        public TokenOptions Options { get; }


        /// <summary>
        /// 生成令牌。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回令牌字符串。</returns>
        public virtual string Generate(IUserModel user)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UniqueId),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, user.UniqueId), // Guid.NewGuid().ToString()
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                Options.Issuer,
                Options.Audience,
                claims,
                now,
                now.Add(Options.Expiration),
                Options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

    }
}
