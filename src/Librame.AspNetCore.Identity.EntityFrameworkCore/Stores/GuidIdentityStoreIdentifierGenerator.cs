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
using System;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions.Core.Services;
    using Extensions.Data.Builders;

    /// <summary>
    /// GUID 身份存储标识符生成器。
    /// </summary>
    public class GuidIdentityStoreIdentifierGenerator : AbstractIdentityStoreIdentifierGenerator<Guid>
    {
        /// <summary>
        /// 构造一个 <see cref="GuidIdentityStoreIdentifierGenerator"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{DataBuilderOptions}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityStoreIdentifierGenerator(IClockService clock,
            IOptions<DataBuilderOptions> options, ILoggerFactory loggerFactory)
            : base(clock, options?.Value.IdentifierGenerator, loggerFactory)
        {
        }

    }
}
