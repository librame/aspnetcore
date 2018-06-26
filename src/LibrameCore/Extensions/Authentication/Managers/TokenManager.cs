#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Algorithm;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LibrameCore.Extensions.Authentication.Managers
{
    /// <summary>
    /// 令牌管理器。
    /// </summary>
    public class TokenManager : AbstractAuthenticationExtensionService<TokenManager>, ITokenManager
    {
        /// <summary>
        /// 构造一个 <see cref="TokenManager"/> 实例。
        /// </summary>
        /// <param name="symmetry">给定的 <see cref="ISymmetryAlgorithm"/>。</param>
        /// <param name="options">给定的认证选项。</param>
        public TokenManager(ISymmetryAlgorithm symmetry, IOptionsMonitor<AuthenticationExtensionOptions> options)
            : base(options)
        {
            Symmetry = symmetry.NotNull(nameof(symmetry));
        }


        /// <summary>
        /// 对称算法。
        /// </summary>
        public ISymmetryAlgorithm Symmetry { get; }


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="identity">给定的 <see cref="LibrameClaimsIdentity"/>。</param>
        /// <returns>返回令牌字符串。</returns>
        public virtual string Encode(LibrameClaimsIdentity identity)
        {
            // 默认令牌签名证书
            if (Options.Local.Credentials == null)
            {
                // 默认使用 AES 密钥
                var key = Symmetry.KeyGenerator.FromAESKey();
                var securityKey = new SymmetricSecurityKey(key);

                Options.Local.Credentials = new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha384);
            }

            var jwt = new JwtSecurityToken(
                //identity.Issuer,
                //identity.Audience,
                claims: identity.Claims,
                signingCredentials: Options.Local.Credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <returns>返回 <see cref="LibrameClaimsIdentity"/>。</returns>
        public virtual LibrameClaimsIdentity Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
                return null;

            var jwt = handler.ReadJwtToken(token);
            return new LibrameClaimsIdentity(jwt, Options);
        }

    }
}
