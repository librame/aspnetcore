﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using AspNetCore.Identity.Stores;
    using Extensions.Core.Identifiers;
    using Extensions.Core.Services;

    /// <summary>
    /// <see cref="long"/> 身份服务器存储标识生成器。
    /// </summary>
    public class LongIdentityServerStoreIdentificationGenerator : LongIdentityStoreIdentificationGenerator
    {
        /// <summary>
        /// 构造一个 <see cref="LongIdentityServerStoreIdentificationGenerator"/>。
        /// </summary>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="factory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public LongIdentityServerStoreIdentificationGenerator(IClockService clock,
            IIdentificationGeneratorFactory factory, ILoggerFactory loggerFactory)
            : base(clock, factory, loggerFactory)
        {
        }

    }
}
