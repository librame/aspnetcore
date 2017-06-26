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
using System.Threading.Tasks;

namespace LibrameStandard.Authentication.Managers
{
    using Handlers;
    using Models;

    /// <summary>
    /// 令牌管理器接口。
    /// </summary>
    public interface ITokenManager : IManager
    {
        /// <summary>
        /// 令牌处理程序设置。
        /// </summary>
        TokenHandlerSettings HandlerSettings { get; }


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


        /// <summary>
        /// 异步验证令牌。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回用户身份结果。</returns>
        Task<UserIdentityResult> ValidateAsync(string name);
    }
}
