#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard.Authentication
{
    using Adaptation;
    using Handlers;
    using Managers;
    using Models;
    using Senders;

    /// <summary>
    /// 认证适配器接口。
    /// </summary>
    public interface IAuthenticationAdapter : IAdapter
    {
        /// <summary>
        /// 令牌处理程序。
        /// </summary>
        ITokenHandler TokenHandler { get; }

        /// <summary>
        /// 密码管理器。
        /// </summary>
        IPasswordManager PasswordManager { get; }

        /// <summary>
        /// 角色管理器。
        /// </summary>
        IRoleManager RoleManager { get; }

        /// <summary>
        /// 令牌管理器。
        /// </summary>
        ITokenManager TokenManager { get; }


        /// <summary>
        /// 邮箱发送器。
        /// </summary>
        IEmailSender EmailSender { get; }

        /// <summary>
        /// 短信发送器。
        /// </summary>
        ISmsSender SmsSender { get; }


        /// <summary>
        /// 获取用户管理器。
        /// </summary>
        /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
        /// <returns>返回用户管理器。</returns>
        IUserManager<TUserModel> GetUserManager<TUserModel>()
            where TUserModel : class, IUserModel;


        /// <summary>
        /// 尝试添加认证模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        ILibrameBuilder TryAddAuthentication();
    }
}
