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
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 应用程序导航接口。
    /// </summary>
    public interface IApplicationNavigation
    {
        /// <summary>
        /// 所有键集合。
        /// </summary>
        /// <value>返回 <see cref="ICollection{String}"/>。</value>
        ICollection<string> AllKeys { get; }

        /// <summary>
        /// 所有值集合。
        /// </summary>
        /// <value>返回列表集合。</value>
        ICollection<IList<NavigationDescriptor>> AllValues { get; }


        /// <summary>
        /// 获取指定键名的描述符列表。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        IList<NavigationDescriptor> this[string key] { get; }


        /// <summary>
        /// 增加或更新导航。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key or descriptors is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="descriptors">给定的 <see cref="IList{NavigationDescriptor}"/>。</param>
        void AddOrUpdate(string key, IList<NavigationDescriptor> descriptors);


        /// <summary>
        /// 包含指定键名的导航。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回布尔值。</returns>
        bool ContainsKey(string key);


        /// <summary>
        /// 尝试获取指定键名的导航。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="descriptors">输出 <see cref="IList{NavigationDescriptor}"/>。</param>
        /// <returns>返回布尔值。</returns>
        bool TryGet(string key, out IList<NavigationDescriptor> descriptors);
    }
}
