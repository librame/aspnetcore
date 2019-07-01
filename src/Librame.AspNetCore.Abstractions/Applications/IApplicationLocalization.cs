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
using System.Collections.Generic;

namespace Librame.AspNetCore
{
    /// <summary>
    /// 应用程序本地化接口。
    /// </summary>
    public interface IApplicationLocalization
    {
        /// <summary>
        /// 所有键集合。
        /// </summary>
        /// <value>返回 <see cref="ICollection{String}"/>。</value>
        ICollection<string> AllKeys { get; }

        /// <summary>
        /// 所有定位器集合。
        /// </summary>
        /// <value>返回 <see cref="ICollection{IStringLocalizer}"/>。</value>
        ICollection<IStringLocalizer> AllLocalizers { get; }


        /// <summary>
        /// 获取指定键名的定位器。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        IStringLocalizer this[string key] { get; }


        /// <summary>
        /// 增加或更新本地化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key or localizer is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        void AddOrUpdate(string key, IStringLocalizer localizer);


        /// <summary>
        /// 包含指定键名的本地化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回布尔值。</returns>
        bool ContainsKey(string key);


        /// <summary>
        /// 尝试获取指定键名的本地化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="localizer">输出 <see cref="IStringLocalizer"/>。</param>
        /// <returns>返回布尔值。</returns>
        bool TryGet(string key, out IStringLocalizer localizer);
    }
}
