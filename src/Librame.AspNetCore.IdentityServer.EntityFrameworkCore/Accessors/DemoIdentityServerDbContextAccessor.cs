#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    /// <summary>
    /// 演示身份服务器数据库上下文访问器（集成身份数据库）。
    /// </summary>
    public class DemoIdentityServerDbContextAccessor : IdentityServerDbContextAccessor<Guid, int, Guid>
    {
        /// <summary>
        /// 构造一个身份服务器数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public DemoIdentityServerDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }
}
