#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Interfaces;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions.Data;

    /// <summary>
    /// 身份服务器数据库上下文访问器接口。
    /// </summary>
    public interface IIdentityServerDbContextAccessor : IAccessor, IConfigurationDbContext
    {
    }
}
