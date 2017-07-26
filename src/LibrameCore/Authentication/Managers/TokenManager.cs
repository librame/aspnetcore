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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    /// <summary>
    /// 令牌管理器。
    /// </summary>
    public class TokenManager : AbstractManager, ITokenManager
    {
        /// <summary>
        /// 构造一个用户管理器实例。
        /// </summary>
        /// <param name="options">给定的认证选项。</param>
        /// <param name="algorithmOptions">给定的算法选项。</param>
        public TokenManager(IOptions<AuthenticationOptions> options, IOptions<AlgorithmOptions> algorithmOptions)
            : base(options)
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
            var options = Options.TokenProvider;

            // 默认令牌签名证书
            if (options.SigningCredentials == null)
            {
                // 默认以授权编号为密钥
                var key = AlgorithmOptions.FromAuthIdAsBytes();
                var securityKey = new SymmetricSecurityKey(key);

                options.SigningCredentials = new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha384);
            }
            
            var jwt = new JwtSecurityToken(
                identity.Issuer,
                identity.Audience,
                identity.Claims,
                signingCredentials: options.SigningCredentials);

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
            return new LibrameIdentity(jwt, Options.TokenProvider);
        }


        /// <summary>
        /// 异步验证令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <param name="requiredRoles">需要的角色集合。</param>
        /// <returns>返回 Librame 身份结果与标识。</returns>
        public virtual Task<(IdentityResult identityResult, LibrameIdentity identity)> ValidateAsync(string token,
            IEnumerable<string> requiredRoles)
        {
            if (string.IsNullOrEmpty(token))
                return Task.FromResult((IdentityResultHelper.InvalidToken, (LibrameIdentity)null));

            // 解码令牌代替数据库验证
            var identity = Decode(token);

            if (identity == null)
                return Task.FromResult((IdentityResultHelper.InvalidToken, identity));

            // 验证角色
            if (requiredRoles != null)
            {
                var hasRole = false;

                foreach (var rr in requiredRoles)
                {
                    foreach (var r in identity.Roles)
                    {
                        // 忽略大小写
                        if (rr.Equals(r, StringComparison.OrdinalIgnoreCase))
                        {
                            hasRole = true;
                            break;
                        }
                    }

                    if (hasRole)
                        break;
                }

                if (!hasRole)
                {
                    return Task.FromResult((IdentityResultHelper.InvalidRole, identity));
                }
            }

            return Task.FromResult((IdentityResult.Success, identity));
        }

    }
}
