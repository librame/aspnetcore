#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameCore.Authentication.Managers
{
    /// <summary>
    /// 管理器接口。
    /// </summary>
    public interface IManager
    {
        /// <summary>
        /// 认证设置。
        /// </summary>
        AuthenticationOptions Options { get; }
    }
}
