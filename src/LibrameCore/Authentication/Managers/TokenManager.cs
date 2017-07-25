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
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    using Models;

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
        /// <param name="identity">给定的用户身份标识。</param>
        /// <returns>返回令牌字符串。</returns>
        public virtual string Encode(ClaimsIdentity identity)
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

            var utcNow = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                options.Issuer,
                options.Audience,
                identity.Claims,
                utcNow,
                utcNow.Add(options.Expiration),
                options.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <param name="parseUserRolesFactory">给定的解析用户与角色集合工厂方法。</param>
        /// <returns>返回用户模型与角色集合。</returns>
        public virtual (IUserModel User, IEnumerable<string> Roles) Decode(string token,
            Func<JwtSecurityToken, (IUserModel User, IEnumerable<string> Roles)> parseUserRolesFactory)
        {
            parseUserRolesFactory.NotNull(nameof(parseUserRolesFactory));

            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
                return (null, null);

            var jwt = handler.ReadJwtToken(token);
            return parseUserRolesFactory.Invoke(jwt);
        }


        /// <summary>
        /// 异步验证令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <param name="requiredRoles">需要的角色集合。</param>
        /// <param name="parseUserRolesFactory">给定的解析用户与角色集合工厂方法。</param>
        /// <returns>返回用户身份结果。</returns>
        public virtual Task<LibrameIdentityResult> ValidateAsync(string token, IEnumerable<string> requiredRoles,
            Func<JwtSecurityToken, (IUserModel User, IEnumerable<string> Roles)> parseUserRolesFactory)
        {
            if (string.IsNullOrEmpty(token))
                return null;
            
            // 解码令牌代替数据库验证
            var userRoles = Decode(token, parseUserRolesFactory);

            if (userRoles.User == null)
                return Task.FromResult(LibrameIdentityResult.InvalidToken);

            // 验证角色
            if (requiredRoles != null)
            {
                var hasRole = false;

                foreach (var rr in requiredRoles)
                {
                    foreach (var r in userRoles.Roles)
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
                    return Task.FromResult(new LibrameIdentityResult
                    {
                        IdentityResult = LibrameIdentityResult.InvalidRole,
                        User = userRoles.User
                    });
                }
            }

            return Task.FromResult(new LibrameIdentityResult
            {
                IdentityResult = IdentityResult.Success,
                User = userRoles.User
            });
        }

    }
}
