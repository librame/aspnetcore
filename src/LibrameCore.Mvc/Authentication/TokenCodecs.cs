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
using System.Linq;
using System.Security.Claims;

namespace LibrameStandard.Authentication
{
    using Models;
    using Utilities;

    /// <summary>
    /// 令牌编解码器接口。
    /// </summary>
    public interface ITokenCodec
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }

        /// <summary>
        /// 令牌处理程序设置。
        /// </summary>
        TokenHandlerSettings Settings { get; }


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回令牌字符串。</returns>
        string Encode(IUserModel user);


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <returns>返回用户模型。</returns>
        IUserModel Decode(string token);
    }


    /// <summary>
    /// 令牌编解码器。
    /// </summary>
    public class TokenCodec : ITokenCodec
    {
        /// <summary>
        /// 构造一个令牌编解码器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public TokenCodec(ILibrameBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }

        /// <summary>
        /// 令牌处理程序设置。
        /// </summary>
        public TokenHandlerSettings Settings => Builder.GetService<TokenHandlerSettings>();


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回令牌字符串。</returns>
        public virtual string Encode(IUserModel user)
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
                Settings.Issuer,
                Settings.Audience,
                claims,
                now,
                now.Add(Settings.Expiration),
                Settings.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <returns>返回用户模型。</returns>
        public virtual IUserModel Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
                return null;

            var jwt = handler.ReadJwtToken(token);

            return new UserModel()
            {
                UniqueId = jwt.Claims.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.Sub)?.Value,
                Name = jwt.Claims.FirstOrDefault(p => p.Type == JwtRegisteredClaimNames.UniqueName)?.Value
            };
        }

    }
}
