#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard.Authentication.Managers
{
    using Utilities;

    /// <summary>
    /// 用户管理器接口。
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }


        /// <summary>
        /// 验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <param name="user">输出用户模型接口。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        bool Validate(string name, string passwd, out IUserModel user);
    }


    /// <summary>
    /// 用户管理器。
    /// </summary>
    public class UserManager : IUserManager
    {
        /// <summary>
        /// 构造一个用户管理器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public UserManager(ILibrameBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }


        /// <summary>
        /// 验证用户。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <param name="user">输出用户模型接口。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        public virtual bool Validate(string name, string passwd, out IUserModel user)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passwd))
            {
                user = null;
                return false;
            }

            return ValidateCore(name, passwd, out user);
        }

        /// <summary>
        /// 验证核心。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="passwd">给定的密码。</param>
        /// <param name="user">输出用户模型接口。</param>
        /// <returns>返回是否通过验证的布尔值。</returns>
        protected virtual bool ValidateCore(string name, string passwd, out IUserModel user)
        {
            // 默认用户
            var defaultUser = new UserModel();
            if (name == defaultUser.Name && passwd == defaultUser.Passwd)
            {
                user = new UserModel()
                {
                    Passwd = string.Empty
                };
                return true;
            }

            user = null;
            return false;
        }

    }
}
