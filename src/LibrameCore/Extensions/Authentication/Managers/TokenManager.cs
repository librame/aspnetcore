#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Algorithm;
using LibrameStandard.Utilities;
using Microsoft.Extensions.Logging;
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
        /// 构造一个令牌管理器实例。
        /// </summary>
        /// <param name="algorithmOptions">给定的算法选项。</param>
        /// <param name="options">给定的认证选项。</param>
        /// <param name="logger">给定的记录器。</param>
        public TokenManager(IOptions<AlgorithmOptions> algorithmOptions,
            IOptions<AuthenticationExtensionOptions> options, ILogger<TokenManager> logger)
            : base(options, logger)
        {
            AlgorithmOptions = algorithmOptions.NotNull(nameof(algorithmOptions)).Value;
        }


        /// <summary>
        /// 算法选项。
        /// </summary>
        public AlgorithmOptions AlgorithmOptions { get; }


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="identity">给定的 Librame 身份标识。</param>
        /// <returns>返回令牌字符串。</returns>
        public virtual string Encode(LibrameIdentity identity)
        {
            // 默认令牌签名证书
            if (Options.Local.Credentials == null)
            {
                // 默认以授权编号为密钥
                var key = AlgorithmOptions.AuthId.FromAuthIdByBytes();
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
        /// <returns>返回 Librame 身份标识。</returns>
        public virtual LibrameIdentity Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
                return null;

            var jwt = handler.ReadJwtToken(token);
            return new LibrameIdentity(jwt, Options);
        }

    }
}
