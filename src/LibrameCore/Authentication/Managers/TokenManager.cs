#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrameStandard.Authentication.Managers
{
    using Models;
    using Handlers;

    /// <summary>
    /// 令牌管理器。
    /// </summary>
    public class TokenManager : AbstractManager, ITokenManager
    {
        /// <summary>
        /// 构造一个用户管理器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public TokenManager(ILibrameBuilder builder)
            : base(builder)
        {
        }


        /// <summary>
        /// 令牌处理程序设置。
        /// </summary>
        public TokenHandlerSettings HandlerSettings => Builder.GetService<TokenHandlerSettings>();


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
                HandlerSettings.Issuer,
                HandlerSettings.Audience,
                claims,
                now,
                now.Add(HandlerSettings.Expiration),
                HandlerSettings.SigningCredentials);

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


        /// <summary>
        /// 异步验证令牌。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回用户身份结果。</returns>
        public virtual Task<UserIdentityResult> ValidateAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            
            // 解码令牌代替数据库验证
            var user = Decode(name);

            var identityResult = (user == null) ? IdentityResult.Failed(UserIdentityErrors.TokenInvalid) : IdentityResult.Success;

            return Task.FromResult(new UserIdentityResult(identityResult, user));
        }

    }
}
