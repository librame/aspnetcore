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

    }
}
