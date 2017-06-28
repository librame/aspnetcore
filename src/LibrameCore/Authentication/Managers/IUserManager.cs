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
    using Models;

    /// <summary>
    /// 用户管理器接口。
    /// </summary>
    /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
    public interface IUserManager<TUserModel> : IManager
        where TUserModel : class, IUserModel
    {
        /// <summary>
        /// 密码管理器。
        /// </summary>
        IPasswordManager PasswordManager { get; }


        /// <summary>
        /// 异步创建用户。
        /// </summary>
        /// <param name="model">给定的用户模型。</param>
        /// <returns>返回用户身份结果。</returns>
        Task<UserIdentityResult> CreateAsync(TUserModel model);


        /// <summary>
        /// 异步验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <returns>返回用户身份结果。</returns>
        Task<UserIdentityResult> ValidateAsync(string name, string passwd);


        /// <summary>
        /// 验证唯一性。
        /// </summary>
        /// <param name="field">给定的字段。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回表示是否唯一的布尔值。</returns>
        Task<bool> ValidateUniquenessAsync(string field, string value);
    }
}
