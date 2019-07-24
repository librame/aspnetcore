#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    /// <summary>
    /// URI 定位符接口。
    /// </summary>
    public interface IUriLocator : ILocator<Uri>
    {
        /// <summary>
        /// 协议。
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// 主机。
        /// </summary>
        HostString Host { get; }

        /// <summary>
        /// 路径。
        /// </summary>
        PathString Path { get; }

        /// <summary>
        /// 查询字符串。
        /// </summary>
        QueryString Query { get; }

        /// <summary>
        /// 锚点。
        /// </summary>
        string Anchor { get; }
    }
}
