#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions.Core;
    using Identity;

    /// <summary>
    /// 身份服务器存储标识符。
    /// </summary>
    public class IdentityServerStoreIdentifier : IdentityStoreIdentifier
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityStoreIdentifier"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityServerStoreIdentifier(IClockService clock, ILoggerFactory loggerFactory)
            : base(clock, loggerFactory)
        {
        }

    }
}
