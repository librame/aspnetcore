#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Identity.Accessors;

    /// <summary>
    /// 身份服务器数据库上下文访问器接口。
    /// </summary>
    public interface IIdentityServerDbContextAccessor : IIdentityDbContextAccessor, IConfigurationDbContextAccessor, IPersistedGrantDbContextAccessor
    {
    }
}
