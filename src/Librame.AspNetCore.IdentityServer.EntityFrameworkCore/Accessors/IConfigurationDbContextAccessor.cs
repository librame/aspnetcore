#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Interfaces;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Extensions.Data.Accessors;

    /// <summary>
    /// 配置数据库上下文访问器接口。
    /// </summary>
    public interface IConfigurationDbContextAccessor : IAccessor, IConfigurationDbContext
    {
    }
}
