#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Microsoft.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// HTML 定位器静态扩展。
    /// </summary>
    public static class HtmlLocalizerExtensions
    {
        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static LocalizedHtmlString GetString<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> propertyExpression)
            where TResource : class
        {
            localizer.NotNull(nameof(localizer));
            return localizer[propertyExpression.AsPropertyName()];
        }

        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static LocalizedHtmlString GetString<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> propertyExpression, params object[] arguments)
            where TResource : class
        {
            localizer.NotNull(nameof(localizer));
            return localizer[propertyExpression.AsPropertyName(), arguments];
        }

    }
}
