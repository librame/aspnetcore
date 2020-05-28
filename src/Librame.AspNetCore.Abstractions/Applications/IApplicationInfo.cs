#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System;
using System.Reflection;

namespace Librame.AspNetCore.Applications
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
        /// 显示名称。
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// 作者集合。
        /// </summary>
        string Authors { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        string Contact { get; }

        /// <summary>
        /// 公司。
        /// </summary>
        string Company { get; }

        /// <summary>
        /// 版权。
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// 框架。
        /// </summary>
        string Framework { get; }

        /// <summary>
        /// 版本。
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 程序集。
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 程序集名称。
        /// </summary>
        AssemblyName AssemblyName { get; }

        /// <summary>
        /// 程序集版本。
        /// </summary>
        Version AssemblyVersion { get; }


        /// <summary>
        /// 本地化定位器。
        /// </summary>
        IStringLocalizer Localizer { get; }
    }
}
