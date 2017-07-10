#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Handlers;

namespace LibrameCore.Authentication.Handlers
{
    using Managers;
    using Models;

    /// <summary>
    /// 令牌处理程序接口。
    /// </summary>
    /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
    public interface ITokenHandler<TUserModel> : IHander
        where TUserModel : class, IUserModel
    {
        /// <summary>
        /// 令牌管理器。
        /// </summary>
        ITokenManager TokenManager { get; }
        
        /// <summary>
        /// 用户管理器。
        /// </summary>
        IUserManager<TUserModel> UserManager { get; }

        /// <summary>
        /// 角色管理器。
        /// </summary>
        IRoleManager RoleManager { get; }


        /// <summary>
        /// 参数设置。
        /// </summary>
        TokenHandlerSettings Settings { get; }
    }
}
