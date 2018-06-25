#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Abstractions;

namespace LibrameCore.Extensions.Server
{
    /// <summary>
    /// 服务器扩展服务接口。
    /// </summary>
    public interface IServerExtensionService : IExtensionService<ServerExtensionOptions>
    {
    }
}
