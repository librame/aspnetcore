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

namespace Librame.AspNetCore.Identity.Accessors
{
    /// <summary>
    /// 演示身份数据库上下文访问器。
    /// </summary>
    public class DemoIdentityDbContextAccessor : IdentityDbContextAccessor<Guid, int, Guid>
    {
        /// <summary>
        /// 构造一个身份数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public DemoIdentityDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }
}
