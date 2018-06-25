#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Abstractions;
using Microsoft.Extensions.Options;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// 抽象认证扩展模块。
    /// </summary>
    public class AbstractAuthenticationExtensionService<TAuthentication> : AbstractExtensionService<AuthenticationExtensionOptions>, IAuthenticationExtensionService
        where TAuthentication : IAuthenticationExtensionService
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractAuthenticationExtensionService{TModule}"/> 实例。
        /// </summary>
        /// <param name="options">给定的认证选项。</param>
        public AbstractAuthenticationExtensionService(IOptionsMonitor<AuthenticationExtensionOptions> options)
            : base(options)
        {
        }

    }
}
