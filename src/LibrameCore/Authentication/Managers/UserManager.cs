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
using LibrameStandard.Entity.DbContexts;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
        /// 受保护的用户名集合。
        /// </summary>
        public IEnumerable<string> ProtectedUsernames => Options.ProtectedUsernames.Split(',').Invoke(n => n.ToLower());


        /// <summary>
        /// 检测是否包含受保护的用户名。
        /// </summary>
        /// <param name="name">给定的用户名。</param>
        /// <returns>返回布尔值。</returns>
        protected virtual bool ContainsProtectedUsernames(string name)
        {
            var lower = name.ToLower();

            foreach (var n in ProtectedUsernames)
            {
                if (lower.Contains(n))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// 异步创建用户。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <returns>返回用户身份结果和用户模型。</returns>
        public virtual async Task<(IdentityResult identity, TUserModel model)> CreateAsync(TUserModel user)
        {
            try
            {
                if (ContainsProtectedUsernames(user.Name))
                    return (IdentityResultHelper.InvalidName, user);

                var result = await Repository.ExistsAsync(p => p.Name == user.Name);
                if (result.exists)
                    return (IdentityResultHelper.NameExists, user);

                await Repository.Writer.CreateAsync(user);

                return (IdentityResult.Success, user);
            }
            catch (Exception ex)
            {
                return (IdentityResultHelper.CreateFailed(ex), user);
            }
        }


        /// <summary>
        /// 异步验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <returns>返回用户身份结果和用户模型。</returns>
        public virtual async Task<(IdentityResult identity, TUserModel model)> ValidateAsync(string name, string passwd)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwd))
                return (IdentityResultHelper.InvalidName, null);
            
            // 查找用户
            var user = await Repository.GetAsync(p => p.Name == name);

            if (user == null)
                return (IdentityResultHelper.NameNotExists, user);

            // 验证密码是否正确
            if (!PasswordManager.Validate(user.Passwd, passwd))
                return (IdentityResultHelper.PasswordError, user);

            return (IdentityResult.Success, user);
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

            if (ContainsProtectedUsernames(value))
                return false;
            
            // 建立表达式
            var predicate = field.AsEqualPropertyExpression<TUserModel>(value, typeof(string));

            // 不存在表示唯一
            var result = await Repository.ExistsAsync(predicate);
            return (!result.exists);
        }

    }
}
