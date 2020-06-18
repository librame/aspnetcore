#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using Extensions.Core.Services;
    using Extensions.Data.Builders;
    using Identity.Stores;

    /// <summary>
    /// GUID 身份服务器存储标识符生成器。
    /// </summary>
    public class GuidIdentityServerStoreIdentifierGenerator : GuidIdentityStoreIdentifierGenerator
    {
        /// <summary>
        /// 构造一个 <see cref="GuidIdentityServerStoreIdentifierGenerator"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{DataBuilderOptions}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityServerStoreIdentifierGenerator(IClockService clock,
            IOptions<DataBuilderOptions> options, ILoggerFactory loggerFactory)
            : base(clock, options, loggerFactory)
        {
        }

    }
}
