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
using System;
using System.Threading.Tasks;

namespace LibrameStandard.Authentication.Managers
{
    using Entity;
    using Models;
    using Utilities;

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
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public UserManager(ILibrameBuilder builder)
            : base(builder)
        {
        }


        /// <summary>
        /// 密码管理器。
        /// </summary>
        public IPasswordManager PasswordManager => Builder.GetService<IPasswordManager>();


        /// <summary>
        /// 异步创建用户。
        /// </summary>
        /// <param name="model">给定的用户模型。</param>
        /// <returns>返回用户身份结果。</returns>
        public virtual async Task<UserIdentityResult> CreateAsync(TUserModel model)
        {
            var repository = Builder.GetRepositoryReaderWriter<TUserModel>();

            try
            {
                await repository.Writer.CreateAsync(model);

                return new UserIdentityResult(IdentityResult.Success, model);
            }
            catch (Exception ex)
            {
                return new UserIdentityResult(IdentityResult.Failed(UserIdentityErrors.CreateUserError(ex)), model);
            }
        }


        /// <summary>
        /// 异步验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <returns>返回用户身份结果。</returns>
        public virtual async Task<UserIdentityResult> ValidateAsync(string name, string passwd)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwd))
                return null;

            // 获取用户仓库
            var repository = Builder.GetRepositoryReaderWriter<TUserModel>();

            // 查找用户名称
            var model = await repository.GetAsync(p => p.Name == name);

            if (model == null)
                return new UserIdentityResult(IdentityResult.Failed(UserIdentityErrors.NameNotExists));

            if (!PasswordManager.Validate(model.Passwd, passwd))
                return new UserIdentityResult(IdentityResult.Failed(UserIdentityErrors.PasswordError));

            return new UserIdentityResult(IdentityResult.Success, model);
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

            // 获取用户仓库
            var repository = Builder.GetRepositoryReaderWriter<TUserModel>();

            // 查找用户
            var predicate = field.AsEqualPropertyExpression<TUserModel>(value, typeof(string));
            var exists = await repository.ExistsAsync(predicate);

            // 不存在表示唯一
            return (!exists);
        }

    }
}
