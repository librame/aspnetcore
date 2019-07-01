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
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 应用程序导航。
    /// </summary>
    public class ApplicationNavigation : IApplicationNavigation
    {
        private readonly IDictionary<string, IList<NavigationDescriptor>> _navigations;


        /// <summary>
        /// 构造一个 <see cref="ApplicationNavigation"/> 实例。
        /// </summary>
        public ApplicationNavigation()
            : this(null)
        {
        }
        /// <summary>
        /// 构造一个 <see cref="ApplicationNavigation"/> 实例。
        /// </summary>
        /// <param name="navigations">给定的导航字典。</param>
        public ApplicationNavigation(IDictionary<string, IList<NavigationDescriptor>> navigations)
        {
            _navigations = navigations ?? new Dictionary<string, IList<NavigationDescriptor>>();
        }


        /// <summary>
        /// 所有键集合。
        /// </summary>
        /// <value>返回 <see cref="ICollection{String}"/>。</value>
        public ICollection<string> AllKeys
        {
            get { return _navigations.Keys; }
        }

        /// <summary>
        /// 所有值集合。
        /// </summary>
        /// <value>返回列表集合。</value>
        public ICollection<IList<NavigationDescriptor>> AllValues
        {
            get { return _navigations.Values; }
        }


        /// <summary>
        /// 获取指定键名的描述符列表。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回 <see cref="IList{NavigationDescriptor}"/> 或 NULL。</returns>
        public IList<NavigationDescriptor> this[string key]
        {
            get
            {
                if (TryGet(key, out IList<NavigationDescriptor> descriptors))
                    return descriptors;

                return null;
            }
        }


        /// <summary>
        /// 增加或更新导航。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key or descriptors is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="descriptors">给定的 <see cref="IList{NavigationDescriptor}"/>。</param>
        public void AddOrUpdate(string key, IList<NavigationDescriptor> descriptors)
        {
            descriptors.NotNullOrEmpty(nameof(descriptors));

            if (_navigations.ContainsKey(key))
            {
                var exists = _navigations[key];
                if (exists.IsNotNullOrEmpty())
                {
                    _navigations[key] = exists.Union(descriptors).ToList();
                }
                else
                {
                    _navigations[key] = descriptors;
                }
            }
            else
            {
                _navigations.Add(key, descriptors);
            }
        }


        /// <summary>
        /// 包含指定键名的导航。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回布尔值。</returns>
        public bool ContainsKey(string key)
        {
            return _navigations.ContainsKey(key);
        }


        /// <summary>
        /// 尝试获取指定键名的导航。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="descriptors">输出 <see cref="IList{NavigationDescriptor}"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool TryGet(string key, out IList<NavigationDescriptor> descriptors)
        {
            return _navigations.TryGetValue(key, out descriptors);
        }

    }
}
