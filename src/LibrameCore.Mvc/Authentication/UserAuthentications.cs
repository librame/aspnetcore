#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameCore.Authentication
{
    /// <summary>
    /// 用户认证接口。
    /// </summary>
    public interface IUserAuthentication
    {
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
    /// 用户认证。
    /// </summary>
    public class UserAuthentication : IUserAuthentication
    {
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
