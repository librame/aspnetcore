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
using System.Runtime.Versioning;

namespace Librame.AspNetCore
{
    /// <summary>
    /// 抽象应用信息。
    /// </summary>
    public abstract class AbstractApplicationInfo : IApplicationInfo
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 标题。
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// 作者集合。
        /// </summary>
        public virtual string Authors
            => "Librame Pang";

        /// <summary>
        /// 联系。
        /// </summary>
        public virtual string Contact
            => "https://github.com/librame/LibrameCore";

        /// <summary>
        /// 公司。
        /// </summary>
        public virtual string Company
            => Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;

        /// <summary>
        /// 版权。
        /// </summary>
        public virtual string Copyright
            => Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

        /// <summary>
        /// 框架。
        /// </summary>
        public virtual string Framework
            => Assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

        /// <summary>
        /// 版本。
        /// </summary>
        public virtual string Version
            => AssemblyVersion.ToString();

        /// <summary>
        /// 程序集。
        /// </summary>
        public virtual Assembly Assembly
            => GetType().Assembly;

        /// <summary>
        /// 程序集名称。
        /// </summary>
        public virtual AssemblyName AssemblyName
            => Assembly.GetName();

        /// <summary>
        /// 程序集版本。
        /// </summary>
        public virtual Version AssemblyVersion
            => AssemblyName.Version;


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="IApplicationInfo"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(IApplicationInfo other)
            => Name == other?.Name;

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is IApplicationInfo other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{Name} - {Title}";


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="a">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <param name="b">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator ==(AbstractApplicationInfo a, AbstractApplicationInfo b)
            => a.Equals(b);

        /// <summary>
        /// 是否不等。
        /// </summary>
        /// <param name="a">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <param name="b">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator !=(AbstractApplicationInfo a, AbstractApplicationInfo b)
            => !a.Equals(b);
    }
}
