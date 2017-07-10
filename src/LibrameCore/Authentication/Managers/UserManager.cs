﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Entity;
using LibrameStandard.Entity.DbContexts;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Managers
{
    using Models;
    
    /// <summary>
    /// 用户管理器。
    /// </summary>
    /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
    public class UserManager<TUserModel> : AbstractManager, IUserManager<TUserModel>
        where TUserModel : class, IUserModel
    {
        /// <summary>
        /// 构造一个用户管理器实例。
        /// </summary>
        /// <param name="repository">给定的用户仓库。</param>
        /// <param name="passwordManager">给定的密码管理器。</param>
        /// <param name="options">给定的认证选项。</param>
        public UserManager(IRepository<SqlServerDbContextWriter, SqlServerDbContextReader, TUserModel> repository,
            IPasswordManager passwordManager, IOptions<AuthenticationOptions> options)
            : base(options)
        {
            Repository = repository.NotNull(nameof(repository));
            PasswordManager = passwordManager.NotNull(nameof(passwordManager));
        }


        /// <summary>
        /// 用户仓库。
        /// </summary>
        public IRepository<DbContext, DbContext, TUserModel> Repository { get; }

        /// <summary>
        /// 密码管理器。
        /// </summary>
        public IPasswordManager PasswordManager { get; }


        /// <summary>
        /// 异步创建用户。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回用户身份结果。</returns>
        public virtual async Task<LibrameIdentityResult> CreateAsync(TUserModel user)
        {
            try
            {
                string name = user.Name.ToLower();
                if (name.Contains("admin") || name.Contains("librame"))
                    return LibrameIdentityResult.NameInvalid;

                if (await Repository.ExistsAsync(p => p.Name == user.Name))
                    return LibrameIdentityResult.NameExists;

                await Repository.Writer.CreateAsync(user);

                return new LibrameIdentityResult
                {
                    IdentityResult = IdentityResult.Success,
                    User = user
                };
            }
            catch (Exception ex)
            {
                return LibrameIdentityResult.CreateAuthenticationFailed(ex, user);
            }
        }


        /// <summary>
        /// 异步验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <returns>返回用户身份结果。</returns>
        public virtual async Task<LibrameIdentityResult> ValidateAsync(string name, string passwd)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwd))
                return null;
            
            // 查找用户
            var user = await Repository.GetAsync(p => p.Name == name);

            if (user == null)
                return LibrameIdentityResult.NameNotExists;

            // 验证密码是否正确
            if (!PasswordManager.Validate(user.Passwd, passwd))
                return LibrameIdentityResult.PasswordError;

            return new LibrameIdentityResult
            {
                IdentityResult = IdentityResult.Success,
                User = user
            };
        }


        /// <summary>
        /// 验证唯一性。
        /// </summary>
        /// <param name="field">给定的字段。</param>
        /// <param name="value">给定的值。</param>
        /// <returns>返回表示是否唯一的布尔值。</returns>
        public virtual async Task<bool> ValidateUniquenessAsync(string field, string value)
        {
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(value))
                return false;
            
            // 建立表达式
            var predicate = field.AsEqualPropertyExpression<TUserModel>(value, typeof(string));

            // 不存在表示唯一
            return (!await Repository.ExistsAsync(predicate));
        }

    }
}
