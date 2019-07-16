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

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 抽象应用程序信息。
    /// </summary>
    public abstract class AbstractApplicationInfo : IApplicationInfo
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public abstract string Name { get;}

        /// <summary>
        /// 标题。
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        public abstract string Contact { get; }

        /// <summary>
        /// 版权。
        /// </summary>
        public abstract string Copyright { get; }

        /// <summary>
        /// 版本。
        /// </summary>
        public abstract string Version { get; }

        /// <summary>
        /// 程序集。
        /// </summary>
        /// <value>返回 <see cref="System.Reflection.Assembly"/>。</value>
        public virtual Assembly Assembly
            => GetType().Assembly;

        /// <summary>
        /// 程序集版本。
        /// </summary>
        /// <value>返回 <see cref="System.Version"/>。</value>
        public virtual Version AssemblyVersion
            => Assembly.GetName().Version;
    }
}
