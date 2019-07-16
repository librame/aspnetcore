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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 内部应用程序导航。
    /// </summary>
    internal class InternalApplicationNavigation : IApplicationNavigation
    {
        private readonly IDictionary<string, IList<NavigationDescriptor>> _navigations;


        /// <summary>
        /// 构造一个 <see cref="InternalApplicationNavigation"/> 实例。
        /// </summary>
        public InternalApplicationNavigation()
            : this(null)
        {
        }
        /// <summary>
        /// 构造一个 <see cref="InternalApplicationNavigation"/> 实例。
        /// </summary>
        /// <param name="navigations">给定的导航字典。</param>
        public InternalApplicationNavigation(IDictionary<string, IList<NavigationDescriptor>> navigations)
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


        #region ICollection

        /// <summary>
        /// 导航列表数。
        /// </summary>
        public int Count => _navigations.Count;

        /// <summary>
        /// 是只读。
        /// </summary>
        public bool IsReadOnly => _navigations.IsReadOnly;


        /// <summary>
        /// 添加导航列表项。
        /// </summary>
        /// <param name="item">给定的键列表对。</param>
        public void Add(KeyValuePair<string, IList<NavigationDescriptor>> item)
        {
            AddOrUpdate(item.Key, item.Value);
        }

        /// <summary>
        /// 清空所有导航列表。
        /// </summary>
        public void Clear()
        {
            _navigations.Clear();
        }

        /// <summary>
        /// 包含指定导航列表项。
        /// </summary>
        /// <param name="item">给定的键列表对。</param>
        /// <returns>返回布尔值。</returns>
        public bool Contains(KeyValuePair<string, IList<NavigationDescriptor>> item)
        {
            return _navigations.Contains(item);
        }

        /// <summary>
        /// 复制到目标数组。
        /// </summary>
        /// <param name="array">给定的目标数组。</param>
        /// <param name="arrayIndex">给定的索引。</param>
        public void CopyTo(KeyValuePair<string, IList<NavigationDescriptor>>[] array, int arrayIndex)
        {
            _navigations.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 移除导航列表项。
        /// </summary>
        /// <param name="item">给定的键列表对。</param>
        /// <returns>返回是否成功移除的布尔值。</returns>
        public bool Remove(KeyValuePair<string, IList<NavigationDescriptor>> item)
        {
            return _navigations.Remove(item);
        }

        /// <summary>
        /// 获取枚举器。
        /// </summary>
        /// <returns>返回枚举器。</returns>
        public IEnumerator<KeyValuePair<string, IList<NavigationDescriptor>>> GetEnumerator()
        {
            return _navigations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
