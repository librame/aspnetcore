#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Reflection;

namespace Librame.AspNetCore
{
    /// <summary>
    /// 应用信息接口。
    /// </summary>
    public interface IApplicationInfo : IEquatable<IApplicationInfo>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 标题。
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 作者。
        /// </summary>
        string Author { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        string Contact { get; }

        /// <summary>
        /// 版权。
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// 版本。
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 程序集。
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 程序集版本。
        /// </summary>
        Version AssemblyVersion { get; }
    }
}
