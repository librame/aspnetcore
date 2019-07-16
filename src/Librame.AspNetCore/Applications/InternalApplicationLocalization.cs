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
using System.Collections;
using System.Collections.Generic;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// 内部应用程序本地化。
    /// </summary>
    internal class InternalApplicationLocalization : IApplicationLocalization
    {
        private readonly IDictionary<string, IStringLocalizer> _localizations;


        /// <summary>
        /// 构造一个 <see cref="InternalApplicationLocalization"/> 实例。
        /// </summary>
        public InternalApplicationLocalization()
            : this(null)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="InternalApplicationLocalization"/> 实例。
        /// </summary>
        /// <param name="localizations">给定的本地化字典集合。</param>
        public InternalApplicationLocalization(IDictionary<string, IStringLocalizer> localizations)
        {
            _localizations = localizations ?? new Dictionary<string, IStringLocalizer>();
        }


        /// <summary>
        /// 所有键集合。
        /// </summary>
        /// <value>返回 <see cref="ICollection{String}"/>。</value>
        public ICollection<string> AllKeys
        {
            get { return _localizations.Keys; }
        }

        /// <summary>
        /// 所有定位器集合。
        /// </summary>
        /// <value>返回 <see cref="ICollection{IStringLocalizer}"/>。</value>
        public ICollection<IStringLocalizer> AllLocalizers
        {
            get { return _localizations.Values; }
        }


        /// <summary>
        /// 获取指定键名的定位器。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/> 或 NULL。</returns>
        public IStringLocalizer this[string key]
        {
            get
            {
                if (TryGet(key, out IStringLocalizer localizer))
                    return localizer;

                return null;
            }
        }


        /// <summary>
        /// 增加或更新本地化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key or localizer is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        public void AddOrUpdate(string key, IStringLocalizer localizer)
        {
            localizer.NotNull(nameof(localizer));

            if (_localizations.ContainsKey(key))
            {
                _localizations[key] = localizer;
            }
            else
            {
                _localizations.Add(key, localizer);
            }
        }


        /// <summary>
        /// 包含指定键名的本地化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <returns>返回布尔值。</returns>
        public bool ContainsKey(string key)
        {
            return _localizations.ContainsKey(key);
        }


        /// <summary>
        /// 尝试获取指定键名的本地化。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// key is null or empty.
        /// </exception>
        /// <param name="key">给定的键名。</param>
        /// <param name="localizer">输出 <see cref="IStringLocalizer"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool TryGet(string key, out IStringLocalizer localizer)
        {
            return _localizations.TryGetValue(key, out localizer);
        }


        #region ICollection

        /// <summary>
        /// 字符串定位器数。
        /// </summary>
        public int Count => _localizations.Count;

        /// <summary>
        /// 是只读。
        /// </summary>
        public bool IsReadOnly => _localizations.IsReadOnly;


        /// <summary>
        /// 添加字符串定位器项。
        /// </summary>
        /// <param name="item">给定的键值对。</param>
        public void Add(KeyValuePair<string, IStringLocalizer> item)
        {
            AddOrUpdate(item.Key, item.Value);
        }

        /// <summary>
        /// 清除所有字符串定位器项。
        /// </summary>
        public void Clear()
        {
            _localizations.Clear();
        }

        /// <summary>
        /// 包含指定字符串定位器项。
        /// </summary>
        /// <param name="item">给定的键值对。</param>
        /// <returns>返回布尔值。</returns>
        public bool Contains(KeyValuePair<string, IStringLocalizer> item)
        {
            return _localizations.Contains(item);
        }

        /// <summary>
        /// 复制到目标数组。
        /// </summary>
        /// <param name="array">给定的目标数组。</param>
        /// <param name="arrayIndex">给定的索引。</param>
        public void CopyTo(KeyValuePair<string, IStringLocalizer>[] array, int arrayIndex)
        {
            _localizations.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 移除字符串定位器项。
        /// </summary>
        /// <param name="item">给定的键列表对。</param>
        /// <returns>返回是否成功移除的布尔值。</returns>
        public bool Remove(KeyValuePair<string, IStringLocalizer> item)
        {
            return _localizations.Remove(item);
        }

        /// <summary>
        /// 获取枚举器。
        /// </summary>
        /// <returns>返回枚举器。</returns>
        public IEnumerator<KeyValuePair<string, IStringLocalizer>> GetEnumerator()
        {
            return _localizations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
