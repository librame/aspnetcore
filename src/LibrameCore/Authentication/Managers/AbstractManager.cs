#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.Extensions.Options;

namespace LibrameCore.Authentication.Managers
{
    /// <summary>
    /// 抽象管理器。
    /// </summary>
    public class AbstractManager : IManager
    {
        /// <summary>
        /// 构造一个抽象管理器实例。
        /// </summary>
        /// <param name="options">给定的认证选项。</param>
        public AbstractManager(IOptions<AuthenticationOptions> options)
        {
            Options = options.NotNull(nameof(options)).Value;
        }

        
        /// <summary>
        /// 认证设置。
        /// </summary>
        public AuthenticationOptions Options { get; }
    }
}
