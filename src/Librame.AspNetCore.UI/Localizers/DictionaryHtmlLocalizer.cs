#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

//using Microsoft.AspNetCore.Mvc.Localization;
//using Microsoft.Extensions.Localization;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Globalization;

//namespace Librame.AspNetCore.UI
//{
//    using Extensions;
//    using Extensions.Core;
    
//    /// <summary>
//    /// 字典 HTML 定位器。
//    /// </summary>
//    public class DictionaryHtmlLocalizer : IHtmlLocalizer
//    {
//        /// <summary>
//        /// 构造一个 <see cref="DictionaryStringLocalizer"/>。
//        /// </summary>
//        /// <param name="manager">给定的 <see cref="ResourceDictionaryManager"/>。</param>
//        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "manager")]
//        public DictionaryHtmlLocalizer(ResourceDictionaryManager manager)
//        {
//            Manager = manager.NotNull(nameof(manager));
//            Dictionary = manager.CreateDictionary();
//        }


//        /// <summary>
//        /// 管理器。
//        /// </summary>
//        protected ResourceDictionaryManager Manager { get; }

//        /// <summary>
//        /// 资源字典。
//        /// </summary>
//        protected IResourceDictionary Dictionary { get; }


//        /// <summary>
//        /// 获取指定名称的本地化字符串。
//        /// </summary>
//        /// <param name="name">给定的名称。</param>
//        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
//        public LocalizedHtmlString this[string name]
//            => ToLocalizedHtmlString(name, !Dictionary.TryGetValue(name, out object value), value?.ToString());

//        /// <summary>
//        /// 获取指定名称的本地化字符串。
//        /// </summary>
//        /// <param name="name">给定的名称。</param>
//        /// <param name="arguments">给定的参数数组。</param>
//        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
//        public LocalizedHtmlString this[string name, params object[] arguments]
//            => ToLocalizedHtmlString(name, !Dictionary.TryGetValue(name, out object value), value?.ToString(), arguments);


//        /// <summary>
//        /// 获取所有本地化的字符串。
//        /// </summary>
//        /// <param name="includeParentCultures">参数无效。</param>
//        /// <returns>返回 <see cref="IEnumerable{LocalizedString}"/>。</returns>
//        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
//        {
//            foreach (var pair in Dictionary)
//            {
//                yield return ToLocalizedString(pair.Key, true, pair.Value?.ToString());
//            }
//        }


//        /// <summary>
//        /// 获取字符串。
//        /// </summary>
//        /// <param name="name">给定的名称。</param>
//        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
//        public LocalizedString GetString(string name)
//            => ToLocalizedString(name, !Dictionary.TryGetValue(name, out object value), value?.ToString());

//        /// <summary>
//        /// 获取字符串。
//        /// </summary>
//        /// <param name="name">给定的名称。</param>
//        /// <param name="arguments">给定的参数数组。</param>
//        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
//        public LocalizedString GetString(string name, params object[] arguments)
//            => ToLocalizedString(name, !Dictionary.TryGetValue(name, out object value), value?.ToString().Format(arguments));


//        /// <summary>
//        /// 转换为本地化的 HTML 字符串。
//        /// </summary>
//        /// <param name="name">给定的名称。</param>
//        /// <param name="resourceNotFound">未找到此资源。</param>
//        /// <param name="value">给定的值。</param>
//        /// <param name="arguments">给定的参数数组。</param>
//        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
//        [SuppressMessage("Microsoft.Design", "CA1822:MarkMembersAsStatic")]
//        protected LocalizedHtmlString ToLocalizedHtmlString(string name, bool resourceNotFound, string value, params object[] arguments)
//            => new LocalizedHtmlString(name, value.NotEmptyOrDefault(name), resourceNotFound, arguments);

//        /// <summary>
//        /// 转换为本地化的字符串。
//        /// </summary>
//        /// <param name="name">给定的名称。</param>
//        /// <param name="resourceNotFound">未找到此资源。</param>
//        /// <param name="value">给定的值。</param>
//        /// <returns>返回 <see cref="LocalizedString"/>。</returns>
//        [SuppressMessage("Microsoft.Design", "CA1822:MarkMembersAsStatic")]
//        protected LocalizedString ToLocalizedString(string name, bool resourceNotFound, string value)
//            => new LocalizedString(name, value.NotEmptyOrDefault(name), resourceNotFound, searchedLocation: null);


//        /// <summary>
//        /// 带文化信息。
//        /// </summary>
//        /// <param name="culture">给定的 <see cref="CultureInfo"/>（可选；默认为 <see cref="CultureInfo.CurrentUICulture"/>）。</param>
//        /// <returns>返回 <see cref="IHtmlLocalizer"/>。</returns>
//        [Obsolete("This method is obsolete. Use `CurrentCulture` and `CurrentUICulture` instead.")]
//        public IHtmlLocalizer WithCulture(CultureInfo culture)
//        {
//            CultureInfo.CurrentUICulture = culture;
//            return new DictionaryHtmlLocalizer(Manager);
//        }

//    }
//}
