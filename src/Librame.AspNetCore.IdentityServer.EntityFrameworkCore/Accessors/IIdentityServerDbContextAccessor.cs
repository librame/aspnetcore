#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    /// <summary>
    /// 身份服务器数据库上下文访问器接口。
    /// </summary>
    public interface IIdentityServerDbContextAccessor : IConfigurationDbContextAccessor, IPersistedGrantDbContextAccessor
    {
    }
}
