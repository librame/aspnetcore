#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;

namespace LibrameStandard.Handlers
{
    /// <summary>
    /// 处理程序设置接口。
    /// </summary>
    public interface IHandlerSettings : ILibrameSettings
    {
        /// <summary>
        /// 映射路径。
        /// </summary>
        PathString Path { get; set; }
    }
}
