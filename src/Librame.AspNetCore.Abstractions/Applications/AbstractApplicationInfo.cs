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

namespace Librame.AspNetCore.Applications
{
    using Extensions;

    /// <summary>
    /// 抽象应用信息。
    /// </summary>
    public abstract class AbstractApplicationInfo : IApplicationInfo
    {
        /// <summary>
        /// AspNetCore 项目库 URL。
        /// </summary>
        public const string AspNetCoreRepositoryUrl
            = "https://github.com/librame/aspnetcore";


        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationInfo"/>。
        /// </summary>
        protected AbstractApplicationInfo()
        {
            var setup = AppDomain.CurrentDomain.SetupInformation;
            Framework = setup.TargetFrameworkName;
        }


        /// <summary>
        /// 名称。
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 显示名称。
        /// </summary>
        public string DisplayName
            => Localizer.GetString(nameof(DisplayName));

        /// <summary>
        /// 作者集合。
        /// </summary>
        public virtual string Authors
            => "Librame Pang";

        /// <summary>
        /// 联系。
        /// </summary>
        public virtual string Contact
            => AspNetCoreRepositoryUrl;

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
        public virtual string Framework { get; }

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
            => ToString().GetHashCode(StringComparison.OrdinalIgnoreCase);


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{Name}:{DisplayName}";


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
        /// <param name="applicationInfo">给定的 <see cref="AbstractApplicationInfo"/>。</param>
        public static implicit operator string(AbstractApplicationInfo applicationInfo)
            => applicationInfo?.ToString();
    }
}
