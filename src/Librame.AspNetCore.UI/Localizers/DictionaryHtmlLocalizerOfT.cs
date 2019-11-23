#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 字典 HTML 定位器。
    /// </summary>
    /// <typeparam name="TResource">指定的资源类型。</typeparam>
    public class DictionaryHtmlLocalizer<TResource> : IDictionaryHtmlLocalizer<TResource>
    {
        private IHtmlLocalizer _localizer;


        /// <summary>
        /// 构造一个字典字符串定位器。
        /// </summary>
        /// <param name="factory">给定的 <see cref="IDictionaryHtmlLocalizerFactory"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "factory")]
        public DictionaryHtmlLocalizer(IDictionaryHtmlLocalizerFactory factory)
        {
            factory.NotNull(nameof(factory));
            _localizer = factory.Create(typeof(TResource));
        }


        /// <summary>
        /// 获取指定名称的本地化字符串。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public LocalizedHtmlString this[string name]
            => _localizer[name];

        /// <summary>
        /// 获取指定名称的本地化字符串。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        public LocalizedHtmlString this[string name, params object[] arguments]
            => _localizer[name, arguments];


        /// <summary>
        /// 获取所有本地化的字符串。
        /// </summary>
        /// <param name="includeParentCultures">参数无效。</param>
        /// <returns>返回 <see cref="IEnumerable{LocalizedString}"/>。</returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => _localizer.GetAllStrings(includeParentCultures);


        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public LocalizedString GetString(string name)
            => _localizer.GetString(name);

        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
        public LocalizedString GetString(string name, params object[] arguments)
            => _localizer.GetString(name, arguments);


        /// <summary>
        /// 带文化信息。
        /// </summary>
        /// <param name="culture">给定的 <see cref="CultureInfo"/>（可选；默认为 <see cref="CultureInfo.CurrentUICulture"/>）。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer"/>。</returns>
        [Obsolete("This method is obsolete. Use `CurrentCulture` and `CurrentUICulture` instead.")]
        public IHtmlLocalizer WithCulture(CultureInfo culture)
            => _localizer.WithCulture(culture);
    }
}
