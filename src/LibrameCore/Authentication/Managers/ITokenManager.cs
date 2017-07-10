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
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    using Models;

    /// <summary>
    /// 令牌管理器接口。
    /// </summary>
    public interface ITokenManager : IManager
    {
        /// <summary>
        /// 算法选项。
        /// </summary>
        AlgorithmOptions AlgorithmOptions { get; }


        /// <summary>
        /// 编码令牌。
        /// </summary>
        /// <param name="identity">给定的用户身份标识。</param>
        /// <returns>返回令牌字符串。</returns>
        string Encode(ClaimsIdentity identity);


        /// <summary>
        /// 解码令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <param name="parseUserRolesFactory">给定的解析用户与角色集合工厂方法。</param>
        /// <returns>返回用户模型与角色集合。</returns>
        (IUserModel User, IEnumerable<string> Roles) Decode(string token,
            Func<JwtSecurityToken, (IUserModel User, IEnumerable<string> Roles)> parseUserRolesFactory);


        /// <summary>
        /// 异步验证令牌。
        /// </summary>
        /// <param name="token">给定的令牌字符串。</param>
        /// <param name="requiredRoles">需要的角色集合。</param>
        /// <param name="parseUserRolesFactory">给定的解析用户与角色集合工厂方法。</param>
        /// <returns>返回用户身份结果。</returns>
        Task<LibrameIdentityResult> ValidateAsync(string token, IEnumerable<string> requiredRoles,
            Func<JwtSecurityToken, (IUserModel User, IEnumerable<string> Roles)> parseUserRolesFactory);
    }
}
