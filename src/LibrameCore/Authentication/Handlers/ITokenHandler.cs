#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Handlers;

namespace LibrameStandard.Authentication.Handlers
{
    using Managers;

    /// <summary>
    /// 令牌处理程序接口。
    /// </summary>
    public interface ITokenHandler : IHander<TokenHandlerSettings>
    {
        /// <summary>
        /// 令牌管理器。
        /// </summary>
        ITokenManager TokenManager { get; }
    }
}
