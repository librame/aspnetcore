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
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// HTML 定位器静态扩展。
    /// </summary>
    public static class HtmlLocalizerExtensions
    {

        #region IHtmlLocalizer

        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        public static LocalizedHtmlString GetString<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> propertyExpression)
            where TResource : class
            => localizer[propertyExpression.AsPropertyName()];

        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        public static LocalizedHtmlString GetString<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> propertyExpression, params object[] arguments)
            where TResource : class
            => localizer[propertyExpression.AsPropertyName(), arguments];

        #endregion


        #region WithCulture

        /// <summary>
        /// 创建一个指定文化名称的 HTML 定位器副本。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="cultureName">给定的文化名称。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer"/>。</returns>
        public static IHtmlLocalizer WithCulture(this IHtmlLocalizer localizer, string cultureName)
            => localizer.WithCulture(CultureInfo.CreateSpecificCulture(cultureName));

        /// <summary>
        /// 创建一个指定文化名称的 HTML 定位器副本。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{TResource}"/>。</param>
        /// <param name="cultureName">给定的文化名称。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer{TResource}"/>。</returns>
        public static IHtmlLocalizer<TResource> WithCulture<TResource>(this IHtmlLocalizer<TResource> localizer, string cultureName)
            => localizer.WithCulture<TResource>(CultureInfo.CreateSpecificCulture(cultureName));

        /// <summary>
        /// 创建一个指定文化名称的 HTML 定位器副本。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{TResource}"/>。</param>
        /// <param name="culture">给定的 <see cref="CultureInfo"/>。</param>
        /// <returns>返回 <see cref="IHtmlLocalizer{TResource}"/>。</returns>
        public static IHtmlLocalizer<TResource> WithCulture<TResource>(this IHtmlLocalizer<TResource> localizer, CultureInfo culture)
        {
            localizer.WithCulture(culture);
            return localizer;
        }

        /// <summary>
        /// 创建一个指定文化名称的 HTML 定位器副本。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IExpressionHtmlLocalizer{TResource}"/>。</param>
        /// <param name="cultureName">给定的文化名称。</param>
        /// <returns>返回 <see cref="IExpressionHtmlLocalizer{TResource}"/>。</returns>
        public static IExpressionHtmlLocalizer<TResource> WithCulture<TResource>(this IExpressionHtmlLocalizer<TResource> localizer, string cultureName)
            => localizer.WithCulture<TResource>(CultureInfo.CreateSpecificCulture(cultureName));

        /// <summary>
        /// 创建一个指定文化名称的 HTML 定位器副本。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IExpressionHtmlLocalizer{TResource}"/>。</param>
        /// <param name="culture">给定的 <see cref="CultureInfo"/>。</param>
        /// <returns>返回 <see cref="IExpressionHtmlLocalizer{TResource}"/>。</returns>
        public static IExpressionHtmlLocalizer<TResource> WithCulture<TResource>(this IExpressionHtmlLocalizer<TResource> localizer, CultureInfo culture)
        {
            localizer.WithCulture(culture);
            return localizer;
        }

        #endregion

    }
}
