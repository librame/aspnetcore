#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace Librame.AspNetCore
{
    using Extensions;
    using Extensions.Core;

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
        /// 本地化定位器。
        /// </summary>
        public abstract IStringLocalizer Localizer { get; }

        /// <summary>
        /// 服务工厂。
        /// </summary>
        public ServiceFactoryDelegate ServiceFactory { get; private set; }


        /// <summary>
        /// 应用服务工厂。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        /// <returns>返回 <see cref="IApplicationInfo"/>。</returns>
        public virtual IApplicationInfo ApplyServiceFactory(ServiceFactoryDelegate serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));
            return this;
        }


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
            => Localizer[nameof(Name)];


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="a">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <param name="b">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator ==(AbstractApplicationInfo a, AbstractApplicationInfo b)
            => (a?.Equals(b)).Value;

        /// <summary>
        /// 是否不等。
        /// </summary>
        /// <param name="a">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <param name="b">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator !=(AbstractApplicationInfo a, AbstractApplicationInfo b)
            => !(a?.Equals(b)).Value;


        /// <summary>
        /// 隐式转换为字符串形式。
        /// </summary>
        /// <param name="identifier">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        public static implicit operator string(AbstractApplicationInfo identifier)
            => identifier?.ToString();
    }
}
