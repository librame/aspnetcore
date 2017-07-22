#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    using Models;
    
    /// <summary>
    /// 用户管理器接口。
    /// </summary>
    /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
    public interface IUserManager<TUserModel> : IManager
        where TUserModel : class, IUserModel
    {
        /// <summary>
        /// 用户仓库。
        /// </summary>
        IRepository<DbContext, DbContext, TUserModel> Repository { get; }

        /// <summary>
        /// 密码管理器。
        /// </summary>
        IPasswordManager PasswordManager { get; }

        /// <summary>
        /// 受保护的用户名集合。
        /// </summary>
        IEnumerable<string> ProtectedUsernames { get; }


        /// <summary>
        /// 异步创建用户。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回用户身份结果。</returns>
        Task<LibrameIdentityResult> CreateAsync(TUserModel user);


        /// <summary>
        /// 异步验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <returns>返回用户身份结果。</returns>
        Task<LibrameIdentityResult> ValidateAsync(string name, string passwd);


        /// <summary>
        /// 验证唯一性。
        /// </summary>
        /// <param name="field">给定的字段。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回表示是否唯一的布尔值。</returns>
        Task<bool> ValidateUniquenessAsync(string field, string value);
    }
}
