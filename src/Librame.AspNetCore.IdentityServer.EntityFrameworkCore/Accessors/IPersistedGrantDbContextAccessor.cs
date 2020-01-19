﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Interfaces;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Extensions.Data.Accessors;

    /// <summary>
    /// 持久化授予数据库上下文访问器接口。
    /// </summary>
    public interface IPersistedGrantDbContextAccessor : IAccessor, IPersistedGrantDbContext
    {
    }
}
