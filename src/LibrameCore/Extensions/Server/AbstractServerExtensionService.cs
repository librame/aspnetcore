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
using Microsoft.Extensions.Options;

namespace LibrameCore.Extensions.Server
{
    /// <summary>
    /// 抽象服务器扩展模块。
    /// </summary>
    /// <typeparam name="TServer">指定的服务器类型。</typeparam>
    public class AbstractServerExtensionService<TServer> : AbstractExtensionService<ServerExtensionOptions>, IServerExtensionService
        where TServer : IServerExtensionService
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractServerExtensionService{TServer}"/> 实例。
        /// </summary>
        /// <param name="options">给定的服务器选项。</param>
        public AbstractServerExtensionService(IOptionsMonitor<ServerExtensionOptions> options)
            : base(options)
        {
        }

    }
}
